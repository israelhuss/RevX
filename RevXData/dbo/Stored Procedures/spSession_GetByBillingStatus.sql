CREATE PROCEDURE [dbo].[spSession_GetByBillingStatus]
	@BillingStatusId int,
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [UserId], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.Session
	WHERE BillingStatusId = @BillingStatusId AND UserId = @UserId;
END
