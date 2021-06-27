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
IF NOT EXISTS(SELECT Id FROM dbo.Session 
						WHERE @StudentId = StudentId
						AND @Date = [Date]
						AND ((@StartTime >= StartTime AND @StartTime < EndTime) OR (@EndTime <= EndTime AND @EndTime > StartTime)))
BEGIN
	INSERT INTO dbo.Session (StudentId, [Date], StartTime, EndTime, ProviderId, BillingStatusId, Notes)
	VALUES (@StudentId, @Date, @StartTime, @EndTime, @ProviderId, @BillingStatusId, @Notes)
END
