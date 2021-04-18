CREATE PROCEDURE [dbo].[spInvoice_Lookup]
	@InvoiceDate datetime2,
	@TotalHours decimal(4,2)
AS
BEGIN
	SELECT Id FROM dbo.Invoice
	WHERE InvoiceDate = @InvoiceDate AND TotalHours = @TotalHours;
END
