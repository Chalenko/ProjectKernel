CREATE TABLE [dbo].[Logs] (
    [id]          INT            IDENTITY (1, 1) NOT NULL,
    [user]        INT            NULL,
    [workstation] NVARCHAR (50)  NULL,
    [date]        DATETIME       NOT NULL,
    [level]       NVARCHAR (50)  NOT NULL,
    [message]     NVARCHAR (MAX) NULL,
    [exception]   NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Logs_User] FOREIGN KEY ([user]) REFERENCES [dbo].[Users] ([id])
);

