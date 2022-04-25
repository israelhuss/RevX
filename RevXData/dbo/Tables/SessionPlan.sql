CREATE TABLE [dbo].[SessionPlan]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL,
	[ClientId] INT NOT NULL, 
	[ProviderId] INT NOT NULL, 
	[DayOfWeek] TINYINT NOT NULL CHECK (DayOfWeek BETWEEN 0 AND 6), 
	[DateFrom] DATE NOT NULL, 
    [DateTo] DATE NOT NULL, 
	[StartTime] TIME(0) NOT NULL, 
	[EndTime] TIME(0) NOT NULL, 
    CONSTRAINT [FK_SessionPlans_Provider] FOREIGN KEY ([ProviderId]) REFERENCES [Provider]([Id]),
	CONSTRAINT [FK_SessionPlans_Client] FOREIGN KEY ([ClientId]) REFERENCES [Client]([Id]),
	CONSTRAINT [FK_SessionPlan_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
