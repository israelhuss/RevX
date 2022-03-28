CREATE TABLE [dbo].[Workplace]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NULL, 
    [EmailAddress] NVARCHAR(250) NULL, 
    [ClientNickname] NVARCHAR(50) NULL, 
    [RequireSignature] BIT NOT NULL DEFAULT 1
)
