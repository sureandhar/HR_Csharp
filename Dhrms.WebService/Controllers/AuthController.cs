using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Dhrms.DataAccess;
using Dhrms.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dhrms.WebService.Controllers
{
    

    [Route("api/auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private DhrmsRepository _repository;
        private IConfiguration _configuration;

        
        public AuthController(IConfiguration iconfig)
        {
            _configuration = iconfig;
            _repository = new DhrmsRepository();
        }
        [HttpPost,Route("login")]
        public IActionResult Login([FromBody]Users user)
        {
            if (user == null)
            {
                return BadRequest("Invalid client credentials");
            }
            else
            {
                try
                {
                    string rolename = _repository.validatelogin(user.Email, user.Userpassword);

                    if (rolename!=null && rolename!="-1" && rolename !="-99")
                    {
                        //getting values from the appsetting.json file
                        var _key = _configuration.GetValue<string>("Jwt:Key");
                        var _issuer = _configuration.GetValue<string>("Jwt:Issuer");
                        var _audience = _configuration.GetValue<string>("Jwt:Audience");
                        var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
                        var signingCredentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);
                        //Create a List of Claims, Keep claims name short    
                        var permClaims = new List<Claim>();
                        //permClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                        //permClaims.Add(new Claim("valid", "true"));
                        permClaims.Add(new Claim("role", rolename));
                        permClaims.Add(new Claim("email", user.Email));
                        var tokenOptions = new JwtSecurityToken(
                            issuer: _issuer,
                            audience: _audience,
                            claims: permClaims,
                            expires: DateTime.Now.AddMinutes(30),
                            signingCredentials: signingCredentials
                            );



                        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

                        return Ok(new { Token = tokenString });
                    }
                    else if(rolename.Equals("-1"))
                    {
                        return Unauthorized("Invalid client credentials");
                    }
                    else if(rolename.Equals("-99"))
                    {
                        return BadRequest("Something went wrong try agin after some time");
                    }
                    else
                    {
                        return Unauthorized();
                    }
                    
                    }
                    catch (Exception)
                    {
                    return Unauthorized("Something went wrong try agin after some time");
                }
            }
                
            
            
        }
    }
}
