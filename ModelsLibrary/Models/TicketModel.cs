namespace ModelsLibrary.Models;

public class TicketModel
{
    public int Id { get; set; }
    public string? CreateId { get; set; }
    public DateTime CreateTimeStamp { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
    public string? AssignedId { get; set; }
    public string? Status { get; set; }
    public bool Archived { get; set; }
    public string? RequesterId { get; set; }
}
