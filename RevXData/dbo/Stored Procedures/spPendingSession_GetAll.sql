CREATE PROCEDURE [dbo].[spPendingSession_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT *
	FROM dbo.PendingSession
	WHERE UserId = @UserId
END
