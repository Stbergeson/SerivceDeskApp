using ModelsLibrary.Models;

namespace ServiceDeskLibrary.DataAccess
{
    public interface ITicketData
    {
        Task ArchiveTicket(bool archived, int ticketId);
        Task Create(string subject, string body, string assignedId, string status, string requesterId, string createId);
        Task DeleteTicket(int ticketId);
        Task<List<TicketModel>> GetAllArchived();
        Task<List<TicketModel>> GetAllNonArchived();
        Task<List<TicketModel>> GetAllNonArchivedByUser(string userId);
        Task<TicketModel?> GetOne(int ticketId);
        Task UpdateTicket(string subject, string body, string assignedId, string status, string requesterId, int ticketId);
    }
}