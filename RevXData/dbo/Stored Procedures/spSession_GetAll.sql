CREATE PROCEDURE [dbo].[spSession_GetAll]
	@UserId nvarchar(128)
AS	
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [UserId], [ClientId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [InvoiceId], [Notes]
	FROM dbo.[Session]
	WHERE UserId = @UserId
	ORDER BY [Date] , [StartTime]
END