CREATE PROCEDURE [dbo].[spSession_GetAll]

AS	
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.[Session]
END