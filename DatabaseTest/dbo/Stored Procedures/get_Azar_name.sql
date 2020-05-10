CREATE PROCEDURE [dbo].[get_Azar_name]
AS
	--SELECT first_name, department FROM Users WHERE surname LIKE 'Chalenko'
	SELECT first_name, department FROM Users WHERE surname = N'Азар'
RETURN 0