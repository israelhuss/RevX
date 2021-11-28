CREATE PROCEDURE [dbo].[spRates_GetByDate]
	@Date DATETIME,
	@UserId nvarchar(128),
	@ProviderId int = null
AS
BEGIN
	Create Table #RatesFromSp(Id int, [UserId] NVARCHAR(128), [StartDate] DATETIME2, [EndDate] DATETIME2, [Rate] FLOAT, [ProviderId] INT)
	INSERT INTO #RatesFromSp EXEC spRates_getAll @UserId = @UserId

	IF (@ProviderId is null) OR NOT EXISTS (SELECT * FROM #RatesFromSp
		WHERE StartDate <= @Date AND (EndDate is null or EndDate >= @Date) AND ProviderId = @ProviderId AND UserId = @UserId)
			SELECT * FROM #RatesFromSp WHERE StartDate <= @Date AND (EndDate is null or EndDate >= @Date) AND UserId = @UserId
	ELSE 
		  SELECT * FROM #RatesFromSp WHERE StartDate <= @Date AND (EndDate is null or EndDate >= @Date) AND ProviderId = @ProviderId AND UserId = @UserId
END