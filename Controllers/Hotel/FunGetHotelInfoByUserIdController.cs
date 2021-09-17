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
    public class FunGetHotelInfoByUserIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetHotelInfoByUserId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_HOTEL, LD_BOOK_ROOM>(
                (u, h, b) => u.USER_ID == b.USER_ID && b.HOTEL_ID == h.HOTEL_ID)
                .Select((u, h, b) => new {
                    h.HOTEL_ID,
                    h.HOTEL_NAME,
                    h.PICTURE,
                    b.ROOM_ID,
                    b.ORDER_AMOUNT,
                    b.ORDER_TIME,
                    u.USER_ID,
                    h.HLOCATION
                })
                .MergeTable()
                .Where(it => it.USER_ID == id)
                .Select(it => new {
                    it.HOTEL_ID,
                    it.HOTEL_NAME,
                    it.PICTURE,
                    it.ORDER_AMOUNT,
                    it.ORDER_TIME,
                    it.HLOCATION,
                    it.ROOM_ID
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