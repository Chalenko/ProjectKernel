CREATE PROCEDURE [dbo].[get_name]
	@id int = 0
AS
	SELECT first_name FROM Users WHERE id = @id
RETURN 0