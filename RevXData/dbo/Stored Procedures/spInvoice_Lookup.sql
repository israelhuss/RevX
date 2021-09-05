CREATE PROCEDURE [dbo].[spInvoice_Lookup]
	@InvoiceDate datetime2,
	@UserId nvarchar(128),
	@TotalHours decimal(4,2)
AS
BEGIN
	SELECT Id FROM dbo.Invoice
	WHERE InvoiceDate = @InvoiceDate AND TotalHours = @TotalHours and UserId = @UserId;
END
