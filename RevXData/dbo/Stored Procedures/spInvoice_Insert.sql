CREATE PROCEDURE [dbo].[spInvoice_Insert]
	@Id int output,
	@InvoiceDate datetime2,
	@TotalHours decimal(4,2)
AS
BEGIN
	INSERT INTO dbo.Invoice (InvoiceDate, TotalHours)
	VALUES (@InvoiceDate, @TotalHours);

	SELECT @Id = SCOPE_IDENTITY();
END
