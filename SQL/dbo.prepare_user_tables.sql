USE [DBTEST]
GO

/****** Object:  StoredProcedure [dbo].[prepare_user_tables]    Script Date: 08/10/2017 10:51:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[prepare_user_tables]
AS
	ALTER TABLE [dbo].[UserRole]	DROP CONSTRAINT [FK_UserRole_Role];
	ALTER TABLE [dbo].[UserRole]	DROP CONSTRAINT [FK_UserRole_User];
	ALTER TABLE [dbo].[UserRole]	DROP CONSTRAINT [FK_UserRole_User_Creator];
	ALTER TABLE [dbo].[Role]		DROP CONSTRAINT [FK_Role_ToUser_Creator];
	ALTER TABLE [dbo].[Role]		DROP CONSTRAINT [FK_Role_ToUser_Modifier];
	ALTER TABLE [dbo].[User]		DROP CONSTRAINT [FK_User_ToUser_Creator];
	ALTER TABLE [dbo].[User]		DROP CONSTRAINT [FK_User_ToUser_Modifier];

	--TRUNCATE TABLE [dbo].[Logs];
	TRUNCATE TABLE [dbo].[UserRole];
	TRUNCATE TABLE [dbo].[Role];
	TRUNCATE TABLE [dbo].[User];

	DECLARE @first_id NVARCHAR(50)
	SET @first_id = newid()

	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (@first_id	,'Chalenko'		,'Pavel'	,'Vladislavovich'	,'p.v.chalenko'		,'jE0787BkT84+kjZe/QsTib70o3OXjAbo0+0qqlu5WCc='		,'2t4xV8JoDsLg61Q2U5zcPDXlOvNqqPWxVAJEldfzy0uffcWh+06JooUzZcKv1NFVGP7l9E36JqoKxK8ow0auxw=='		, 0, 'ORPOiTP'		,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Ильичев'		,N'Андрей'	,N'Сергеевич'		,'a.s.ilichev'		,'i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo='		,'K3yPrAI/5bNd1y1ldbxiY1dtEPDklh6yBVpiaX/XwWrqY3xPjYsVT+T1E3uWEBTJ+0SGq0xBg6IHMWvIAFADIg=='		, 0, N'ОРПОиТП'		,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Азар'		,N'Джон'	,N''				,'j.azar'			,''													,'757602046'																					, 0, N'ННГУ'		,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Крылова'		,N'Любовь'	,N'Львовна'			,'l.l.krylova'		,''													,'757602046'																					, 0, N'ННГУ'		,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Леванова'	,N'Татьяна' ,N'Александровна'	,'t.a.levanova'		,''													,'757602046'																					, 0, N'ННГУ'		,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Сергиенко'	,N'Сергей'	,N'Эдуардович'		,'s.e.sergienko'	,''													,'757602046'																					, 0, N'Freemake'	,@first_id, GETDATE()); 
	INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid()		,N'Николаев'	,N'Артем'	,N'Олегович'		,'a.o.nikolaev'		,'fu/6RvHylF11q50iE6mgtMQ3BjzyKxW6z4H7QxhC7hQ='		,'UlU/Y3mr+afBCBYlOrs+8ackQYuOafPaJ7WEBYZRHEN6VC/an+gVTr8eEunsr0zdEUx3a9ZARiZTHmDkCRY90Q=='		, 0, N'HSE'			,@first_id, GETDATE()); 

	DECLARE @creator_id UNIQUEIDENTIFIER
	SELECT @creator_id = id FROM [dbo].[User] WHERE login LIKE 'p.v.chalenko'

	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Admin'				,'Administrator of system'		,@creator_id ,GETDATE());
	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Spy'				,'Foreign spy'					,@creator_id ,GETDATE());
	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'DocumentWriter'		,NULL							,@creator_id ,GETDATE());
	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'UIT'				,'UIT employees'				,@creator_id ,GETDATE());
	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'Friends'			,'My friends'					,@creator_id ,GETDATE());
	INSERT INTO [dbo].[Role] (id ,name ,description ,creator_id ,create_date) VALUES (newid() ,'NNSU'				,'NNSU students and employees'	,@creator_id ,GETDATE());

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

	ALTER TABLE [dbo].[UserRole]	WITH NOCHECK ADD CONSTRAINT [FK_UserRole_Role]			FOREIGN KEY ([role_id]) REFERENCES [dbo].[Role] ([id]);
	ALTER TABLE [dbo].[UserRole]	WITH CHECK CHECK CONSTRAINT [FK_UserRole_Role];
	ALTER TABLE [dbo].[UserRole]	WITH NOCHECK ADD CONSTRAINT [FK_UserRole_User]			FOREIGN KEY ([user_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[UserRole]	WITH CHECK CHECK CONSTRAINT [FK_UserRole_User];
	ALTER TABLE [dbo].[UserRole]	WITH NOCHECK ADD CONSTRAINT [FK_UserRole_User_Creator]	FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[UserRole]	WITH CHECK CHECK CONSTRAINT [FK_UserRole_User_Creator];
	ALTER TABLE [dbo].[Role]		WITH NOCHECK ADD CONSTRAINT [FK_Role_ToUser_Creator]	FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[Role]		WITH CHECK CHECK CONSTRAINT [FK_Role_ToUser_Creator];
	ALTER TABLE [dbo].[Role]		WITH NOCHECK ADD CONSTRAINT [FK_Role_ToUser_Modifier]	FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[Role]		WITH CHECK CHECK CONSTRAINT [FK_Role_ToUser_Modifier];
	ALTER TABLE [dbo].[User]		WITH NOCHECK ADD CONSTRAINT [FK_User_ToUser_Creator]	FOREIGN KEY ([creator_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[User]		WITH CHECK CHECK CONSTRAINT [FK_User_ToUser_Creator];
	ALTER TABLE [dbo].[User]		WITH NOCHECK ADD CONSTRAINT [FK_User_ToUser_Modifier]	FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[User] ([id]);
	ALTER TABLE [dbo].[User]		WITH CHECK CHECK CONSTRAINT [FK_User_ToUser_Modifier];
RETURN 0
GO

