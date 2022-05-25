CREATE PROCEDURE [dbo].[spReport_SetAsDefault]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	UPDATE Report SET IsDefault = 0 WHERE UserId = @UserId;

	UPDATE Report SET IsDefault = 1 WHERE Id = @Id AND UserId = @UserId;
END