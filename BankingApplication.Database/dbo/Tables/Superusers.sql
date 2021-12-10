CREATE TABLE [dbo].[Superusers] (
    [Sid]      VARCHAR (20) NOT NULL,
    [UserName] VARCHAR (20) NULL,
    [Password] VARCHAR (10) NULL,
    PRIMARY KEY CLUSTERED ([Sid] ASC),
    UNIQUE NONCLUSTERED ([UserName] ASC)
);

