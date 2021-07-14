using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class FunGetVacantRoomTypeByHotelIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetVacantRoomTypeByHotelId(string id)
        {
            SqlSugar cs = new();
            var db = cs.GetInstance();
            var table = db.Queryable<LD_HOTEL, LD_OFFER_ROOM, LD_ROOM, LD_ROOM_TYPE>(
                (h, o, r, rt) => h.HOTEL_ID == o.HOTEL_ID && o.ROOM_ID == r.ROOM_ID && r.TYPE_ID == rt.TYPE_ID)
                 .Select((h, o, r, rt) => new {
                     rt.TYPE_NAME,
                     r.BOOK_STATUS
                 }).MergeTable()
                     .Where(it => it.BOOK_STATUS == "N")
                     .Select(item => new
                     {
                         item.TYPE_NAME
                     })
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