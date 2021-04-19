CREATE PROCEDURE [dbo].[spSession_Edit]
	@Id int,
	@StudentId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@BillingStatusId int,
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
		Notes = @Notes
	WHERE Id = @Id;
END
