CREATE PROCEDURE [dbo].[spRates_Update]
	@Id int,
	@UserId nvarchar(128),
	@StartDate DATETIME2,
	@EndDate DATETIME2,
	@Rate float,
	@ProviderId int = null
AS
BEGIN
	IF (@ProviderId is null) 
		BEGIN
			IF NOT EXISTS (SELECT * FROM RateByProvider Where Id = @Id AND UserId = @UserId)
				UPDATE Rate SET StartDate = @StartDate, EndDate = @EndDate, Rate = @Rate Where Id = @Id AND UserId = @UserId
			ELSE 
				INSERT INTO Rate(UserId, StartDate, EndDate, Rate) SELECT UserId, StartDate, EndDate, Rate FROM RateByProvider Where Id = @Id AND UserId = @UserId
				DELETE FROM RateByProvider Where Id = @Id AND UserId = @UserId;
		END
	ELSE
		BEGIN
			IF NOT EXISTS (SELECT * FROM Rate Where Id = @Id AND UserId = @UserId)
				UPDATE RateByProvider SET StartDate = @StartDate, EndDate = @EndDate, Rate = @Rate, ProviderId = @ProviderId Where Id = @Id AND UserId = @UserId 
			ELSE
				INSERT INTO RateByProvider(UserId, StartDate, EndDate, Rate, ProviderId) SELECT UserId, StartDate, EndDate, Rate, @ProviderId FROM Rate WHERE Id = @Id AND UserId = @UserId
				DELETE FROM Rate WHERE Id = @Id AND UserId = @UserId
		END
END