CREATE PROCEDURE [dbo].[spBillingStatus_Insert]
	@Id int = 0,
	@BillingStatus varchar(20),
	@Enabled bit = 1
AS
BEGIN
	INSERT INTO dbo.BillingStatus (BillingStatus)
	VALUES (@BillingStatus)
END
