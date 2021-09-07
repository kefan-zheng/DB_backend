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
                     rt.TYPE_ID,
                     r.BOOK_STATUS,
                     rt.ORIGINAL_PRICE,
                     r.HOTEL_ID,
                     r.ROOM_ID,
                     rt.ROOM_NAME,
                     rt.CUSTOMER_NUM,
                     rt.BED,
                     rt.DISH,
                     rt.SMOKE,
                     rt.WINDOW,
                     rt.CANCEL,
                     rt.PRICE,
                     rt.COVER_IMG_URL
                 }).MergeTable()
                     .Where(it => it.BOOK_STATUS == "N" && it.HOTEL_ID==id )
                     .Select(item => new
                     {
                         item.TYPE_NAME,
                         item.TYPE_ID,
                         item.ORIGINAL_PRICE,
                         item.ROOM_NAME,
                         item.CUSTOMER_NUM,
                         item.BED,
                         item.DISH,
                         item.SMOKE,
                         item.WINDOW,
                         item.CANCEL,
                         item.PRICE,
                         item.COVER_IMG_URL
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