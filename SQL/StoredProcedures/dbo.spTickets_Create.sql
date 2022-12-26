CREATE PROCEDURE [dbo].[spTickets_Create]
	@Subject nvarchar(128),
	@Body nvarchar(4000),
	@AssignedId int,
	@RequesterId int,
	@CreateId nvarchar(36),
	@Status nvarchar(32)
AS
begin
	insert into dbo.Tickets ([Subject], [Body], [AssignedId], [Status], [RequesterId], [CreateId], [Archived], [CreateTimeStamp])
	values (@Subject, @Body, @AssignedId, @Status, @RequesterId, @CreateId, 0, GETDATE());
end