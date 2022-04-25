CREATE PROCEDURE [dbo].[spClient_GetById]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	SELECT *
	FROM dbo.Client
	WHERE Id = @Id AND UserId = @UserId;
END
