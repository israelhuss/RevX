CREATE PROCEDURE [dbo].[spSession_Delete]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	DELETE FROM dbo.[Session] 
	WHERE Id = @Id AND UserId = @UserId;
END
