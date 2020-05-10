CREATE PROCEDURE [dbo].[set_Azar_department]
AS
	UPDATE Users SET department = 'NNSU' WHERE surname LIKE N'Азар'
RETURN 0