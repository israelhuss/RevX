CREATE PROCEDURE [dbo].[spClient_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Enabled bit,
	@IsDefault bit = 0
AS
BEGIN
	UPDATE Client SET FirstName = @FirstName, LastName = @LastName, [Enabled] = @Enabled, IsDefault = @IsDefault
	Where Id = @Id AND UserId = @UserId;
END