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
    public class OfferRoomController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_OFFER_ROOM>>> GetOfferRoom()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_OFFER_ROOM>().ToListAsync();
        }

        // GET: api/OfferRoom
        [HttpGet("{hoteid_roomid}")]
        public async Task<ActionResult<IEnumerable<LD_OFFER_ROOM>>> GetOfferRoom(string hoteid_roomid)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_OFFER_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/OfferRoom
        [HttpPost]
        public async Task<ActionResult<LD_OFFER_ROOM>> PostOfferRoom(LD_OFFER_ROOM offerroom)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(offerroom).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_OFFER_ROOM>().Where(it => it.HOTEL_ID == offerroom.HOTEL_ID&&it.ROOM_ID == offerroom.ROOM_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetOfferRoom), new { hoteid = offerroom.HOTEL_ID,roomid= offerroom.ROOM_ID }, offerroom);
        }


        // PUT: api/OfferRoom
        [HttpPut("{hoteid_roomid}")]
        public async Task<IActionResult> PutOfferRoom(string hoteid_roomid, LD_OFFER_ROOM offerroom)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            if (para[0] != offerroom.HOTEL_ID || para[1] != offerroom.ROOM_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(offerroom).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_OFFER_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).Any())
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

        // DELETE: api/OfferRoom
        [HttpDelete("{hoteid_roomid}")]
        public async Task<IActionResult> DeleteOfferRoom(string hoteid_roomid)
        {
            //处理字符串
            string[] para = hoteid_roomid.Split(new char[] { '&' });
            string hoteid = para[0];
            string roomid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_OFFER_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_OFFER_ROOM>().Where(it => it.HOTEL_ID == hoteid&&it.ROOM_ID == roomid).ExecuteCommand());
            return NoContent();
        }
    }
}
