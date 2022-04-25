CREATE TABLE [dbo].[PendingSession]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL, 
    [Date] DATE NOT NULL, 
    [PlanId] INT NOT NULL, 
    CONSTRAINT [FK_PendingSession_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id]),
    CONSTRAINT [FK_PendingSession_ToSessionPlan] FOREIGN KEY ([PlanId]) REFERENCES [SessionPlan]([Id])
)
