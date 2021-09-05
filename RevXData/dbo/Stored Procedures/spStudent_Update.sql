CREATE PROCEDURE [dbo].[spStudent_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@FirstName nvarchar(50),
	@LastName nvarchar(50),
	@Enabled bit
AS
BEGIN
	UPDATE Student SET FirstName = @FirstName, LastName = @LastName, [Enabled] = @Enabled
	Where Id = @Id AND UserId = @UserId;
END