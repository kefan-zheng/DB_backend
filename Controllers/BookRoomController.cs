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

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class BookRoomController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_BOOK_ROOM>>> GetBookRoom()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_BOOK_ROOM>().ToListAsync();
        }

        // GET: api/BookRoom
        [HttpGet("{hotid_romid}")]
        public async Task<ActionResult<IEnumerable<LD_BOOK_ROOM>>> GetBookRoom(string hotid_romid)
        {
            //处理字符串
            string[] para = hotid_romid.Split(new char[] { '&' });
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_BOOK_ROOM>().Where(it => it.HOTEL_ID == para[0]&&it.ROOM_ID == para[1]).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/BookRoom
        [HttpPost]
        public async Task<ActionResult<LD_BOOK_ROOM>> PostBookRoom(LD_BOOK_ROOM bookroom)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(bookroom).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_BOOK_ROOM>().Where(it => it.HOTEL_ID == bookroom.HOTEL_ID&&it.ROOM_ID == bookroom.ROOM_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetBookRoom), new { hotid = bookroom.HOTEL_ID,romid=bookroom.ROOM_ID }, bookroom);
        }


        // PUT: api/BookRoom
        [HttpPut("{hotid_romid}")]
        public async Task<IActionResult> PutBookRoom(string hotid_romid, LD_BOOK_ROOM bookroom)
        {
            //处理字符串
            string[] para = hotid_romid.Split(new char[] { '&' });
            if (para[0] != bookroom.HOTEL_ID&&para[1] != bookroom.ROOM_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(bookroom).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_BOOK_ROOM>().Where(it => it.HOTEL_ID == para[0]&&it.ROOM_ID == para[1]).Any())
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

        // DELETE: api/BookRoom
        [HttpDelete("{hotid_romid}")]
        public async Task<IActionResult> DeleteBookRoom(string hotid_romid)
        {
            //处理字符串
            string[] para = hotid_romid.Split(new char[] { '&' });
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_BOOK_ROOM>().Where(it => it.HOTEL_ID == para[0]&&it.ROOM_ID == para[1]).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_BOOK_ROOM>().Where(it => it.HOTEL_ID == para[0]&&it.ROOM_ID == para[1]).ExecuteCommand());
            return NoContent();
        }
    }
}
