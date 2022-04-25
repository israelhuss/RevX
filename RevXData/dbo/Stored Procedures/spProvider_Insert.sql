CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@Name varchar(20),
	@Enabled bit = 1,
	@IsDefault bit = 0
AS
BEGIN
	INSERT INTO dbo.[Provider] (UserId, [Name], [Enabled], [IsDefault])
	VALUES (@UserId, @Name, @Enabled, @IsDefault)

	SELECT SCOPE_IDENTITY()
END
