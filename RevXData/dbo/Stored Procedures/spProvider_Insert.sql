CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Id int = 0,
	@Name varchar(20)
AS
BEGIN
	INSERT INTO dbo.Providers (Name)
	VALUES (@Name)
END
