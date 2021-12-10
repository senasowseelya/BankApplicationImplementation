CREATE TABLE [dbo].[serviceCharge] (
    [id]     VARCHAR (10) NOT NULL,
    [name]   VARCHAR (25) NOT NULL,
    [value]  DECIMAL (18) NOT NULL,
    [bankId] VARCHAR (15) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    FOREIGN KEY ([bankId]) REFERENCES [dbo].[bank] ([id]),
    UNIQUE NONCLUSTERED ([name] ASC)
);

