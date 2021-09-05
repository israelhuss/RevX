CREATE PROCEDURE [dbo].[spRates_GetByDate]
	@Date DATETIME,
	@UserId nvarchar(128)
AS
BEGIN
	SELECT * FROM dbo.HourlyRate WHERE StartDate <= @Date AND (EndDate is null or EndDate >= @Date) AND UserId = @UserId
END