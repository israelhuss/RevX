CREATE PROCEDURE [dbo].[spRates_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@StartDate DATETIME2(7),
	@EndDate DATETIME2(7) = null,
	@Rate float,
	@ProviderId int = null
AS
BEGIN
	IF @ProviderId is null
		INSERT INTO dbo.Rate (UserId, StartDate, EndDate, Rate)
			VALUES (@UserId, @StartDate, @EndDate, @Rate)
	ELSE 
		INSERT INTO dbo.RateByProvider(UserId, ProviderId, StartDate, EndDate, Rate)
			VALUES (@UserId, @ProviderId, @StartDate, @EndDate, @Rate)
	SELECT @@ROWCOUNT
END