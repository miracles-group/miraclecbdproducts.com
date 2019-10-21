using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MiraclecBDProducts.Dto;
using MiraclecBDProducts.Helpers;
using MiraclecBDProducts.Models;
using MiraclecBDProducts.Services;
using ShopifySharp;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiraclecBDProducts.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[Controller]")]
    [EnableCors("AllowAllHeaders")]
    public class UsersController : ControllerBase
    {
        
       
        public UsersController()
        { 
        }
        [DisableCors]
        [AllowAnonymous]
        [HttpPost("authenticate")]
        public ResponseModel AuthenticateAsync([FromBody]UserDto userDto)
        {
            using(var db = new MiraclesContext())
            {
                var user =db.TblUser.Where(o => o.UserName == userDto.Username && o.Password == userDto.Password).FirstOrDefault();
                if(user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserName.ToString())
                }),
                        Expires = DateTime.UtcNow.AddDays(7),
                    };
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);
                    user.Token = tokenString;
                    db.SaveChanges();
                    return new ResponseModel()
                    {
                        Status = 200,
                        Data = tokenString,
                        Message = "Login successfully"
                    };
                }
            }
            return new ResponseModel()
            {
                Status = 400,
                Message = "Login failed"
            };      
        }
    }
}
