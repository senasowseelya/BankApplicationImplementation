CREATE TABLE [dbo].[transactionType] (
    [id]   VARCHAR (10) NOT NULL,
    [name] VARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([name] ASC)
);

