CREATE PROCEDURE [dbo].[spSession_GetByBillingStatus]
	@BillingStatusId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [StudentId], [Date], [StartTime], [EndTime], [ProviderId], [BillingStatusId], [Notes]
	FROM dbo.Session
	WHERE BillingStatusId = @BillingStatusId;
END
