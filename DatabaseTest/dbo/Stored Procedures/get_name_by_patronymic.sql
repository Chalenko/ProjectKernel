CREATE PROCEDURE [dbo].[get_name_by_patronymic]
	@patronymic NVARCHAR(MAX)
AS
	SELECT first_name FROM Users WHERE patronymic_name = @patronymic
RETURN 0