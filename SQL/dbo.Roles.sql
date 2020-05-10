USE [DBTETS]
GO

/****** Объект: Table [dbo].[Role] Дата скрипта: 19.07.2016 10:50:35 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role] (
    [id]          UNIQUEIDENTIFIER  NOT NULL,
    [name]        NVARCHAR (50)  	NOT NULL,
    [description] NVARCHAR (100) 	NULL,
    [creator_id]  UNIQUEIDENTIFIER  NOT NULL,
    [create_date] DATETIME       	NOT NULL,
    [modifier_id] UNIQUEIDENTIFIER  NULL,
    [modify_date] DATETIME       	NULL,
	PRIMARY KEY CLUSTERED ([id] ASC),
	CONSTRAINT [CK_Roles_name] UNIQUE NONCLUSTERED ([name] ASC),
    CONSTRAINT [FK_Roles_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Roles_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id])
);

DECLARE @creator_id UNIQUEIDENTIFIER
SELECT @creator_id = id FROM [dbo].[USERS] WHERE login LIKE 'Admin'

INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Admin' ,'Administrator of system' ,@creator_id ,GETDATE());
/*
INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Spy' ,	'Foreign spy' ,@creator_id ,GETDATE());
INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'DocumentWriter' ,NULL ,@creator_id ,GETDATE());
INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'UIT' ,'UIT employees' ,@creator_id ,GETDATE());
INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Friends' ,'My friends' ,@creator_id ,GETDATE());
INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'NNSU' ,'NNSU students and employees' ,@creator_id ,GETDATE());
*/
