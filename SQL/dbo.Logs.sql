USE [DBTEST_NEW]
GO

/****** Объект: Table [dbo].[Logs] Дата скрипта: 19.07.2016 11:03:30 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Logs] (
    [id]          UNIQUEIDENTIFIER 	NOT NULL,
    [user]        UNIQUEIDENTIFIER	NULL,
    [workstation] NVARCHAR (50)  	NULL,
    [date]        DATETIME       	NOT NULL,
    [level]       NVARCHAR (50)  	NOT NULL,
    [message]     NVARCHAR (MAX) 	NULL,
    [exception]   NVARCHAR (MAX) 	NULL,
    PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_Logs_User] FOREIGN KEY ([user]) REFERENCES [dbo].[Users] ([id])
);
