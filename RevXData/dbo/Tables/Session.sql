CREATE TABLE [dbo].[Session]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL DEFAULT '%REPLACE_ME%',
	[ClientId] INT NOT NULL,
	[Date] DATE NOT NULL, 
	[StartTime] TIME NOT NULL, 
	[EndTime] TIME NOT NULL,
	[ProviderId] INT NOT NULL, 
	[BillingStatusId] INT NOT NULL,
	[InvoiceId] INT NULL, 
	[Notes] NVARCHAR(250) NULL, 
	CONSTRAINT [FK_Session_ToStatus] FOREIGN KEY ([BillingStatusId]) REFERENCES [BillingStatus]([Id]),
	CONSTRAINT [FK_Session_ToProvider] FOREIGN KEY ([ProviderId]) REFERENCES [Provider]([Id]),
	CONSTRAINT [FK_Session_ToClient] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]),
	CONSTRAINT [FK_Session_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [FK_Session_ToInvoice] FOREIGN KEY ([InvoiceId]) REFERENCES [Invoice]([Id])
)
