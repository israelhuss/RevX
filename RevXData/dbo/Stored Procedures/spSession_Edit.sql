CREATE PROCEDURE [dbo].[spSession_Edit]
	@Id int,
	@UserId nvarchar(128),
	@ClientId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@BillingStatusId int,
	@Notes nvarchar(250),	
	@InvoiceId int
AS
BEGIN
	UPDATE dbo.[Session]
	SET ClientId = @ClientId, 
		[Date] = @Date, 
		StartTime =  @StartTime,
		EndTime = @EndTime, 
		ProviderId = @ProviderId, 
		BillingStatusId = @BillingStatusId, 
		Notes = @Notes
	WHERE Id = @Id AND UserId = @UserId;
END
