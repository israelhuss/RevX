CREATE PROCEDURE [dbo].[spReport_Delete]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	DELETE FROM Report Where Id = @Id AND UserId = @UserId
END