using ModelsLibrary.Models;

namespace ServiceDeskLibrary.DataAccess
{
    public interface ITicketData
    {
        Task ArchiveTicket(bool archived, int ticketId);
        Task Create(string subject, string body, string assignedId, string requesterId, string createId);
        Task DeleteTicket(int ticketId);
        Task<List<Ticket>> GetAllArchived();
        Task<List<Ticket>> GetAllNonArchived();
        Task<List<Ticket>> GetAllNonArchivedByUser(string userId);
        Task<Ticket> GetOne(int ticketId);
        Task UpdateTicket(string subject, string body, string assignedId, string status, string requesterId, int ticketId);
    }
}