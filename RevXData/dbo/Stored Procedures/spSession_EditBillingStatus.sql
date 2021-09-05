CREATE PROCEDURE [dbo].[spSession_EditBillingStatus]
	@Id int,
	@UserId nvarchar(128),
	@StatusId int
AS
BEGIN
	UPDATE dbo.[Session]
	SET BillingStatusId = @StatusId
	WHERE Id = @Id AND UserId = @UserId;
END
