CREATE PROCEDURE [dbo].[spRates_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@StartDate DATETIME2(7),
	@EndDate DATETIME2(7) = null,
	@Rate float
AS
BEGIN
	INSERT INTO dbo.HourlyRate (UserId, StartDate, EndDate, Rate)
	VALUES (@UserId, @StartDate, @EndDate, @Rate)
END