using ModelsLibrary.Models;

namespace ServiceDeskLibrary.DataAccess;

public class TicketData : ITicketData
{
    private readonly ISqlDataAccess _sql;

    public TicketData(ISqlDataAccess sql)
    {
        _sql = sql;
    }

    #region GET

    public Task<List<TicketModel>> GetAllArchived()
    {
        return _sql.LoadData<TicketModel, dynamic>(
            "dbo.spTickets_GetAllArchived",
            new { },
            "Default");
    }

    public Task<List<TicketModel>> GetAllNonArchived()
    {
        return _sql.LoadData<TicketModel, dynamic>(
            "dbo.spTickets_GetAllNonArchived",
            new { },
            "Default");
    }

    public Task<List<TicketModel>> GetAllNonArchivedByUser(string userId)
    {
        return _sql.LoadData<TicketModel, dynamic>(
            "dbo.spTickets_GetAllNonArchivedByUser",
            new { UserId = userId },
            "Default");
    }

    public async Task<TicketModel?> GetOne(int ticketId)
    {
        var results = await _sql.LoadData<TicketModel, dynamic>(
            "dbo.spTickets_GetOne",
            new { TicketId = ticketId },
            "Default");

        return results.FirstOrDefault();
    }

    #endregion

    #region POST
    public Task Create(string subject, string body, string assignedId,
        string status, string requesterId, string createId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTickets_Create",
            new
            {
                Subject = subject,
                Body = body,
                AssignedId = assignedId,
                Status = status,
                RequesterId = requesterId,
                CreateId = createId
            },
            "Default");
    }

    #endregion

    #region PUT
    public Task UpdateTicket(string subject, string body, string assignedId,
        string status, string requesterId, int ticketId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTickets_UpdateTicket",
            new
            {
                Subject = subject,
                Body = body,
                AssignedId = assignedId,
                Status = status,
                RequesterId = requesterId,
                TicketId = ticketId
            },
            "Default");
    }

    public Task ArchiveTicket(bool archived, int ticketId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTickets_ArchiveTicket",
            new { Archived = archived, TicketId = ticketId },
            "Default");
    }
    #endregion

    #region DELETE
    public Task DeleteTicket(int ticketId)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spTickets_DeleteTicket",
            new { TicketId = ticketId },
            "Default");
    }
    #endregion
}
