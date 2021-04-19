CREATE PROCEDURE [dbo].[spStudent_Insert]
	@Id int = 0,
	@FirstName nvarchar(50),
	@LastName nvarchar(50)
AS
BEGIN
	INSERT INTO dbo.Student (FirstName, LastName)
	VALUES (@FirstName, @LastName)
END