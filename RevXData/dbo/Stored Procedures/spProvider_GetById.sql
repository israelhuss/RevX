CREATE PROCEDURE [dbo].[spProvider_GetById]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	SELECT *
	FROM dbo.Provider
	WHERE Id = @Id AND UserId = @UserId;
END
