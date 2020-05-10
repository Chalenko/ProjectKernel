USE [DBTEST]
GO

/****** Объект: Table [dbo].[User] Дата скрипта: 19.07.2016 10:37:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User] (
    [id]              UNIQUEIDENTIFIER  NOT NULL,
    [surname]         NVARCHAR (50)		NOT NULL,
    [first_name]      NVARCHAR (50)		NOT NULL,
    [patronymic_name] NVARCHAR (50)		NULL,
    [login]           NVARCHAR (50)		NOT NULL,
    [salt]            NVARCHAR (50)		NOT NULL,
    [password]        NVARCHAR (100)	NOT NULL,
    [department]      NVARCHAR (50)		NOT NULL,
    [creator_id]      UNIQUEIDENTIFIER  NOT NULL,
    [create_date]     DATETIME			NOT NULL,
    [modifier_id]     UNIQUEIDENTIFIER  NULL,
    [modify_date]     DATETIME			NULL,
	PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_Users_Column] UNIQUE NONCLUSTERED ([login] ASC),
    CONSTRAINT [FK_Users_ToUsers_Creator] FOREIGN KEY ([creator_id]) REFERENCES [dbo].[Users] ([id]),
    CONSTRAINT [FK_Users_ToUsers_Modifier] FOREIGN KEY ([modifier_id]) REFERENCES [dbo].[Users] ([id])
);

DECLARE @first_id NVARCHAR(50)
SET @first_id = newid()

INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (@first_id ,'Admin' ,'Admin' ,'Admin' ,'Admin' ,'DEp5xD96ERuX53cSW1UPk2fQ/rZ0EV5M495xFOm/DHU=' ,'F1Kqu8j00yxLmOij7mR7cEpJ1KhwZ3DQT2DiZMvLvftJneKTjyrJy0WsRI/rTFAdgauC2UBnZLnVL55JBSdZTQ==', 0, NULL, @first_id, GETDATE());
/*
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (@first_id ,'Chalenko' ,'Pavel' ,'Vladislavovich' ,'p.v.chalenko' ,'jE0787BkT84+kjZe/QsTib70o3OXjAbo0+0qqlu5WCc=' ,'2t4xV8JoDsLg61Q2U5zcPDXlOvNqqPWxVAJEldfzy0uffcWh+06JooUzZcKv1NFVGP7l9E36JqoKxK8ow0auxw==', 0, 'ORPOiTP', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Ильичев' ,N'Андрей' ,N'Сергеевич' ,'a.s.ilichev' ,'i3WNDvP49h/qTOF7pJrHp5LUCt74WFn1fgpJr5ffRAo=' ,'K3yPrAI/5bNd1y1ldbxiY1dtEPDklh6yBVpiaX/XwWrqY3xPjYsVT+T1E3uWEBTJ+0SGq0xBg6IHMWvIAFADIg==', 0, N'ОРПОиТП', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Азар' ,N'Джон' ,N'' ,'j.azar' ,'' ,'757602046', 0, N'ННГУ', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Крылова' ,N'Любовь' ,N'Львовна' ,'l.l.krylova' ,'' ,'757602046', 0, N'ННГУ', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Леванова' ,N'Татьяна' ,N'Александровна' ,'t.a.levanova' ,'' ,'757602046', 0, N'ННГУ', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Сергиенко' ,N'Сергей' ,N'Эдуардович' ,'s.e.sergienko' ,'' ,'757602046',0,  N'Freemake', @first_id, GETDATE()); 
INSERT INTO [dbo].[User] ([id] ,[surname] ,[first_name] ,[patronymic_name] ,[login] ,[salt] ,[password], [change_password], [department], [creator_id], [create_date]) VALUES (newid() ,N'Николаев' ,N'Артем' ,N'Олегович' ,'a.o.nikolaev' ,'fu/6RvHylF11q50iE6mgtMQ3BjzyKxW6z4H7QxhC7hQ=' ,'UlU/Y3mr+afBCBYlOrs+8ackQYuOafPaJ7WEBYZRHEN6VC/an+gVTr8eEunsr0zdEUx3a9ZARiZTHmDkCRY90Q==', 0, N'HSE', @first_id, GETDATE()); 
*/

