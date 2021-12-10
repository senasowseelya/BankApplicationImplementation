CREATE TABLE [dbo].[transactions] (
    [transid]           VARCHAR (50)    NOT NULL,
    [senderaccountId]   VARCHAR (20)    NOT NULL,
    [receiveraccountId] VARCHAR (20)    NOT NULL,
    [transactionOn]     DATETIME        NOT NULL,
    [transactionAmount] DECIMAL (10, 2) NOT NULL,
    [type]              VARCHAR (10)    NULL,
    [currency]          VARCHAR (20)    NULL,
    PRIMARY KEY CLUSTERED ([transid] ASC),
    FOREIGN KEY ([currency]) REFERENCES [dbo].[currency] ([id]),
    FOREIGN KEY ([receiveraccountId]) REFERENCES [dbo].[account] ([accountId]),
    FOREIGN KEY ([senderaccountId]) REFERENCES [dbo].[account] ([accountId]),
    FOREIGN KEY ([type]) REFERENCES [dbo].[transactionType] ([id])
);

