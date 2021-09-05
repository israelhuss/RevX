CREATE PROCEDURE [dbo].[spStudent_GetById]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	SELECT [Id], [UserId], [FirstName], [LastName], [Enabled]
	FROM dbo.Student
	WHERE Id = @Id AND UserId = @UserId;
END
