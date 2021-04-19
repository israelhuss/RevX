CREATE TABLE [dbo].[Invoice]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [InvoiceDate] DATETIME2 NOT NULL, 
    [TotalHours] DECIMAL(4, 2) NOT NULL 
)
