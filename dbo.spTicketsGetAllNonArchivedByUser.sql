CREATE PROCEDURE [dbo].[spTickets_GetAllNonArchivedByUser]
	@UserId nchar(36)
AS
begin
	select [Id], [Subject], [AssignedId], [Status], [RequesterId], [CreateTimeStamp]
	from dbo.Tickets
	where [Archived] = 0
	and [AssignedId] = @UserId
	order by [CreateTimeStamp] desc;
end