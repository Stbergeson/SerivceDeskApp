using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceDeskApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
	private readonly IConfiguration _config;

	public AuthenticationController(IConfiguration config)
	{
		_config = config;
	}

	public record AuthenticationData(string? UserName, string? Password);
	public record UserData(string Id, string UserName, string FirstName, string LastName);

	[HttpPost("token")]
	[AllowAnonymous]
	public ActionResult<string> Authenticate([FromBodyAttribute] AuthenticationData data)
	{
		var user = ValidateCredentials(data);

		if (user == null)
			return Unauthorized();

		var token = GenerateToken(user);

		return Ok(token);
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

    private UserData? ValidateCredentials(AuthenticationData data)
    {
        //DO NOT USE THIS CODE IN PRODUCTION - REPLACE WITH CALL TO AUTH SYSTEM
        if (CompareValues(data.UserName, "sclaus") &&
           CompareValues(data.Password, "password"))
        {
            return new UserData("1", "Santa", "Claus", data.UserName!);
        }

        if (CompareValues(data.UserName, "gwashington") &&
           CompareValues(data.Password, "password"))
        {
            return new UserData("2", "George", "Washington", data.UserName!);
        }

        return null;

    }

    private bool CompareValues(string? actual, string expected)
    {
        if (actual != null)
        {
            if (actual.Equals(expected))
            {
                return true;
            }
        }
        return false;
    }
}
