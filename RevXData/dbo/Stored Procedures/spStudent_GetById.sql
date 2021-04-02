CREATE PROCEDURE [dbo].[spStudent_GetById]
	@Id int
AS
BEGIN
	SELECT [Id], [FirstName], [LastName] 
	FROM dbo.Students WHERE Id = @Id;
END
