CREATE PROCEDURE [dbo].[spTickets_DeleteTicket]
	@TicketId int
AS
begin
	delete from dbo.Tickets
	where [Id] = @TicketId
		and [Archived] = 1;
end