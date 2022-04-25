CREATE PROCEDURE [dbo].[spPendingSession_Delete]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	DELETE FROM PendingSession Where Id = @Id AND UserId = @UserId
END