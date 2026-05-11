USE [master]
GO

-- Drop and recreate database
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'EBankingSystem')
BEGIN
    ALTER DATABASE [EBankingSystem] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [EBankingSystem];
END
GO

CREATE DATABASE [EBankingSystem]
ON PRIMARY
(
    NAME = N'EBankingSystem',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EBankingSystem.mdf',
    SIZE = 8192KB, MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB
)
LOG ON
(
    NAME = N'EBankingSystem_log',
    FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\EBankingSystem_log.ldf',
    SIZE = 8192KB, MAXSIZE = 2048GB, FILEGROWTH = 65536KB
)
WITH CATALOG_COLLATION = DATABASE_DEFAULT;
GO

ALTER DATABASE [EBankingSystem] SET RECOVERY SIMPLE;
GO

USE [EBankingSystem]
GO

-- ============================================================
-- [User]
-- Bracketed because USER is a reserved word in SQL Server.
-- ============================================================
CREATE TABLE [dbo].[User]
(
    [userId]       INT           IDENTITY(1,1) NOT NULL,
    [firstName]    NVARCHAR(100) NOT NULL,
    [lastName]     NVARCHAR(100) NOT NULL,
    [dateOfBirth]  DATE          NOT NULL,
    [idCardNumber] NVARCHAR(50)  NOT NULL,
    [phone]        NVARCHAR(20)  NULL,
    [email]        NVARCHAR(255) NOT NULL,
    [userPin]      NVARCHAR(10)  NOT NULL,
    [password]     NVARCHAR(255) NOT NULL,

    CONSTRAINT [PK_User]          PRIMARY KEY CLUSTERED ([userId] ASC),
    CONSTRAINT [UQ_User_Email]    UNIQUE ([email]),
    CONSTRAINT [UQ_User_IdCard]   UNIQUE ([idCardNumber])
);
GO

-- ============================================================
-- Account
-- ============================================================
CREATE TABLE [dbo].[Account]
(
    [accountNumber] NVARCHAR(50)  NOT NULL,
    [userId]        INT           NOT NULL,
    [balance]       DECIMAL(18,2) NOT NULL CONSTRAINT [DF_Account_Balance] DEFAULT 0,
    [currency]      NVARCHAR(10)  NOT NULL,   -- 'RSD' or 'EUR'
    [type]          NVARCHAR(50)  NOT NULL,   -- e.g. 'Checking', 'Savings'
    [dateCreated]   DATETIME      NOT NULL CONSTRAINT [DF_Account_DateCreated] DEFAULT GETDATE(),

    CONSTRAINT [PK_Account]      PRIMARY KEY CLUSTERED ([accountNumber] ASC),
    CONSTRAINT [FK_Account_User] FOREIGN KEY ([userId]) REFERENCES [dbo].[User] ([userId])
);
GO

-- ============================================================
-- Card
-- ============================================================
CREATE TABLE [dbo].[Card]
(
    [cardNumber]     NVARCHAR(50) NOT NULL,
    [accountNumber]  NVARCHAR(50) NOT NULL,
    [pinCode]        NVARCHAR(10) NOT NULL,
    [expirationDate] DATETIME     NOT NULL,
    [dateCreated]    DATETIME     NOT NULL CONSTRAINT [DF_Card_DateCreated] DEFAULT GETDATE(),
    [cvc]            NVARCHAR(5)  NOT NULL,
    [isValid]        BIT          NOT NULL CONSTRAINT [DF_Card_IsValid] DEFAULT 1,

    CONSTRAINT [PK_Card]         PRIMARY KEY CLUSTERED ([cardNumber] ASC),
    CONSTRAINT [FK_Card_Account] FOREIGN KEY ([accountNumber]) REFERENCES [dbo].[Account] ([accountNumber])
);
GO

-- ============================================================
-- [Transaction]
-- Bracketed because TRANSACTION is a reserved word in SQL Server.
-- ============================================================
CREATE TABLE [dbo].[Transaction]
(
    [transactionId]               INT           IDENTITY(1,1) NOT NULL,
    [accountNumber]               NVARCHAR(50)  NOT NULL,
    [cardNumber]                  NVARCHAR(50)  NULL,
    [amount]                      DECIMAL(18,2) NOT NULL,
    [balanceAfterTransaction]     DECIMAL(18,2) NOT NULL,
    [date]                        DATETIME      NOT NULL CONSTRAINT [DF_Transaction_Date] DEFAULT GETDATE(),
    [secondaryPartyName]          NVARCHAR(200) NULL,
    [secondaryPartyAccountNumber] NVARCHAR(50)  NULL,

    CONSTRAINT [PK_Transaction]         PRIMARY KEY CLUSTERED ([transactionId] ASC),
    CONSTRAINT [FK_Transaction_Account] FOREIGN KEY ([accountNumber]) REFERENCES [dbo].[Account] ([accountNumber])
);
GO

-- ============================================================
-- CurrencyExchange
-- Stores the multiplier used to convert from one currency to
-- another.  The 'currency' column identifies the target
-- currency (matches the Currencies enum: 'EUR', 'RSD').
-- ============================================================
CREATE TABLE [dbo].[CurrencyExchange]
(
    [currency] NVARCHAR(10)  NOT NULL,
    [value]    DECIMAL(18,4) NOT NULL,

    CONSTRAINT [PK_CurrencyExchange] PRIMARY KEY CLUSTERED ([currency] ASC)
);
GO
