CREATE PROCEDURE [dbo].[spWorkplace_getAll]
	@WorkplaceId int
AS
BEGIN
	SELECT * FROM Workplace WHERE Id = @WorkplaceId
END