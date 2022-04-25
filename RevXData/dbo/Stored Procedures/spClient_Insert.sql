CREATE PROCEDURE [dbo].[spClient_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Enabled bit = 1,
	@IsDefault bit = 0
AS
BEGIN
	INSERT INTO dbo.Client (UserId, FirstName, LastName, [Enabled], IsDefault)
	VALUES (@UserId, @FirstName, @LastName, @Enabled, @IsDefault)

	SELECT SCOPE_IDENTITY()
END