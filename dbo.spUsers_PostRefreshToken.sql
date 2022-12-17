CREATE PROCEDURE [dbo].[spUsers_PostRefreshToken]
	@token nvarchar(MAX),
	@expiration DateTime,
	@userName varchar(128)
AS
	DECLARE @UserId AS NVARCHAR(450)

	SELECT @UserId = [Id]
	FROM [dbo].[AspNetUsers]
	WHERE [UserName] = @userName

	IF NOT EXISTS ( SELECT 1 FROM [dbo].[AspNetUserTokens] WHERE [UserId] = @UserId AND [LoginProvider] = 'LocalApi' AND [Name] = 'RefreshToken' )

		INSERT INTO [dbo].[AspNetUserTokens]
					([UserId]
					,[LoginProvider]
					,[Name]
					,[Value]
					,[Expiration])
				VALUES
					(@UserID
					,'LocalApi'
					,'RefreshToken'
					,@token
					,@expiration) 

	ELSE

		UPDATE [dbo].[AspNetUserTokens]
		   SET [Value] = @token
		   ,[Expiration] = @expiration
		 WHERE [UserId] = @UserId AND [LoginProvider] = 'LocalApi' AND [Name] = 'RefreshToken' 
RETURN 0