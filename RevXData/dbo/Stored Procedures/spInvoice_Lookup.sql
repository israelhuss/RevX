CREATE PROCEDURE [dbo].[spInvoice_Lookup]
	@UserId nvarchar(128),
	@InvoiceId int
AS
BEGIN
	SELECT * FROM dbo.Invoice
	WHERE Id = @InvoiceId AND UserId = @UserId;
END
