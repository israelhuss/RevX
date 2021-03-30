CREATE TABLE [dbo].[Sessions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[StudentId] INT NOT NULL,
	[Date] DATE NOT NULL, 
	[StartTime] TIME(0) NOT NULL, 
	[EndTime] TIME(0) NOT NULL,
	[ProviderId] INT NOT NULL, 
	[StatusId] INT NOT NULL,

	[Notes] NVARCHAR(250) NOT NULL, 
    CONSTRAINT [FK_Sessions_ToStatus] FOREIGN KEY ([StatusId]) REFERENCES [Status]([Id]),
	CONSTRAINT [FK_Sessions_ToProviders] FOREIGN KEY ([ProviderId]) REFERENCES [Providers]([Id]),
	CONSTRAINT [FK_Sessions_ToStudents] FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id])
)
