using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SiteManagement.Data;
using SiteManagement.Data.Entities;
using SiteManagement.Schema;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace SiteManagement.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {

        private readonly SiteDbContext dbContext;

        public TokenController(SiteDbContext dbContext)
        {

            this.dbContext = dbContext;


        }
        [HttpPost]
        public IActionResult Login(LoginRequest request)
        {
            //token creation and login process

            User user = dbContext.Users.Where(x => x.TcNo == request.TcNo && x.Password == request.password).SingleOrDefault();

            if (user != null)
            {
                List<Claim> claims = new List<Claim>();
                //claims.Add(new Claim("TcNo", request.TcNo));
                claims.Add(new Claim(ClaimTypes.Name,user.Name));
                claims.Add(new Claim(ClaimTypes.Surname,user.Surname));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.TcNo));

                SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Convert.FromBase64String
                    ("managementmanagementmanagementma"));
                SigningCredentials credentials = new SigningCredentials(securityKey, 
                    SecurityAlgorithms.HmacSha256);

                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                    issuer: "localhost",
                    audience: "localhost",
                    signingCredentials: credentials,
                    claims: claims,
                    expires: DateTime.Now.AddDays(10)
                  );
                
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                string userToken=tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(userToken);
                    

             }
            else
            { return BadRequest("You entered an incorrect tcno or password"); }
        }
    }
}
