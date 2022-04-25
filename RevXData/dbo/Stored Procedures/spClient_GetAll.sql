CREATE PROCEDURE [dbo].[spClient_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Client
	WHERE UserId = @UserId
END
