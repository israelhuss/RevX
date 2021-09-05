CREATE PROCEDURE [dbo].[spBillingStatus_GetAll]
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [Id], [BillingStatus], [Enabled]
	FROM dbo.BillingStatus
END
