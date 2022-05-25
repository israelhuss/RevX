CREATE PROCEDURE [dbo].[spReport_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [UserId], [Title], [ReportStyle], [StartDate], [EndDate], [GroupBy], [IsDefault]
	FROM dbo.Report
	WHERE UserId = @UserId
END
