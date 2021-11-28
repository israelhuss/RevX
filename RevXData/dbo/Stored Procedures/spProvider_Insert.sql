CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@Name varchar(20),
	@Enabled bit = 1
AS
BEGIN
	INSERT INTO dbo.[Provider] (UserId, [Name])
	VALUES (@UserId, @Name)

	SELECT SCOPE_IDENTITY()
END
