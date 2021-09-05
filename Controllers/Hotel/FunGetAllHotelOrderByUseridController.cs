using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers.Hotel
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    //[Authorize]
    public class FunGetAllHotelOrderByUseridController:ControllerBase
    {
        [HttpGet("{userid_hotelid}")]
        public List<dynamic> GetAllHotelOrderByUserid(string userid_hotelid)
        {
            string[] para = userid_hotelid.Split(new char[] { '&' });
            string userid = para[0];
            string hotelid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new();
            var table = db.Queryable<LD_ROOM, LD_BOOK_ROOM, LD_ROOM_TYPE>(
                (r, b, t) => r.ROOM_ID == b.ROOM_ID && r.HOTEL_ID == b.HOTEL_ID && b.USER_ID == userid && r.HOTEL_ID == hotelid && r.TYPE_ID == t.TYPE_ID)
                .Select((r, b, t) => new
                {
                    r.TYPE_ID,
                    t.TYPE_NAME,
                    b.ORDER_TIME,
                    b.ORDER_AMOUNT
                }).MergeTable()
                .Select(it => new {
                    typeid = it.TYPE_ID,
                    typename = it.TYPE_NAME,
                    ordertime = it.ORDER_TIME,
                    orderamount = it.ORDER_AMOUNT
                })
                .ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
