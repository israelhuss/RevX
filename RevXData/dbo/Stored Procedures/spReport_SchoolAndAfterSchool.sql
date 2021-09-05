CREATE PROCEDURE [dbo].[spReport_SchoolAndAfterSchool]
	@GroupBy char(20) = 'month',
	@UserId nvarchar(128),
	@StartDate date,
	@EndDate date = null
AS
IF @EndDate is null SET @EndDate = GETDATE();
BEGIN
	SELECT [Date] = (select (case @GroupBy
			when 'year' then cast(DATEADD(YEAR, DATEDIFF(YEAR, 0, [Date]), 0) as date)
			when 'month' then cast(DATEADD(MONTH, DATEDIFF(MONTH, 0, [Date]), 0) as date)
			when 'day' then cast(DATEADD(DAY, DATEDIFF(DAY, 0, [Date]), 0) as date)
				 end)),
	cast(SUM(CASE 
		WHEN ProviderId = 2 AND EndTime >= '15:00:00' AND StartTime <= '15:00:00' THEN DATEDIFF(MINUTE, StartTime, '15:00:00') 
		WHEN ProviderId = 2 AND StartTime <= '15:00:00' AND EndTime <= '15:00:00' THEN DATEDIFF(MINUTE, StartTime, EndTime)
	ELSE 0
	END) as float) / cast(60 as float)
	as SchoolPrimary,
	cast(SUM(CASE 
		WHEN ProviderId = 1 AND EndTime >= '15:00:00' AND StartTime <= '15:00:00' THEN DATEDIFF(MINUTE, StartTime, '15:00:00') 
		WHEN ProviderId = 1 AND StartTime <= '15:00:00' AND EndTime <= '15:00:00' THEN DATEDIFF(MINUTE, StartTime, EndTime)
	ELSE 0
	END) as float) / cast(60 as float)
	as SchoolSecondary,
	cast(SUM(CASE 
		WHEN ProviderId = 2 AND EndTime > '15:00:00' AND StartTime <= '15:00:00' THEN DATEDIFF(MINUTE, '15:00:00', EndTime)
		WHEN ProviderId = 2 AND EndTime > '15:00:00' AND StartTime > '15:00:00' THEN DATEDIFF(MINUTE, StartTime, EndTime)
		ELSE 0
	END) as float) / cast(60 as float)
	as AfterSchoolPrimary,
	cast(SUM(CASE 
		WHEN ProviderId = 1 AND EndTime > '15:00:00' AND StartTime <= '15:00:00' THEN DATEDIFF(MINUTE, '15:00:00', EndTime)
		WHEN ProviderId = 1 AND EndTime > '15:00:00' AND StartTime > '15:00:00' THEN DATEDIFF(MINUTE, StartTime, EndTime)
		ELSE 0
	END) as float) / cast(60 as float)
	as AfterSchoolSecondary	FROM dbo.Session
	where [Date] between @StartDate and @EndDate AND BillingStatusId <> 1 and UserId = @UserId
	group by case @GroupBy
			when 'year' then cast(DATEADD(YEAR, DATEDIFF(YEAR, 0, [Date]), 0) as date)
			when 'month' then cast(DATEADD(MONTH, DATEDIFF(MONTH, 0, [Date]), 0) as date)
			when 'day' then cast(DATEADD(DAY, DATEDIFF(DAY, 0, [Date]), 0) as date)
				 end
END