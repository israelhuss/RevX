﻿CREATE TABLE [dbo].[SessionPlan]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL DEFAULT '%REPLACE_ME%',
	[StudentId] INT NOT NULL, 
	[DayOfWeek] TINYINT NOT NULL CHECK (DayOfWeek BETWEEN 1 AND 7), 
	[StartTime] TIME(0) NOT NULL, 
	[EndTime] TIME(0) NOT NULL, 
	CONSTRAINT [FK_SessionPlans_Student] FOREIGN KEY ([StudentId]) REFERENCES [Student]([Id]),
	CONSTRAINT [FK_SessionPlan_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
