CREATE PROCEDURE [dbo].[spTickets_ArchiveTicket]
	@Archived bit,
	@TicketId int
AS
begin
	update dbo.Tickets
	set [Archived] = @Archived
	where [Id] = @TicketId
end