CREATE TABLE [dbo].[InvoiceDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL,
	[InvoiceId] INT NOT NULL, 
	[SessionId] INT NOT NULL, 
	CONSTRAINT [FK_InvoiceDetail_ToInvoice] FOREIGN KEY (InvoiceId) REFERENCES dbo.Invoice(Id), 
	CONSTRAINT [FK_InvoiceDetail_ToSession] FOREIGN KEY (SessionId) REFERENCES dbo.Session(Id),
	CONSTRAINT [FK_InvoiceDetail_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
