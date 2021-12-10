CREATE TABLE [dbo].[currency] (
    [id]           VARCHAR (20)    NOT NULL,
    [name]         VARCHAR (10)    NULL,
    [exchangeRate] DECIMAL (10, 2) NOT NULL,
    [bankid]       VARCHAR (15)    NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([bankid]) REFERENCES [dbo].[bank] ([id]),
    UNIQUE NONCLUSTERED ([name] ASC)
);

