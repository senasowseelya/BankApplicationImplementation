CREATE TABLE [dbo].[bankuser] (
    [id]            VARCHAR (20) NOT NULL,
    [name]          VARCHAR (20) NOT NULL,
    [username]      VARCHAR (20) NULL,
    [password]      VARCHAR (20) NOT NULL,
    [age]           INT          NOT NULL,
    [dob]           DATE         NOT NULL,
    [contactNumber] VARCHAR (10) NOT NULL,
    [aadharNumber]  BIGINT       NOT NULL,
    [nationality]   VARCHAR (10) NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    UNIQUE NONCLUSTERED ([username] ASC)
);

