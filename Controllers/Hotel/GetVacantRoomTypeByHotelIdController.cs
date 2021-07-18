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
    [Authorize]
    public class FunGetVacantRoomTypeByHotelIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetVacantRoomTypeByHotelId(string id)
        {
            SqlSugar cs = new();
            var db = cs.GetInstance();
            var table = db.Queryable<LD_ROOM, LD_ROOM_TYPE>(
                (r, rt) =>  r.TYPE_ID == rt.TYPE_ID)
                 .Select(( r, rt) => new {
                     rt.TYPE_NAME,
                     r.BOOK_STATUS,
                     rt.PRICE,
                     r.HOTEL_ID,
                     r.ROOM_ID
                 }).MergeTable()
                     .Where(it => it.BOOK_STATUS == "N" && it.HOTEL_ID==id )
                     .Select(item => new
                     {
                         item.TYPE_NAME,
                         item.PRICE,
                         item.ROOM_ID
                     })
                     .OrderBy(it => it.PRICE,OrderByType.Asc)
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