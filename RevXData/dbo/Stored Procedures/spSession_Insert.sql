CREATE PROCEDURE [dbo].[spSession_Insert]
	@Id int = 0,
	@StudentId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@StatusId int,
	@Notes nvarchar(250)
AS
BEGIN
	INSERT INTO dbo.Sessions (StudentId, [Date], StartTime, EndTime, ProviderId, StatusId, Notes)
	VALUES (@StudentId, @Date, @StartTime, @EndTime, @ProviderId, @StatusId, @Notes)
END
