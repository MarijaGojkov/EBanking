USE [EBankingSystem]
GO

-- ============================================================
-- CurrencyExchange
-- 'currency' = target currency code (matches Currencies enum).
-- 'value'    = multiplier applied to the source amount.
--              EUR row: 1 EUR = 117.50 RSD
--              RSD row: 1 RSD = 0.0085 EUR
-- ============================================================
INSERT INTO [dbo].[CurrencyExchange] ([currency], [value]) VALUES
    ('EUR', 0.0085),
    ('RSD', 117.50);
GO

-- ============================================================
-- Users
-- Passwords are stored as BCrypt hashes (work-factor 11). Demo
-- credentials below are kept readable in this comment so a
-- reviewer can log in; the column itself never contains plaintext.
--   ana.petrovic@email.com    / password123
--   marko.jovanovic@email.com / password456
--   jelena.nikolic@email.com  / password789
-- Hashes were generated with EBanking.TestConsole:
--   dotnet run --project EBanking.TestConsole -- hash <password>
-- ============================================================
SET IDENTITY_INSERT [dbo].[User] ON;

INSERT INTO [dbo].[User]
    ([userId], [firstName], [lastName], [dateOfBirth], [idCardNumber], [phone], [email], [userPin], [password])
VALUES
    (1, 'Ana',    'Petrovic',  '1990-03-15', 'ID100001', '+381641234567', 'ana.petrovic@email.com',    '1234', '$2a$11$BrfMZu862HHa6HDo.RdwOuXJFU30i8vfs4np5ImZ48p6CexOcw7KC'),
    (2, 'Marko',  'Jovanovic', '1985-07-22', 'ID100002', '+381651234567', 'marko.jovanovic@email.com', '5678', '$2a$11$Jqsw0dv0st46HNJBsK6hnOuQnpjM5x.pF3LZnr.PUQ/d9Y2cBbdva'),
    (3, 'Jelena', 'Nikolic',   '1995-11-08', 'ID100003', '+381661234567', 'jelena.nikolic@email.com',  '9012', '$2a$11$E/eJKQnGgtfEs5xLfMsQRuf0RyCJ1EIsQMHF70Szv838vwN/YnE2e');

SET IDENTITY_INSERT [dbo].[User] OFF;
GO

-- ============================================================
-- Accounts
-- currency values must be 'RSD' or 'EUR' (Currencies enum).
-- type values are free-form strings displayed in the UI.
-- ============================================================
INSERT INTO [dbo].[Account]
    ([accountNumber], [userId], [balance], [currency], [type], [dateCreated])
VALUES
    -- Ana: one RSD checking account, one EUR savings account
    ('111-0000001-11', 1, 148000.00, 'RSD', 'Checking', '2023-01-10 09:00:00'),
    ('111-0000002-22', 1,   2000.00, 'EUR', 'Savings',  '2023-01-10 09:05:00'),

    -- Marko: one RSD checking account
    ('222-0000001-33', 2,  93000.00, 'RSD', 'Checking', '2023-03-05 10:00:00'),

    -- Jelena: one EUR checking account
    ('333-0000001-44', 3,   1800.00, 'EUR', 'Checking', '2024-06-15 11:00:00');
GO

-- ============================================================
-- Cards
-- ============================================================
INSERT INTO [dbo].[Card]
    ([cardNumber], [accountNumber], [pinCode], [expirationDate], [dateCreated], [cvc], [isValid])
VALUES
    -- Ana's card linked to her RSD account
    ('4532015112830366', '111-0000001-11', '1234', '2027-12-31 00:00:00', '2023-01-10 09:10:00', '123', 1),

    -- Marko's card linked to his RSD account
    ('4916338506082832', '222-0000001-33', '5678', '2026-06-30 00:00:00', '2023-03-05 10:10:00', '456', 1);
GO

-- ============================================================
-- Transactions
--
-- Logic used to derive balances:
--   Ana RSD  : started at 155000 → sent 7000 → 148000  (current)
--   Marko RSD: started at 86000  → received 7000 → 93000 (current)
--   Ana EUR  : started at 2200   → sent 200 → 2000       (current)
--   Jelena EUR: started at 1600  → received 200 → 1800   (current)
-- ============================================================

-- Transfer 1: Ana (RSD) → Marko (RSD), 7000 RSD
INSERT INTO [dbo].[Transaction]
    ([accountNumber], [cardNumber], [amount], [balanceAfterTransaction], [date], [secondaryPartyName], [secondaryPartyAccountNumber])
VALUES
    -- debit Ana's RSD account
    ('111-0000001-11', NULL, 7000.00, 148000.00, '2025-02-10 14:22:00', 'Marko Jovanovic', '222-0000001-33'),
    -- credit Marko's RSD account
    ('222-0000001-33', NULL, 7000.00,  93000.00, '2025-02-10 14:22:00', 'Ana Petrovic',    '111-0000001-11');

-- Transfer 2: Ana (EUR) → Jelena (EUR), 200 EUR
INSERT INTO [dbo].[Transaction]
    ([accountNumber], [cardNumber], [amount], [balanceAfterTransaction], [date], [secondaryPartyName], [secondaryPartyAccountNumber])
VALUES
    -- debit Ana's EUR account
    ('111-0000002-22', NULL, 200.00, 2000.00, '2025-03-22 10:05:00', 'Jelena Nikolic',  '333-0000001-44'),
    -- credit Jelena's EUR account
    ('333-0000001-44', NULL, 200.00, 1800.00, '2025-03-22 10:05:00', 'Ana Petrovic',    '111-0000002-22');

-- Card payment: Ana pays utility bill using her card
INSERT INTO [dbo].[Transaction]
    ([accountNumber], [cardNumber], [amount], [balanceAfterTransaction], [date], [secondaryPartyName], [secondaryPartyAccountNumber])
VALUES
    -- Note: this transaction happens BEFORE the transfer above in real time;
    -- for seeding we just need representative data, not strict chronological order.
    ('111-0000001-11', '4532015112830366', 3200.00, 155000.00, '2025-01-05 08:30:00', 'EPS Distribucija', NULL);
GO
