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
        /// 正常登录获取Token
        [HttpGet]
        [Route("token")]
        public ActionResult Login(string role, string id, string password, string type)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var Isexisted = false;
            if(type == "logbypwd")
            {
                if (role == "user")
                {
                    if (Isexisted = db.Queryable<LD_USER>().Where(it => it.USER_ID == id && it.UPASSWORD == password).Any())
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, id),
                            new Claim(ClaimTypes.Role, ""),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
                        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                        return Ok(token);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (role == "administrator")
                {
                    if (Isexisted = db.Queryable<LD_ADMINISTRATOR>().Where(it => it.ADMINISTRATOR_ID == id && it.PASSWORD == password).Any())
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, id),
                            new Claim(ClaimTypes.Role, ""),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
                        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                        return Ok(token);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else if (role == "hotel")
                {
                    if (Isexisted = db.Queryable<LD_HOTEL>().Where(it => it.HOTEL_ID == id && it.HPASSWORD == password).Any())
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, id),
                            new Claim(ClaimTypes.Role, ""),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
                        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                        return Ok(token);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            else if(type == "logbyvericode")
            {
                if(password == EmailController.LogVeriCode)
                {
                    if (db.Queryable<LD_USER>().Where(it => it.USER_ID == id).Any() || db.Queryable<LD_USER>().Where(it => it.MAILBOX_ID == id).Any())
                    {
                        var claims = new[]
                        {
                            new Claim(ClaimTypes.Name, id),
                            new Claim(ClaimTypes.Role, ""),
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenParameter.Secret));
                        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var jwtToken = new JwtSecurityToken(TokenParameter.Issuer, TokenParameter.Audience, claims, expires: DateTime.UtcNow.AddMinutes(TokenParameter.AccessExpiration), signingCredentials: credentials);
                        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

                        return Ok(token);
                    }
                    else
                    {
                        return NoContent();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            return NoContent();
        }

        //通过验证码进行注册
        [HttpPost("{uservericode}")]
        public async Task<ActionResult<LD_USER>> PostUser(LD_USER user,string uservericode)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            if(uservericode == EmailController.RegVeriCode)
            {
                if (db.Queryable<LD_USER>().Where(it => it.USER_ID == user.USER_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    await Task.Run(() => db.Insertable(user).ExecuteCommand());
                    return CreatedAtAction(nameof(PostUser), new { id = user.USER_ID }, user);
                }
            }
            else
            {
                return NoContent();
            }               
        }

    }
}
