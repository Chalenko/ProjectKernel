CREATE PROCEDURE [dbo].[set_department]
	@id int,
	@value nvarchar(MAX)
AS
	UPDATE Users SET department = @value WHERE id = @id
RETURN 0