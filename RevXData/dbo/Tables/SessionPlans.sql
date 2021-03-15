CREATE TABLE [dbo].[SessionPlans]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[StudentId] INT NOT NULL, 
	[DayOfWeek] TINYINT NOT NULL CHECK (DayOfWeek BETWEEN 1 AND 7), 
	[StartTime] TIME(0) NOT NULL, 
	[EndTime] TIME(0) NOT NULL, 
	CONSTRAINT [FK_SessionPlans_Student] FOREIGN KEY ([StudentId]) REFERENCES [Students]([Id])
)
