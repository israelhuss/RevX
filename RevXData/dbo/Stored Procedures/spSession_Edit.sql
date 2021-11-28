CREATE PROCEDURE [dbo].[spSession_Edit]
	@Id int,
	@UserId nvarchar(128),
	@StudentId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@BillingStatusId int,
	@InvioceId int = null,
	@Notes nvarchar(250)
AS
BEGIN
	UPDATE dbo.[Session]
	SET StudentId = @StudentId, 
		[Date] = @Date, 
		StartTime =  @StartTime,
		EndTime = @EndTime, 
		ProviderId = @ProviderId, 
		BillingStatusId = @BillingStatusId, 
		InvoiceId = @InvioceId,
		Notes = @Notes
	WHERE Id = @Id AND UserId = @UserId;
END
