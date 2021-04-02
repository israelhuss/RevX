CREATE PROCEDURE [dbo].[spBillingStatus_GetAll]

AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [BillingStatus]
	FROM dbo.BillingStatus
END
