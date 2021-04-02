CREATE TABLE [dbo].[Sessions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[StudentId] INT NOT NULL,
	[Date] DATE NOT NULL, 
	[StartTime] TIME NOT NULL, 
	[EndTime] TIME NOT NULL,
	[ProviderId] INT NOT NULL, 
	[BillingStatusId] INT NOT NULL,
	[Notes] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Sessions_ToStatus] FOREIGN KEY ([BillingStatusId]) REFERENCES [BillingStatus]([Id]),
	CONSTRAINT [FK_Sessions_ToProviders] FOREIGN KEY ([ProviderId]) REFERENCES [Providers]([Id]),
	CONSTRAINT [FK_Sessions_ToStudents] FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id])
)
