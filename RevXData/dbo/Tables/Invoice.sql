﻿CREATE TABLE [dbo].[Invoice]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL DEFAULT '%REPLACE_ME%',
	[InvoiceDate] DATETIME2 NOT NULL, 
	[TotalHours] DECIMAL(4, 2) NOT NULL ,
	CONSTRAINT [FK_Invoice_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
