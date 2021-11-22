CREATE PROCEDURE [dbo].[spInvoice_Insert]
	@Id int = 0 output,
	@UserId nvarchar(128),
	@ProviderId int,
	@StartDate Date,
	@EndDate Date,
	@InvoiceDate datetime2,
	@TotalHours float,
	@Rate MONEY,
	@Total Money
AS
BEGIN
	INSERT INTO dbo.Invoice (UserId, StartDate, EndDate, InvoiceDate, ProviderId, TotalHours, Rate, Total)
	VALUES (@UserId, @StartDate, @EndDate, @InvoiceDate, @ProviderId, @TotalHours, @Rate, @Total);

	SELECT SCOPE_IDENTITY();
END
