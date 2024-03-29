﻿CREATE TABLE [dbo].[User]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL, 
	[EmailAddress] NVARCHAR(256) NOT NULL, 
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
	[EncoreUser] NVARCHAR(256) NULL, 
	[EncorePass] NVARCHAR(50) NULL, 
	[DownloadEncore] BIT NOT NULL DEFAULT 0
)