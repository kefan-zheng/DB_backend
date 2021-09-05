using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CityHotelController : ControllerBase
    {
        // GET: api/CityHotel
        [HttpGet("{city}")]
        public List<dynamic> CityHotel(string city)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            var table = db.Queryable<LD_HOTEL, LD_COMMENT_ON_HOTELS>((h, c) => h.HOTEL_ID == c.HOTEL_ID && h.HLOCATION.Contains(city))
                .Select((h, c) => new
                {
                    h.HOTEL_ID,
                    h.HOTEL_NAME,
                    h.HLOCATION,
                    h.PICTURE,
                    h.STAR,
                    c.USER_ID
                }).MergeTable()
                .GroupBy(it => new { it.HOTEL_ID,it.HOTEL_NAME,it.HLOCATION,it.PICTURE,it.STAR })
                .Select(it => new { hoteid = it.HOTEL_ID,hotelname = it.HOTEL_NAME,location = it.HLOCATION,
                    picture = it.PICTURE,star = it.STAR,commentnum = SqlFunc.AggregateCount(it.USER_ID) })
                .ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
