# EBanking

A WPF desktop application that simulates a small retail-banking workflow:
login, view accounts and transaction history, make a payment, and run a
currency exchange between two of your own accounts. School / portfolio
project, not a real banking system.

## Tech stack

- **.NET 6** (WPF, `net6.0-windows`)
- **MVVM:** [CommunityToolkit.Mvvm](https://learn.microsoft.com/dotnet/communitytoolkit/mvvm/) for `RelayCommand` and observables, **MvvmLight** `SimpleIoc` as the DI container, [Fody.PropertyChanged](https://github.com/Fody/PropertyChanged) for INPC weaving
- **Data access:** raw ADO.NET (`System.Data.SqlClient`) against SQL Server Express
- **Password hashing:** [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net) (work-factor 11)
- **Configuration:** `App.config` via `System.Configuration.ConfigurationManager`

The solution has four projects:

| Project | Purpose |
| --- | --- |
| `EBanking.UI` | WPF entry point (Views, ViewModels, Models, validators) |
| `EBanking.Services` | Business-logic layer over the repositories |
| `EBanking.DataAccess` | Repositories, ADO.NET, `PasswordHasher` |
| `EBanking.TestConsole` | One-shot utility: generate BCrypt hashes for seed data, or re-hash any plaintext rows left in a legacy dev database |

## Running it locally

1. Open `EBankingSystem_Create.sql` in SQL Server Management Studio against a local `SQLEXPRESS` instance and run it. This drops and recreates the `EBankingSystem` database.
2. Run `EBankingSystem_Seed.sql`. This inserts the demo users (with BCrypt-hashed passwords), accounts, cards, currency rates, and a handful of transactions.
3. If your SQL Server isn't `.\SQLEXPRESS` or the database name differs, edit the connection string in `EBanking.UI/App.config` — it's the single source of truth, and all four repositories read from it.
4. Open `EBanking.sln` in Visual Studio 2022 and run `EBanking.UI`.

### Seed credentials

Demo passwords are stored as BCrypt hashes; the plaintext values below are the seeded passwords (kept readable so reviewers can sign in):

| Email | Password |
| --- | --- |
| `ana.petrovic@email.com` | `password123` |
| `marko.jovanovic@email.com` | `password456` |
| `jelena.nikolic@email.com` | `password789` |

### Re-hashing a stale dev database

If you have an older snapshot of the database where `[User].password` is still plaintext, run the migration utility once:

```
dotnet run --project EBanking.TestConsole
```

It picks up any row where `password NOT LIKE '$2%'` and rehashes it in place. The pass is idempotent — running it on an already-hashed table is a no-op.

To generate a hash for a new password (e.g. when editing the seed script):

```
dotnet run --project EBanking.TestConsole -- hash MyNewPassword
```

## Modernization pass

This branch (`critical-fixes-modernization`) addresses the issues that would make a reviewer wince at the original codebase. Each change landed as its own commit:

- **Refactored the payment flow to use a single SQL transaction.** Transfers now run inside one `SqlConnection` + `SqlTransaction`. The payer row is locked with `WITH (UPDLOCK, ROWLOCK)`; balance and insufficient-funds checks happen inside the transaction, so UI validation isn't load-bearing. Either both balance changes and both transaction rows land, or the transaction rolls back and nothing does. Previously the flow was four sequential calls with an `async void` fire-and-forget for the recipient credit — failures left orphan rows or debited money that was never credited.
- **Replaced `double` with `decimal` for all monetary values.** `Balance`, `Amount`, `BalanceAfterTransaction`, and exchange-rate `Value` are `decimal` end-to-end. Repository readers stopped round-tripping through `decimal.ToDouble`, and `decimal` parameters are now bound via explicit `SqlParameter` with `Precision = 18, Scale = 2` instead of `AddWithValue`. The DB schema (`DECIMAL(18,2)`) was already correct; the application tier had been quietly laundering it through binary floating point.
- **Hashed user passwords with BCrypt** (work-factor 11). Plaintext storage and string-equality comparison are gone; `UserRepository.IsValidUser` pulls the stored hash and calls `BCrypt.Verify`. Seed data ships pre-hashed, and `EBanking.TestConsole` can rehash any leftover plaintext rows in a stale dev database.
- **Moved the database connection string into a single configuration source** (`EBanking.UI/App.config`). Removed five hardcoded copies across the data layer, including a dead `DatabaseAccess.cs` constant that pointed at a different database name.
- **Cleaned up WPF lifecycle bugs.** Removed the last `async void` handler in the UI project (`async void Payment()` was a stranded remnant after the payment-flow refactor). The three `view.Closing` subscriptions (login → account view, account → payment view, account → currency exchange) were anonymous lambdas that captured `this` and were never detached, pinning every prior ViewModel for the app's lifetime; they're now named local-function handlers that `-=` themselves on first invocation.

## What's intentionally out of scope

This pass kept the architecture as-is: still MvvmLight `SimpleIoc`, still raw ADO.NET, still .NET 6, no automated test project, no `INavigationService`, no logging framework. Those are bigger swings worth considering separately — they aren't here.
