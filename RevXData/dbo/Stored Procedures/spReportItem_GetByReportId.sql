CREATE PROCEDURE [dbo].[spReportItem_GetByReportId]
	@ReportId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.ReportItem
	WHERE ReportId = @ReportId
END
