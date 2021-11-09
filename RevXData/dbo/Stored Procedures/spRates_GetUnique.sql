--THIS PROCEDURE WILL RETURN GENERAL RATE ONLY IF AT LEAST ONE PROVIDER DOESNT HAVE A UNIQUE ENTRY

CREATE PROCEDURE [dbo].[spRates_getUnique]
	@UserId nvarchar(128),
	@ProviderId int = null
AS	
BEGIN
	IF @ProviderId is null
	BEGIN
		DECLARE @ProviderCount int;
		SET @ProviderCount = (SELECT COUNT(Id) FROM dbo.Provider WHERE UserId = @UserId)
		Create Table #TempRates(Id int, [UserId] NVARCHAR(128), [StartDate] DATETIME2, [EndDate] DATETIME2, [Rate] FLOAT, [ProviderId] INT)
		DECLARE @i int = 0;
		DECLARE @CurrentProviderId int;
		WHILE @i < @ProviderCount
			BEGIN
				SET @CurrentProviderId = (SELECT Id FROM dbo.Provider WHERE UserId = @UserId ORDER BY Id OFFSET @i ROWS FETCH NEXT 1 ROWS ONLY)
				IF NOT EXISTS (SELECT * FROM dbo.[RateByProvider]
					WHERE UserId = @UserId AND ProviderId = @CurrentProviderId)
						INSERT INTO #TempRates(Id, UserId, StartDate, EndDate, Rate) 
							SELECT * FROM dbo.[Rate] 
								where UserId = @UserId
				ELSE 
					INSERT INTO #TempRates(Id, UserId, StartDate, EndDate, Rate, ProviderId)
						SELECT * FROM dbo.[RateByProvider] 
							where UserId = @UserId AND ProviderId = @CurrentProviderId
				SET @i = @i + 1
			END

		SELECT * FROM #TempRates
	END
	ELSE
	BEGIN
		SELECT * FROM dbo.[RateByProvider] 
				where UserId = @UserId AND ProviderId = @ProviderId
	END
END