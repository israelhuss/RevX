CREATE PROCEDURE [dbo].[spPendingSession_GetByMonth]
	@UserId nvarchar(128),
	@Month int,
	@Year int
AS
BEGIN
	SELECT [p].[Id], [p].[UserId], [p].[Date], [s].[ClientId], [s].[ProviderId], [s].[StartTime], [s].[EndTime] FROM PendingSession p
	JOIN SessionPlan s ON p.PlanId = s.Id
	WHERE s.UserId = @UserId
       AND Month([p].[Date]) = @Month 
       AND YEAR([p].[Date]) = @Year
END