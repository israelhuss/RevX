CREATE PROCEDURE [dbo].[spStudent_GetAll]
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [FirstName], [LastName]
	FROM dbo.Student
END
