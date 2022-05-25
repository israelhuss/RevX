CREATE PROCEDURE [dbo].[spReport_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@Title varchar(50),
	@ReportStyle int,
	@StartDate date,
	@EndDate date,
	@GroupBy int,
	@IsDefault bit
AS
BEGIN
	IF @Id > 0
		BEGIN
			SET IDENTITY_INSERT dbo.Report ON;
			INSERT INTO dbo.Report (Id, UserId, Title, ReportStyle, StartDate, EndDate, GroupBy, IsDefault)
			VALUES (@Id, @UserId, @Title, @ReportStyle, @StartDate, @EndDate, @GroupBy, @IsDefault);
			SET IDENTITY_INSERT dbo.Report OFF;
		END
	ELSE
		BEGIN
			INSERT INTO dbo.Report ( UserId, Title, ReportStyle, StartDate, EndDate, GroupBy, IsDefault)
			VALUES (@UserId, @Title, @ReportStyle, @StartDate, @EndDate, @GroupBy, @IsDefault)
		END
	SELECT SCOPE_IDENTITY()
END
