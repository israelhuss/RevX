CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Id int = 0,
	@Name varchar(20)
AS
BEGIN
	INSERT INTO dbo.[Provider] ([Name])
	VALUES (@Name)
END
