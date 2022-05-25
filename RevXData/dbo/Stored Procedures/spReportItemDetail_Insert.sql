CREATE PROCEDURE [dbo].[spReportItemDetail_Insert]
	@Id int = 0,
	@ReportItemId int,
	@Field nvarchar(50),
	@Value nvarchar(200),
	@Operator int,
	@Level int
AS
BEGIN
	INSERT INTO dbo.ReportItemDetail(ReportItemId, Field, [Value], Operator, [Level])
	VALUES (@ReportItemId, @Field, @Value, @Operator, @Level)

	SELECT SCOPE_IDENTITY()
END
