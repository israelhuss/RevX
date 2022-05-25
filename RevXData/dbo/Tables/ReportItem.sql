CREATE TABLE [dbo].[ReportItem]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ReportId] INT NOT NULL, 
    [ViewAs] INT NOT NULL, 
    [Color] NVARCHAR(50) NULL, 
    [Nickname] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_ReportItem_ToReport] FOREIGN KEY ([ReportId]) REFERENCES [Report]([Id]) ON DELETE CASCADE
)
