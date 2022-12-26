using ModelsLibrary.Models;

namespace ServiceDeskLibrary.DataAccess
{
    public interface ITicketData
    {
        Task ArchiveTicket(bool archived, int ticketId);
        Task Create(string subject, string body, int assignedId, int requesterId, string createId, string status);
        Task DeleteTicket(int ticketId);
        Task<List<Ticket>> GetAllArchived();
        Task<List<Ticket>> GetAllNonArchived();
        Task<List<Ticket>> GetAllNonArchivedByUser(string userId);
        Task<Ticket> GetOne(int ticketId);
        Task UpdateTicket(string subject, string body, int assignedId, string status, int requesterId, int ticketId);
    }
}