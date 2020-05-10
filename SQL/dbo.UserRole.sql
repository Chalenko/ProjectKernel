USE [DBTEST_NEW]
GO

/****** Объект: Table [dbo].[UserRole] Дата скрипта: 19.07.2016 11:05:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserRole] (
    [user_id]     UNIQUEIDENTIFIER 	NOT NULL,
    [role_id]     UNIQUEIDENTIFIER 	NOT NULL,
    [creator_id]  UNIQUEIDENTIFIER 	NOT NULL,
    [create_date] DATETIME 			NOT NULL,
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED ([user_id] ASC, [role_id] ASC),
    CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[Roles] ([id]),
    CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_UserRole_User_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id])
);

INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM Users WHERE login LIKE 'Admin') 	,(SELECT id FROM Roles WHERE name LIKE 'Admin') 			,(SELECT id FROM Users WHERE login LIKE 'Admin') ,GETDATE());
/*

INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'Admin') 				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'a.s.ilichev') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'Admin')  			,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'a.s.ilichev') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'UIT')  				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'j.azar') 			,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'Spy')  				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'a.s.ilichev')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'l.l.krylova') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'DocumentWriter')  	,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 'l.l.krylova') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'NNSU')  				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 't.a.levanova') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'DocumentWriter')  	,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 't.a.levanova') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'Friends')  			,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 't.a.levanova') 		,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'NNSU')  				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 's.e.sergienko') 	,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'Friends')  			,(SELECT id FROM [dbo].[User] WHERE login LIKE 'a.s.ilichev')	,GETDATE());
INSERT INTO [dbo].[UserRole] ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES ((SELECT id FROM [dbo].[User] WHERE login LIKE 's.e.sergienko') 	,(SELECT id FROM [dbo].[Role] WHERE name LIKE 'NNSU')  				,(SELECT id FROM [dbo].[User] WHERE login LIKE 'a.s.ilichev')	,GETDATE());
*/

