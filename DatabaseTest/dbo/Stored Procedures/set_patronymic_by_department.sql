CREATE PROCEDURE [dbo].[set_patronymic_by_department]
	@patronymic NVARCHAR(MAX),
	@department NVARCHAR(MAX)
AS
	UPDATE Users SET patronymic_name = @patronymic WHERE department = @department
RETURN 0