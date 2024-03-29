﻿CREATE TABLE [dbo].[Student]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[UserId] NVARCHAR(128) NOT NULL DEFAULT '%REPLACE_ME%',
	[FirstName] NVARCHAR(50) NOT NULL, 
	[LastName] NVARCHAR(50) NOT NULL, 
	[Enabled] BIT NOT NULL DEFAULT 1,
	CONSTRAINT [FK_Student_ToUser] FOREIGN KEY ([UserId]) REFERENCES [User]([Id])
)
