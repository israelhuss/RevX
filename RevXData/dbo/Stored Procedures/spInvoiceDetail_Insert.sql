CREATE PROCEDURE [dbo].[spInvoiceDetail_Insert]
	@Id int = 0,
	@UserId nvarchar(128),
	@InvoiceId int,
	@SessionId int
AS
BEGIN
	INSERT INTO dbo.InvoiceDetail (UserId, InvoiceId, SessionId)
	VALUES (@UserId, @InvoiceId, @SessionId);
END
