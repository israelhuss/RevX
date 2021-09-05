CREATE PROCEDURE [dbo].[spProvider_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@Name nvarchar(50),
	@Enabled bit
AS
BEGIN
	UPDATE Provider SET [Name] = @Name, [Enabled] = @Enabled Where Id = @Id AND UserId = @UserId
END