USE [master]
GO

IF DB_ID('DB_NAME') IS NULL
  EXEC('CREATE DATABASE DB_NAME');
GO

USE [DB_NAME]

IF OBJECT_ID('User') IS NULL
	CREATE TABLE [dbo].[User] (
		[id]              UNIQUEIDENTIFIER NOT NULL,
		[surname]         NVARCHAR (50)    NOT NULL,
		[first_name]      NVARCHAR (50)    NOT NULL,
		[patronymic_name] NVARCHAR (50)    NULL,
		[login]           NVARCHAR (50)    NOT NULL,
		[salt]            NVARCHAR (50)    NOT NULL,
		[password]        NVARCHAR (100)   NOT NULL,
		[change_password] BIT              NOT NULL,
		[department]      NVARCHAR (50)    NOT NULL,
		[creator_id]      UNIQUEIDENTIFIER NOT NULL,
		[create_date]     DATETIME         NOT NULL,
		[modifier_id]     UNIQUEIDENTIFIER NULL,
		[modify_date]     DATETIME         NULL,
		PRIMARY KEY CLUSTERED ([id] ASC),
		CONSTRAINT [CK_User_login] UNIQUE NONCLUSTERED ([login] ASC),
		CONSTRAINT [FK_User_ToUser_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id]),
		CONSTRAINT [FK_User_ToUser_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[User] ([id])
	);
GO

IF OBJECT_ID('Role') IS NULL
	CREATE TABLE [dbo].[Role] (
		[id]          UNIQUEIDENTIFIER NOT NULL,
		[name]        NVARCHAR (50)    NOT NULL,
		[description] NVARCHAR (100)   NULL,
		[creator_id]  UNIQUEIDENTIFIER NOT NULL,
		[create_date] DATETIME         NOT NULL,
		[modifier_id] UNIQUEIDENTIFIER NULL,
		[modify_date] DATETIME         NULL,
		PRIMARY KEY CLUSTERED ([id] ASC),
		CONSTRAINT [CK_Role_name] UNIQUE NONCLUSTERED ([name] ASC),
		CONSTRAINT [FK_Role_ToUser_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id]),
		CONSTRAINT [FK_Role_ToUser_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[User] ([id])
	);
GO

IF OBJECT_ID('UserRole') IS NULL
	CREATE TABLE [dbo].[UserRole] (
		[user_id]     UNIQUEIDENTIFIER NOT NULL,
		[role_id]     UNIQUEIDENTIFIER NOT NULL,
		[creator_id]  UNIQUEIDENTIFIER NOT NULL,
		[create_date] DATETIME         NOT NULL,
		CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([user_id] ASC, [role_id] ASC),
		CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[Role] ([id]),
		CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]),
		CONSTRAINT [FK_UserRole_User_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id])
	);
GO

IF OBJECT_ID('Log') IS NULL
	CREATE TABLE [dbo].[Log] (
		[id]          INT            IDENTITY (1, 1) NOT NULL,
		[user_type]   NVARCHAR (50)  NOT NULL,
		[user_login]  NVARCHAR (50)  NOT NULL,
		[workstation] NVARCHAR (50)  NULL,
		[date]        DATETIME       NOT NULL,
		[level]       NVARCHAR (50)  NOT NULL,
		[message]     NVARCHAR (MAX) NULL,
		[exception]   NVARCHAR (MAX) NULL,
		PRIMARY KEY CLUSTERED ([id] ASC)
	);
GO

IF OBJECT_ID('Activity') IS NULL
	CREATE TABLE [dbo].[Activity] (
		[user_login]				NVARCHAR (50)		NULL,
		[state]						NVARCHAR (50)		NOT NULL,
		[last_login_datetime]		DATETIME			NULL,
		[last_login_workstation]	NVARCHAR (50)		NULL,
		[last_logout_datetime]		DATETIME			NULL,
		CONSTRAINT [CK_Activity_user_login] UNIQUE NONCLUSTERED ([user_login] ASC)
	);
GO


IF (SELECT id from [dbo].[User] WHERE login = 'Admin') IS NULL
	DECLARE @id UNIQUEIDENTIFIER;
	DECLARE @creator_id UNIQUEIDENTIFIER ;
	SET @id = newid();
	SET @creator_id = @id;
	
	INSERT INTO [dbo].[User] (
		[id],
		[surname],
		[first_name],
		[patronymic_name],
		[login],
		[salt],
		[password],
		[change_password]
		[department],
		[creator_id],
		[create_date],
		[modifier_id],
		[modify_date]
	) VALUES (
		@id,
		'Admin',
		'Admin',
		NULL,
		'Admin',
		'dwX4SPrm0hPaDAMVxhIwmftbtETfgJi3fun1k6pX6C4=',
		'HhwQxC1oQ8PyGOgXqtTRF8Wt+J3n39IKbjbmnlFz8442SluGQ7ZSbYyGPG+uyBy/+RfNXjdhkc3JRzsekDbC/Q==',
		0,
		'UIT',
		@creator_id,
		getdate(),
		NULL,
		NULL
	);
GO

IF (SELECT id from [dbo].[Role] WHERE name = 'Admin') IS NULL
	DECLARE @role_id UNIQUEIDENTIFIER;
	SET @role_id = newid();
	DECLARE @creator_id UNIQUEIDENTIFIER;
	SET @creator_id = (SELECT TOP 1 id FROM [dbo].[User] WHERE login = 'Admin');

	INSERT INTO [dbo].[Role] (
		[id],
		[name],
		[description],
		[creator_id],
		[create_date],
		[modifier_id],
		[modify_date]
	) VALUES (
		@role_id,
		'Admin',
		'Administrator of system',
		@creator_id,
		getdate(),
		NULL,
		NULL
	);
GO

DECLARE @user_id UNIQUEIDENTIFIER;
SET @user_id = (SELECT TOP 1 id from [dbo].[User] WHERE login = 'Admin')
DECLARE @role_id UNIQUEIDENTIFIER;
SET @role_id = (SELECT TOP 1 id from [dbo].[Role] WHERE name = 'Admin')
IF (SELECT user_id from [dbo].[UserRole] WHERE user_id = @user_id AND role_id = @role_id) IS NULL
	DECLARE @creator_id UNIQUEIDENTIFIER;
	SET @creator_id = (SELECT TOP 1 id FROM [dbo].[User] WHERE login = 'Admin');

	INSERT INTO [dbo].[UserRole] (
		[user_id],
		[role_id],
		[creator_id],
		[create_date]
	) VALUES (
		@user_id,
		@role_id,
		@creator_id,
		getdate()
	);
GO
