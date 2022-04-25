CREATE PROCEDURE [dbo].[spSession_Insert]
	@Id int = 0 output,
	@UserId nvarchar(128),
	@ClientId int,
	@Date date,
	@StartTime time(0),
	@EndTime time(0),
	@ProviderId int,
	@BillingStatusId int,
	@Notes nvarchar(250),
	@InvoiceId int = 0
AS

IF EXISTS(SELECT Id FROM dbo.Session 
						WHERE @ClientId = ClientId
						AND @UserId = UserId
						AND @Date = [Date]
						AND @StartTime = StartTime
						AND @EndTime = EndTime)
	SELECT -100
ELSE IF EXISTS(SELECT Id FROM dbo.Session 
						WHERE @ClientId = ClientId
						AND @UserId = UserId
						AND @Date = [Date]
						AND ((@StartTime >= StartTime AND @StartTime < EndTime) OR (@EndTime <= EndTime AND @EndTime > StartTime)))
	SELECT -128
ELSE
	BEGIN
		INSERT INTO dbo.Session (UserId, ClientId, [Date], StartTime, EndTime, ProviderId, BillingStatusId, Notes)
		VALUES (@UserId, @ClientId, @Date, @StartTime, @EndTime, @ProviderId, @BillingStatusId, @Notes)
		SELECT SCOPE_IDENTITY();
	END