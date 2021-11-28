CREATE PROCEDURE [dbo].[spStudent_GetEnabled]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserId], [FirstName], [LastName], [Enabled]
	FROM dbo.Student
	WHERE [Enabled] = 1 AND USerId = @UserId;
END
