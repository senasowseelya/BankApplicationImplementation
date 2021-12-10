CREATE TABLE [dbo].[account] (
    [accountId]     VARCHAR (20)    NOT NULL,
    [bankId]        VARCHAR (15)    NULL,
    [accountNumber] VARCHAR (20)    NOT NULL,
    [balance]       DECIMAL (10, 2) NOT NULL,
    [status]        VARCHAR (10)    NOT NULL,
    [dateOfIssue]   DATETIME        NOT NULL,
    [userId]        VARCHAR (20)    NULL,
    PRIMARY KEY CLUSTERED ([accountId] ASC),
    FOREIGN KEY ([bankId]) REFERENCES [dbo].[bank] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[bankuser] ([id])
);

