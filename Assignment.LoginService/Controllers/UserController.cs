using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Assignment.LoginService.Dtos;
using Assignment.LoginService.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Assignment.LoginService.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<User> Users = new List<User> {
            new User
            {
                 Id=1,
                  Password="1234",
                  UserName="Admin",
                  Role="Admin"
            },
            new User
            {
                 Id=1,
                  Password="1234",
                  UserName="Test",
                  Role="Non-Admin"
            }
        };

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginRequestDto loginRequest)
        {
            var user = Users.FirstOrDefault(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);
            if (user == null)
            {
                return BadRequest();
            }
            var token = generateJSONWebToken(user);
            return Ok(new ResponseModel { Data = token, Message = "Success" });
        }

        private string generateJSONWebToken(User user)
        {
            var claims = new[] {
                                 new Claim("UserId", user.Id.ToString()),
                                 new Claim(ClaimTypes.Role, user.Role),
                               };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token;
            token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
               _configuration["Jwt:Issuer"],
               claims,
               expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:JwtTokenExpiringTime"])),
               signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}