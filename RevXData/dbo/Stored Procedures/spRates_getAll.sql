CREATE PROCEDURE [dbo].[spRates_getAll]
	@UserId nvarchar(128)
AS	
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [UserId], [StartDate], [EndDate], [Rate]
	FROM dbo.[HourlyRate]
	WHERE UserId = @UserId
END