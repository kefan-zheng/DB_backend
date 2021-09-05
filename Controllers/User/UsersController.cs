using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_USER>>> GetUser()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_USER>().ToListAsync();
        }
        

        // GET: api/Users
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_USER>>> GetUser(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_USER>().Where(it => it.USER_ID == id).ToListAsync();
            if(res==null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Users
        [HttpPost]
        public async Task<ActionResult<LD_USER>> PostUser(LD_USER user)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(()=>db.Insertable(user).ExecuteCommand());
            }
            catch(Exception)
            {
                if(db.Queryable<LD_USER>().Where(it => it.USER_ID == user.USER_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }     
            return CreatedAtAction(nameof(PostUser), new { id = user.USER_ID }, user);
        }

        
        // PUT: api/Users
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, LD_USER user)
        {
            if(id != user.USER_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result =await Task.Run(()=>db.Updateable(user).ExecuteCommand());
            }
            catch (Exception)
            {
                if(!db.Queryable<LD_USER>().Where(it => it.USER_ID == id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/Users
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_USER>().Where(it => it.USER_ID == id).ToListAsync();
            if(res == null)
            {
                return NotFound();
            }
            await Task.Run(()=>db.Deleteable<LD_USER>().In(id).ExecuteCommand());
            return NoContent();
        }

        //忘记密码
        [HttpPatch("{id_vericode_newpwd}")]
        public async Task<IActionResult> PatchUser(string id_vericode_newpwd)
        {
            //字符串分割
            string[] para = id_vericode_newpwd.Split(new char[] { '&' });
            string id = para[0];
            string vericode = para[1];
            string newpwd = para[2];
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                if(vericode == EmailController.FinVeriCode)
                {
                    if (db.Queryable<LD_USER>().Where(it => it.USER_ID == id).Any())
                    {
                        var result = await Task.Run(() => db.Updateable<LD_USER>()
                        .SetColumns(it => it.UPASSWORD == newpwd)
                        .Where(it => it.USER_ID == id)
                        .ExecuteCommand());
                        return Ok("ok");
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_USER>().Where(it => it.USER_ID == id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }
    }
}
