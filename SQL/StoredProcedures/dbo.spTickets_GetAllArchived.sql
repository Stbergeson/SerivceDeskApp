CREATE PROCEDURE [dbo].[spTickets_GetAllArchived]
AS
begin
	select [Id], [Subject], [AssignedId], [Status], [RequesterId], [CreateTimeStamp]
	from dbo.Tickets
	where [Archived] = 1
	order by [CreateTimeStamp] desc;
end