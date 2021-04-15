CREATE PROCEDURE [dbo].[spInvoiceDetail_Insert]
	@Id int = 0,
	@InvoiceId int,
	@SessionId int
AS
BEGIN
	INSERT INTO dbo.InvoiceDetail (InvoiceId, SessionId)
	VALUES (@InvoiceId, @SessionId);
END
