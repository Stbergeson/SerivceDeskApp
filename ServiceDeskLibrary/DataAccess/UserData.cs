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
}
