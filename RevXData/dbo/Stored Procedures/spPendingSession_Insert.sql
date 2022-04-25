CREATE PROCEDURE [dbo].[spPendingSession_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@Date date,
	@PlanId int
AS
BEGIN
	INSERT INTO dbo.[PendingSession] (UserId, [Date], [PlanId])
	VALUES (@UserId, @Date, @PlanId)

	SELECT SCOPE_IDENTITY()
END
