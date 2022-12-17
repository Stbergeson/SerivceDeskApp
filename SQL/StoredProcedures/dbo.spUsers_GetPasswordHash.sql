CREATE PROCEDURE [dbo].[spUsers_GetPasswordHash]
	@userName nvarchar(128)
AS
	SELECT [PasswordHash]
	  FROM [dbo].[AspNetUsers]
	  WHERE [Username] = @userName
RETURN 0