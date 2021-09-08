using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    //[Authorize]
    public class FunChangeStatusController : ControllerBase
    {
        [HttpPut("{hotelid_roomid}")]
        public async Task<IActionResult> FunChangeStatus(string hotelid_roomid, LD_ROOM room)
        {
            string[] para = hotelid_roomid.Split(new char[] { '&' });
            string hotelid = para[0];
            string roomid = para[1];
            if (hotelid != room.HOTEL_ID || roomid != room.ROOM_ID)
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
                if (!db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == hotelid&&it.ROOM_ID == roomid).Any())
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
    }
}
