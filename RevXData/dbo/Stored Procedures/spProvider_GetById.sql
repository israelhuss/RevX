CREATE PROCEDURE [dbo].[spProvider_GetById]
	@Id int
AS
BEGIN
	SELECT [Id], [Name] 
	FROM dbo.Provider
	WHERE Id = @Id;
END
