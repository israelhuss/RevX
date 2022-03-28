CREATE PROCEDURE [dbo].[spWorkplace_GetByUserId]
	@UserId nvarchar(128)
AS
BEGIN
	SELECT w.* FROM Workplace w JOIN [User] u ON u.WorkplaceId = w.Id WHERE u.Id = @UserId
END