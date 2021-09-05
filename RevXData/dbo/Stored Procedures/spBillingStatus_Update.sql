CREATE PROCEDURE [dbo].[spBillingStatus_Update]
	@Id int = 0,
	@BillingStatus nvarchar(50),
	@Enabled bit
AS
BEGIN
	UPDATE BillingStatus SET [BillingStatus] = @BillingStatus, [Enabled] = @Enabled
	Where Id = @Id
END