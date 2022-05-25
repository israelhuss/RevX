CREATE TABLE [dbo].[Report]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] NVARCHAR(128) NOT NULL ,
    [Title] NVARCHAR(50) NULL, 
    [ReportStyle] INT NOT NULL, 
    [StartDate] DATE NOT NULL, 
    [EndDate] DATE NOT NULL, 
    [GroupBy] INT NOT NULL DEFAULT 1, 
    [IsDefault] BIT NULL
)
