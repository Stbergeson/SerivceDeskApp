using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NuGet.Protocol.Plugins;

namespace ServiceDeskClient.Shared
{
    public class RefreshModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IHttpClientFactory _factory;
        public record AuthenticationData(string? Username, string? Password);
        public record Tokens(string BearerToken, string RefreshToken);

        public RefreshModel(SignInManager<IdentityUser> signInManager, IHttpClientFactory factory)
        {
            _signInManager = signInManager;
            _factory = factory;
        }

        public async Task Refresh()
        {
            //set cookie options
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddMinutes(1);
            cookieOptions.Secure = true;

            //grab api token for user
            var client = _factory.CreateClient("api");
            AuthenticationData login = new("username", "refreshtoken");
            //get new bearer and refresh token
            var info = await client.PostAsJsonAsync<AuthenticationData>("Authentication/token/refresh", login);
            string token = await info.Content.ReadAsStringAsync();
            Tokens tokens = JsonConvert.DeserializeObject<Tokens>(token);

            Response.Cookies.Append("ApiBearerToken", tokens.BearerToken, cookieOptions);

            cookieOptions.Expires = DateTime.Now.AddDays(7);
            Response.Cookies.Append("ApiRefreshToken", tokens.RefreshToken, cookieOptions);
        }
    }
}
