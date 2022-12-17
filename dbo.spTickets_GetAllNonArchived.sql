CREATE PROCEDURE [dbo].[spTickets_GetAllNonArchived]
AS
begin
	select [Id], [Subject], [AssignedId], [Status], [RequesterId], [CreateTimeStamp]
	from dbo.Tickets
	where Archived = 0
	order by [CreateTimeStamp] desc;
end