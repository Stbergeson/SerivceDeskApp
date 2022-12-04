namespace ServiceDeskLibrary.Models;

public class RefreshToken
{
    public string Token { get; set; }
    public DateTime Expiration { get; set; }

    public RefreshToken(string token, DateTime expiration)
    {
        Token = token;
        Expiration = expiration;
    }
}
