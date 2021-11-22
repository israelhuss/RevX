CREATE PROCEDURE [dbo].[spInvoice_GetAll]
	@UserId nvarchar(128)
AS
BEGIN
	SET NOCOUNT ON
	SELECT [Id], [UserId], [ProviderId], [StartDate], [EndDate], [InvoiceDate], [TotalHours], [Rate], [Total]
	FROM dbo.Invoice
	WHERE UserId = @UserId
END
