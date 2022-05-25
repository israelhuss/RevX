CREATE PROCEDURE [dbo].[spReportItem_Insert]
	@Id int = 0,
	@ReportId int,
	@ViewAs int,
	@Color nvarchar(50) = null,
	@Nickname nvarchar(50) = null
AS
BEGIN
	INSERT INTO dbo.ReportItem(ReportId, ViewAs, Color, Nickname)
	VALUES (@ReportId, @ViewAs, @Color, @Nickname)

	SELECT SCOPE_IDENTITY()
END
