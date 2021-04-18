CREATE PROCEDURE [dbo].[spSession_GetById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.Session
	WHERE Id = @Id;

END