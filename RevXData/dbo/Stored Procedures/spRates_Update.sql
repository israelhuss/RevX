CREATE PROCEDURE [dbo].[spRates_Update]
	@Id int = 0,
	@UserId nvarchar(128),
	@StartDate DATETIME2,
	@EndDate DATETIME2,
	@RATE float
AS
BEGIN
	UPDATE HourlyRate SET StartDate = @StartDate, EndDate = @EndDate, Rate = @Rate Where Id = @Id AND UserId = @UserId
END