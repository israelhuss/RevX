CREATE PROCEDURE [dbo].[spSession_GetAll]
	@UserId nvarchar(128)
AS	
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [UserId], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.[Session]
	WHERE UserId = @UserId
	ORDER BY [Date] , [StartTime]
END