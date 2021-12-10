CREATE TABLE [dbo].[bank] (
    [id]      VARCHAR (15) NOT NULL,
    [name]    VARCHAR (20) NOT NULL,
    [branch]  VARCHAR (10) NOT NULL,
    [ifsc]    VARCHAR (12) NOT NULL,
    [balance] DECIMAL (18) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC)
);

