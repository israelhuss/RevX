CREATE PROCEDURE [dbo].[spInvoice_Insert]
	@Id int = 0,
	@InvoiceDate datetime2,
	@TotalHours decimal(4,2)
AS
BEGIN
	INSERT INTO dbo.Invoice (InvoiceDate, TotalHours)
	VALUES (@InvoiceDate, @TotalHours);
END
