CREATE PROCEDURE [dbo].[spRates_getAll]
	@UserId nvarchar(128),
	@ProviderId int = null
AS	
BEGIN
	IF @ProviderId is null OR NOT EXISTS (SELECT * FROM dbo.[RateByProvider]
				where UserId = @UserId AND ProviderId = @ProviderId)
	BEGIN
		Create Table #TempRates(Id int, [UserId] NVARCHAR(128), [StartDate] DATETIME2, [EndDate] DATETIME2, [Rate] FLOAT, [ProviderId] INT)
		INSERT INTO #TempRates(Id, UserId, StartDate, EndDate, Rate) SELECT * FROM Rate Where UserId = @UserId
		INSERT INTO #TempRates(Id, UserId, StartDate, EndDate, Rate, ProviderId) SELECT * FROM RateByProvider Where UserId = @UserId AND ProviderId = @ProviderId

		SELECT * FROM #TempRates
	END
	ELSE
	BEGIN
		SELECT * FROM dbo.[RateByProvider] 
				where UserId = @UserId AND ProviderId = @ProviderId
	END
END