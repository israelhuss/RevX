CREATE PROCEDURE [dbo].[spReportItemDetail_GetByReportItemId]
	@ReportItemId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT *
	FROM dbo.ReportItemDetail
	WHERE ReportItemId = @ReportItemId
END
