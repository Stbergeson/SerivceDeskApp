CREATE PROCEDURE [dbo].[spTickets_GetOne]
	@TicketId int
AS
begin
	select *
	from dbo.Tickets
	where Tickets.Id = @TicketId;
end