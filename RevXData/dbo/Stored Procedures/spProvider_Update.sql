CREATE PROCEDURE [dbo].[spProvider_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@Name nvarchar(50),
	@Enabled bit,
	@IsDefault bit = 0
AS
BEGIN
	UPDATE Provider SET [Name] = @Name, [Enabled] = @Enabled, IsDefault = @IsDefault Where Id = @Id AND UserId = @UserId
END