CREATE PROCEDURE [dbo].[spSession_GetAll]

AS	
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [Date], [StartTime], [EndTime], [StatusId], [ProviderId], [StudentId]
	FROM dbo.Sessions
END