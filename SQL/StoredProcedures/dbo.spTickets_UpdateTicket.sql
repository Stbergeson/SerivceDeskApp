CREATE PROCEDURE [dbo].[spTickets_UpdateTicket]
	@Subject nvarchar(128),
	@Body nvarchar(4000),
	@AssignedId nchar(36),
	@Status nchar(32),
	@RequesterId nchar(36),
	@TicketId int
AS
begin
	update dbo.Tickets
	set [Subject] = @Subject,
		[Body] = @Body,
		[AssignedId] = @AssignedId,
		[Status] = @Status,
		[RequesterId] = @RequesterId
	where [Id] = @TicketId
end