CREATE PROCEDURE [dbo].[spProvider_GetEnabled]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.[Provider]
	WHERE [Enabled] = 1 AND UserId = @UserId
END