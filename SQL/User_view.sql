USE [DBTEST]
GO

/****** Object:  View [dbo].[User_view]    Script Date: 08/10/2017 11:17:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[User_view]
	AS SELECT u.id AS 'id', u.surname AS 'Фамилия', u.first_name AS 'Имя', u.patronymic_name AS 'Отчество', u.login AS 'Логин', u.change_password AS 'Сменить пароль при первом входе', u.department AS 'Подразделение', a.last_login_datetime AS 'Дата последнего входа', a.last_login_workstation AS 'ПК последнего входа', cr.login AS 'Создатель', u.create_date AS 'Дата создания', m.login AS 'Модификатор', u.modify_date AS 'Дата изменения' 
	FROM [dbo].[User] AS u
	LEFT JOIN [dbo].[User] AS cr ON u.creator_id = cr.id
	LEFT JOIN [dbo].[User] AS m ON u.modifier_id = m.id
	LEFT JOIN [dbo].[Activity] AS a ON u.login = a.user_login

GO

