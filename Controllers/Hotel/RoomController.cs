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
    public class RoomController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_ROOM>>> GetRoom()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_ROOM>().ToListAsync();
        }

        // GET: api/Room
        [HttpGet("{hoteid_roomid}")]
        public async Task<ActionResult<IEnumerable<LD_ROOM>>> GetRoom(string hoteid_roomid)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/Room
        [HttpPost]
        public async Task<ActionResult<LD_ROOM>> PostRoom(LD_ROOM room)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(room).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == room.HOTEL_ID&&it.ROOM_ID == room.ROOM_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetRoom), new { hoteid = room.HOTEL_ID,roomid = room.ROOM_ID }, room);
        }


        // PUT: api/Room
        [HttpPut("{hoteid_roomid}")]
        public async Task<IActionResult> PutRoom(string hoteid_roomid, LD_ROOM room)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            if (hoteid != room.HOTEL_ID || roomid != room.ROOM_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(room).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).Any())
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

        // DELETE: api/Room
        [HttpDelete("{hoteid_roomid}")]
        public async Task<IActionResult> DeleteRoom(string hoteid_roomid)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ExecuteCommand());
            return NoContent();
        }
    }
}
