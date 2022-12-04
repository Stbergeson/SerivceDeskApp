using ServiceDeskLibrary.Models;

namespace ServiceDeskLibrary.DataAccess
{
    public interface IUserData
    {
        //Return value of password of supplied user
        Task<string> GetUserPasswordHash(string user);
        //Update db to have the latest refresh token and expiration date for the supplied user
        Task AddRefreshToken(RefreshToken refreshToken, string user);
    }
}