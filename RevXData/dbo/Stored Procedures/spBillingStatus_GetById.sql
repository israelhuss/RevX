CREATE PROCEDURE [dbo].[spBillingStatus_GetById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT [Id], [BillingStatus], [Enabled]
	FROM dbo.BillingStatus
	WHERE Id = @Id
END
