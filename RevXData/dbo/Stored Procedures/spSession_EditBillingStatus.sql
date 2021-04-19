CREATE PROCEDURE [dbo].[spSession_EditBillingStatus]
	@Id int,
	@StatusId int
AS
BEGIN
	UPDATE dbo.[Session]
	SET BillingStatusId = @StatusId
	WHERE Id = @Id;
END
