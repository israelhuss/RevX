CREATE PROCEDURE [dbo].[spClient_GetEnabled]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.Client
	WHERE [Enabled] = 1 AND USerId = @UserId;
END
