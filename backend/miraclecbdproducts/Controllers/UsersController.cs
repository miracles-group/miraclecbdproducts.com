using ICSharpCode.SharpZipLib.Zip;
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
                        Expires = DateTime.UtcNow.AddDays(7000),
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
        [DisableCors]
        [AllowAnonymous]
        [HttpPost("register")]
        public ResponseModel InsertUser([FromBody]UserDto _userDto)
        {
            var rs = new ResponseModel()
            {
                Status = 200,
                Message = "Company account was created."
            };
            try
            {
                using (var db = new MiraclesContext())
                {
                   
                        db.TblUser.Add(new TblUser
                        {
                          
                            UserName = _userDto.Username,
                            Password = _userDto.Password,
                        });
                  
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                rs.Status = 500;
                rs.Message = "error: " + ex.Message;
            }

            return rs;

        }
        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
