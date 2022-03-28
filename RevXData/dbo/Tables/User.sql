CREATE TABLE [dbo].[User]
(
	[Id] NVARCHAR(128) NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL, 
	[EmailAddress] NVARCHAR(256) NOT NULL, 
	[CreatedDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
    [WorkplaceId] INT NULL,
	[EncoreUser] NVARCHAR(256) NULL, 
	[EncorePass] NVARCHAR(50) NULL, 
	[DownloadEncore] BIT NOT NULL DEFAULT 0,
	[BillingCycleId] INT NULL, 
    CONSTRAINT [FK_User_ToWorkplace] FOREIGN KEY ([WorkplaceId]) REFERENCES [Workplace]([Id]),
	CONSTRAINT [FK_User_ToBillingCycle] FOREIGN KEY ([BillingCycleId]) REFERENCES [BillingCycle]([Id]), 
)