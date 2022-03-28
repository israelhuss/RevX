﻿CREATE PROCEDURE [dbo].[spUser_GetById]
	@Id nvarchar(128)
AS
BEGIN
	SELECT [Id], [FirstName], [LastName], [EmailAddress], [CreatedDate], [WorkplaceId], [BillingCycleId]
	FROM [dbo].[User]
	WHERE Id = @Id;
END