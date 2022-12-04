using ServiceDeskLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceDeskLibrary.DataAccess;

public class UserData : IUserData
{
    private readonly ISqlDataAccess _sql;
    public UserData(ISqlDataAccess sql)
    {
        _sql = sql;
    }
    public async Task<string> GetUserPasswordHash(string user)
    {
        var results = await _sql.LoadData<string, dynamic>(
            "dbo.spUsers_GetPasswordHash",
            new { userName = user },
            "Default");

        return results.FirstOrDefault()!;
    }

    public Task AddRefreshToken(RefreshToken token, string user)
    {
        return _sql.SaveData<dynamic>(
            "dbo.spUsers_PostRefreshToken",
            new { token = token.Token, expiration = token.Expiration, userName = user },
            "Default");
    }
}
