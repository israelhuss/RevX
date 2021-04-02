CREATE PROCEDURE [dbo].[spProvider_GetById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name] 
	FROM dbo.Providers 
	WHERE Id = @Id;
END
