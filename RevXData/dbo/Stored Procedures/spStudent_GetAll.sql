CREATE PROCEDURE [dbo].[spStudent_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserId], [FirstName], [LastName], [Enabled]
	FROM dbo.Student
	WHERE UserId = @UserId
END
