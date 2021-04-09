CREATE PROCEDURE [dbo].[spSession_Delete]
	@Id int
AS
BEGIN
	DELETE FROM dbo.Sessions WHERE Id = @Id;
END
