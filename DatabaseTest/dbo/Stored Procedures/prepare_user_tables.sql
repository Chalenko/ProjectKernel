CREATE PROCEDURE [dbo].[prepare_user_tables]
AS

	ALTER TABLE [dbo].[Logs] DROP CONSTRAINT [FK_Logs_User];
	ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role];
	ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User];
	ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User_Creator];
	ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_Roles_ToUsers_Creator];
	ALTER TABLE [dbo].[Roles] DROP CONSTRAINT [FK_Roles_ToUsers_Modifier];
	ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_ToUsers_Creator];
	ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_ToUsers_Modifier];

	--TRUNCATE TABLE [dbo].[Logs];
	TRUNCATE TABLE [dbo].[UserRole];
	TRUNCATE TABLE [dbo].[Roles];
	TRUNCATE TABLE [dbo].[Users];
	
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES ('Chalenko' ,'Pavel' ,'Vladislavovich' ,'p.v.chalenko' ,'jE0787BkT84+kjZe/QsTib70o3OXjAbo0+0qqlu5WCc=' ,'2t4xV8JoDsLg61Q2U5zcPDXlOvNqqPWxVAJEldfzy0uffcWh+06JooUzZcKv1NFVGP7l9E36JqoKxK8ow0auxw==', 'ORPOiTP', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Ильичев' ,N'Андрей' ,N'Сергеевич' ,'a.s.ilichev' ,'i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo=' ,'K3yPrAI/5bNd1y1ldbxiY1dtEPDklh6yBVpiaX/XwWrqY3xPjYsVT+T1E3uWEBTJ+0SGq0xBg6IHMWvIAFADIg==', N'ОРПОиТП', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Азар' ,N'Джон' ,N'' ,'j.azar' ,'' ,'757602046', N'ННГУ', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Крылова' ,N'Любовь' ,N'Львовна' ,'l.l.krylova' ,'' ,'757602046', N'ННГУ', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Леванова' ,N'Татьяна' ,N'Александровна' ,'t.a.levanova' ,'' ,'757602046', N'ННГУ', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Сергиенко' ,N'Сергей' ,N'Эдуардович' ,'s.e.sergienko' ,'' ,'757602046', N'Freemake', 1, GETDATE()); 
	INSERT INTO [dbo].[Users] ([surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [department], [creator_id], [create_date]) VALUES (N'Николаев' ,N'Артем' ,N'Олегович' ,'a.o.nikolaev' ,'fu/6RvHylF11q50iE6mgtMQ3BjzyKxW6z4H7QxhC7hQ=' ,'UlU/Y3mr+afBCBYlOrs+8ackQYuOafPaJ7WEBYZRHEN6VC/an+gVTr8eEunsr0zdEUx3a9ZARiZTHmDkCRY90Q==', N'HSE', 1, GETDATE()); 
	
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('Admin' ,'Administrator of system' ,1 ,GETDATE());
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('Spy' ,	'Foreign spy' ,1 ,GETDATE());
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('DocumentWriter' ,NULL ,1 ,GETDATE());
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('UIT' ,'UIT employees' ,1 ,GETDATE());
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('Friends' ,'My friends' ,1 ,GETDATE());
	INSERT INTO [dbo].[Roles] (name ,description ,creator_id ,create_date) VALUES ('NNSU' ,'NNSU students and employees' ,1 ,GETDATE());

	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (1 ,1, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (2 ,1, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (2 ,4, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (3 ,2, 2, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (4 ,3, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (4 ,6, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (5 ,3, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (5 ,5, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (5 ,6, 1, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (6 ,5, 2, GETDATE());
	INSERT INTO UserRole ([user_id] ,[role_id] ,[creator_id] ,[create_date]) VALUES (6 ,6, 2, GETDATE());

	ALTER TABLE [dbo].[Logs] WITH NOCHECK ADD CONSTRAINT [FK_Logs_User] FOREIGN KEY ([user]) REFERENCES [dbo].[Users] ([id])
	ALTER TABLE [dbo].[Logs] WITH CHECK CHECK CONSTRAINT [FK_Logs_User];
	ALTER TABLE [dbo].[UserRole] WITH NOCHECK ADD CONSTRAINT [FK_UserRole_Role] FOREIGN KEY ([role_id]) REFERENCES [dbo].[Roles] ([id]);
	ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_Role];
	ALTER TABLE [dbo].[UserRole] WITH NOCHECK ADD CONSTRAINT [FK_UserRole_User] FOREIGN KEY ([user_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_User];
	ALTER TABLE [dbo].[UserRole] WITH NOCHECK ADD CONSTRAINT [FK_UserRole_User_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[UserRole] WITH CHECK CHECK CONSTRAINT [FK_UserRole_User_Creator];
	ALTER TABLE [dbo].[Roles] WITH NOCHECK ADD CONSTRAINT [FK_Roles_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_ToUsers_Creator];
	ALTER TABLE [dbo].[Roles] WITH NOCHECK ADD CONSTRAINT [FK_Roles_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[Roles] WITH CHECK CHECK CONSTRAINT [FK_Roles_ToUsers_Modifier];
	ALTER TABLE [dbo].[Users] WITH NOCHECK ADD CONSTRAINT [FK_Users_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_ToUsers_Creator];
	ALTER TABLE [dbo].[Users] WITH NOCHECK ADD CONSTRAINT [FK_Users_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id]);
	ALTER TABLE [dbo].[Users] WITH CHECK CHECK CONSTRAINT [FK_Users_ToUsers_Modifier];

RETURN 0