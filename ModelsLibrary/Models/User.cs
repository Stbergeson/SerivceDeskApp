using System.ComponentModel.DataAnnotations;

namespace ModelsLibrary.Models;

public class User
{
    public int Id { get; set; }
    [MaxLength(32)]
    public string FirstName { get; set; }
    [MaxLength(32)]
    public string LastName { get; set; }
    public bool IsTech { get; set; }

    public string Name()
    {
        return FirstName + LastName;
    }
}
