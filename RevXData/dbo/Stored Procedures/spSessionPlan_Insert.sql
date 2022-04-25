CREATE PROCEDURE [dbo].[spSessionPlan_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@ClientId int,
	@ProviderId int,
	@DateFrom date,
	@DateTo date,
	@StartTime time,
	@EndTime time,
	@DayOfWeek int
AS
BEGIN
	INSERT INTO dbo.[SessionPlan] (UserId, [ClientId], [ProviderId], [DateFrom], [DateTo], [StartTime], [EndTime], [DayOfWeek])
	VALUES (@UserId, @ClientId, @ProviderId, @DateFrom, @DateTo, @StartTime, @EndTime, @DayOfWeek)

	SELECT SCOPE_IDENTITY()
END
