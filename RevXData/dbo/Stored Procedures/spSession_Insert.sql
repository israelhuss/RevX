CREATE PROCEDURE [dbo].[spSession_Insert]
	@Id int = 0,
	@StudentId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@BillingStatusId int,
	@Notes nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Session (StudentId, [Date], StartTime, EndTime, ProviderId, BillingStatusId, Notes)
	VALUES (@StudentId, @Date, @StartTime, @EndTime, @ProviderId, @BillingStatusId, @Notes)
END
