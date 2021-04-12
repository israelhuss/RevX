CREATE PROCEDURE [dbo].[spSession_Delete]
	@Id int
AS
BEGIN
	DELETE FROM dbo.[Session] WHERE Id = @Id;
END
