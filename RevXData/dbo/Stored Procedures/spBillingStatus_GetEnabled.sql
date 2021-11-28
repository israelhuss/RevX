CREATE PROCEDURE [dbo].[spBillingStatus_GetEnabled]
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [BillingStatus], [Enabled]
	FROM dbo.[BillingStatus]
	WHERE [Enabled] = 1
END