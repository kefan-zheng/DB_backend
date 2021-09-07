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
    public class VacantRoomController : ControllerBase
    {
        [HttpGet("{hotelid_roomtypeid}")]
        public List<dynamic> VacantRoom(string hotelid_roomtypeid)
        {
            SqlSugar cs = new();
            var db = cs.GetInstance();
            string[] para = hotelid_roomtypeid.Split(new char[] { '&' });
            string hotelid = para[0];
            string roomtypeid = para[1];
            var table = db.Queryable<LD_ROOM>()
                     .Where(it => it.HOTEL_ID == hotelid && it.TYPE_ID == roomtypeid&&it.BOOK_STATUS=="N")
                     .Select(item => new
                     {
                         item.HOTEL_ID,
                         item.ROOM_ID,
                         item.TYPE_ID,
                         item.PRICE
                     })
                     .MergeTable()
                     .Distinct()
                     .ToList();

            List<dynamic> res = new();
            for (int i = 0; i < table.Count; i++)
            {
                res.Add(table[i]);
            }
            return res;
        }
    }
}
