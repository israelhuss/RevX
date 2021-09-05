CREATE PROCEDURE [dbo].[spInvoice_Insert]
	@Id int output,
	@UserId nvarchar(128),
	@InvoiceDate datetime2,
	@TotalHours decimal(4,2)
AS
BEGIN
	INSERT INTO dbo.Invoice (UserId, InvoiceDate, TotalHours)
	VALUES (@UserId, @InvoiceDate, @TotalHours);

	SELECT @Id = SCOPE_IDENTITY();
END
