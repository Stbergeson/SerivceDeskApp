using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServiceDeskLibrary.DataAccess;
using ServiceDeskLibrary.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceDeskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IConfiguration _config;
    private readonly IUserData _data;

    public AuthenticationController(IConfiguration config, IUserData data)
	{
		_config = config;
        _data = data;
    }

	public record AuthenticationData(string? UserName, string? Password);
	public record UserData(string Id, string UserName, string FirstName, string LastName);
    public record Tokens(string BearerToken, string RefreshToken);

	[HttpPost("token")]
	[AllowAnonymous]
	public ActionResult<Tokens> Authenticate([FromBodyAttribute] AuthenticationData data)
	{
		var user = ValidateCredentials(data);

		if (user == null)
			return Unauthorized();

		var token = GenerateToken(user);
        Tokens tokens = new(GenerateToken(user), GenerateRefreshToken(user));
		return Ok(tokens);
	}

	private string GenerateToken(UserData user)
	{
		var secretKey = new SymmetricSecurityKey(
			Encoding.ASCII.GetBytes(
				_config.GetValue<string>("Authentication:SecretKey")));

		var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

		List<Claim> claims = new();
        claims.Add(new(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
        claims.Add(new(JwtRegisteredClaimNames.UniqueName, user.UserName));
        claims.Add(new(JwtRegisteredClaimNames.GivenName, user.FirstName));
        claims.Add(new(JwtRegisteredClaimNames.FamilyName, user.LastName));

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.UtcNow,
            DateTime.UtcNow.AddMinutes(1),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private string GenerateRefreshToken(UserData user)
    {
        RefreshToken token = new(Guid.NewGuid().ToString(), DateTime.Now.AddDays(7));
        _data.AddRefreshToken(token, user.UserName);
        return token.Token;
    }

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        IPasswordHasher<object> _hasher;
        _hasher = new PasswordHasher<object>();
        string hashedPass = _data.GetUserPasswordHash(data.UserName!).Result;
        if (hashedPass != null)
        {
            var passResult = _hasher.VerifyHashedPassword(new { }, hashedPass, data.Password);

            if (passResult == PasswordVerificationResult.Success)
            {
                return new UserData("", data.UserName!, "", "");
            }
        }

        return null;

    }

}
