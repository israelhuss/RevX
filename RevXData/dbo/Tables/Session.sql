CREATE TABLE [dbo].[Session]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[StudentId] INT NOT NULL,
	[Date] DATE NOT NULL, 
	[StartTime] TIME NOT NULL, 
	[EndTime] TIME NOT NULL,
	[ProviderId] INT NOT NULL, 
	[BillingStatusId] INT NOT NULL,
	[Notes] NVARCHAR(250) NULL, 
    CONSTRAINT [FK_Session_ToStatus] FOREIGN KEY ([BillingStatusId]) REFERENCES [BillingStatus]([Id]),
	CONSTRAINT [FK_Session_ToProvider] FOREIGN KEY ([ProviderId]) REFERENCES [Provider]([Id]),
	CONSTRAINT [FK_Session_ToStudent] FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id])
)
