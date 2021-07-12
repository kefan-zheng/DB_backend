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

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_USER>>> GetUser()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_USER>().ToListAsync();
        }
       
        // GET: api/Users/5
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
            return CreatedAtAction(nameof(GetUser), new { id = user.USER_ID }, user);
        }

        
        // PUT: api/Users/5
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

        // DELETE: api/Users/5
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
    }
}
