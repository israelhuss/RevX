CREATE PROCEDURE [dbo].[spSession_GetById]
	@Id int,
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserId], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.Session
	WHERE Id = @Id AND UserId = @UserId;

END