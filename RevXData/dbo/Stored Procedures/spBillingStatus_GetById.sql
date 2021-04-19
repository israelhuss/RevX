CREATE PROCEDURE [dbo].[spBillingStatus_GetById]
	@Id int
AS
BEGIN
	SET NOCOUNT ON

	SELECT [Id], [BillingStatus]
	FROM dbo.BillingStatus
	WHERE Id = @Id
END
