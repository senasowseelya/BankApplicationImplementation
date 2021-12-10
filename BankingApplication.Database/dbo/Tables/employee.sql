CREATE TABLE [dbo].[employee] (
    [employeeId] VARCHAR (20) NOT NULL,
    [bankId]     VARCHAR (15) NOT NULL,
    [userId]     VARCHAR (20) NOT NULL,
    PRIMARY KEY CLUSTERED ([employeeId] ASC),
    FOREIGN KEY ([bankId]) REFERENCES [dbo].[bank] ([id]),
    FOREIGN KEY ([userId]) REFERENCES [dbo].[bankuser] ([id])
);

