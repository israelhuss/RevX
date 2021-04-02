CREATE PROCEDURE [dbo].[spProvider_GetAll]
	
AS
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [Name]
	FROM dbo.Providers
END
