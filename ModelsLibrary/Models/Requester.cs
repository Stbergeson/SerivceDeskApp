using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public class Requester
{
    public int Id { get; set; }
    [MaxLength(32)]
    public string FirstName { get; set; }
    [MaxLength(32)]
    public string LastName { get; set; }
}
