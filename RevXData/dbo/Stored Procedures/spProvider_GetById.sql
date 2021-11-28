CREATE PROCEDURE [dbo].[spProvider_GetById]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	SELECT [Id], [UserId], [Name], [Enabled]
	FROM dbo.Provider
	WHERE Id = @Id AND UserId = @UserId;
END
