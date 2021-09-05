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
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class RoomTypeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_ROOM_TYPE>>> GetRoomType()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_ROOM_TYPE>().ToListAsync();
        }

        // GET: api/RoomType
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<LD_ROOM_TYPE>>> GetRoomType(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ROOM_TYPE>().Where(it => it.TYPE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/RoomType
        [HttpPost]
        public async Task<ActionResult<LD_ROOM_TYPE>> PostRoomType(LD_ROOM_TYPE roomtype)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(roomtype).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_ROOM_TYPE>().Where(it => it.TYPE_ID == roomtype.TYPE_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetRoomType), new { id = roomtype.TYPE_ID }, roomtype);
        }


        // PUT: api/RoomType
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoomType(string id, LD_ROOM_TYPE roomtype)
        {
            if (id != roomtype.TYPE_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(roomtype).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_ROOM_TYPE>().Where(it => it.TYPE_ID == id).Any())
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

        // DELETE: api/RoomType
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteType(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ROOM_TYPE>().Where(it => it.TYPE_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_ROOM_TYPE>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
