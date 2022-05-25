CREATE TABLE [dbo].[ReportItemDetail]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [ReportItemId] INT NOT NULL,
    [Field] NVARCHAR(50) NOT NULL, 
    [Value] NVARCHAR(200) NOT NULL, 
    [Operator] INT NOT NULL, 
    [Level] INT NOT NULL, 
    CONSTRAINT [FK_ReportItemDetail_ToReportItem] FOREIGN KEY ([ReportItemId]) REFERENCES [ReportItem]([Id])  ON DELETE CASCADE
)
