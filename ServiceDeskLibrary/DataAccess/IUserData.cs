namespace ServiceDeskLibrary.DataAccess
{
    public interface IUserData
    {
        Task<string> GetUserPasswordHash(string user);
    }
}