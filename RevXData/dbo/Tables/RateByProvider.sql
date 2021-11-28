CREATE TABLE [dbo].[RateByProvider]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL ,
	[StartDate] DATETIME2 NOT NULL, 
	[EndDate] DATETIME2 NULL, 
	[Rate] FLOAT NOT NULL,
	[ProviderId] INT NOT NULL DEFAULT 0,
	CONSTRAINT [FK_RateByProvider_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
	CONSTRAINT [FK_RateByProvider_ToProvider] FOREIGN KEY ([ProviderId]) REFERENCES [Provider]([Id])
)
