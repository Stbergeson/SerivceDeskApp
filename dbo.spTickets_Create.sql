CREATE PROCEDURE [dbo].[spTickets_Create]
	@Subject nvarchar(128),
	@Body nvarchar(4000),
	@AssignedId nchar(36),
	@RequesterId nchar(36),
	@CreateId nchar(36)
AS
begin
	insert into dbo.Tickets ([Subject], [Body], [AssignedId], [Status], [RequesterId], [CreateId], [Archived], [CreateTimeStamp])
	values (@Subject, @Body, @AssignedId, 'Open', @RequesterId, @CreateId, 0, GETDATE());
end