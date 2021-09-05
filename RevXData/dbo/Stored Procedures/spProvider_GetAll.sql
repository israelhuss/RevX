CREATE PROCEDURE [dbo].[spProvider_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [UserId], [Name], [Enabled]
	FROM dbo.Provider
	WHERE UserId = @UserId
END
