using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public class TicketNote
{
    public int Id { get; set; }
    public int TicketId { get; set; }
    public DateTime CreateTimestamp { get; set; }
    [MaxLength(36)]
    public string CreateId { get; set; }
    [MaxLength(512)]
    public string Body { get; set; }
}
