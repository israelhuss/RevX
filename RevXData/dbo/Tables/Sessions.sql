CREATE TABLE [dbo].[Sessions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[Date] DATE NOT NULL, 
	[TimeIn] TIME(0) NOT NULL, 
	[TimeOut] TIME(0) NOT NULL, 
	[StatusId] INT NOT NULL, 
	[ProviderId] INT NOT NULL, 
	[StudentId] INT NOT NULL, 
	CONSTRAINT [FK_Sessions_ToStatus] FOREIGN KEY ([StatusId]) REFERENCES [Status]([Id]),
	CONSTRAINT [FK_Sessions_ToProviders] FOREIGN KEY ([ProviderId]) REFERENCES [Providers]([Id]),
	CONSTRAINT [FK_Sessions_ToStudents] FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id])
)
