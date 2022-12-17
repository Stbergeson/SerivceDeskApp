using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public class Ticket
{
    public int Id { get; set; }
    [MaxLength(36)]
    public string CreateId { get; set; }
    public DateTime CreateTimestamp { get; set; }
    [MaxLength(128)]
    public string Subject { get; set; }
    [MaxLength(4000)]
    public string Body { get; set; }
    public int AssignedId { get; set; }
    [MaxLength(32)]
    public string Status { get; set; }
    public bool Archived { get; set; }
    public int RequesterId { get; set; }
}
