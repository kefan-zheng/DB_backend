using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using LvDao.Tools;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [Route("api/oauth")]
    [ApiController]
    public class OAuthController:ControllerBase
    {
        /// 获取Token
        [HttpGet]
        [Route("token")]
        public ActionResult GetAccessToken(string username, string password)
        {           
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, ""),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
            var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return Ok(token);
        }
    }
}
