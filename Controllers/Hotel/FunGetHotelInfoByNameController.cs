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

namespace LvDao.Controllers.Hotel
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FunGetHotelInfoByNameController:ControllerBase
    {
        // GET: api/
        [HttpGet("{name}")]
        public List<dynamic> GetHotelInfoByName(string name)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new();
            var table = db.Queryable<LD_HOTEL, LD_COMMENT_ON_HOTELS>((h, c) => new JoinQueryInfos(
                JoinType.Left, h.HOTEL_ID == c.HOTEL_ID))
                .Where(h=> h.HOTEL_NAME.Contains(name))
                .Select((h, c) => new
                {
                    h.HOTEL_ID,
                    h.HOTEL_NAME,
                    h.HLOCATION,
                    h.PICTURE,
                    h.STAR,
                    h.LOWEST_PRICE,
                    h.LABEL,
                    c.USER_ID
                }).MergeTable()
                .GroupBy(it => new { it.HOTEL_ID, it.HOTEL_NAME, it.HLOCATION, it.PICTURE, it.STAR, it.LOWEST_PRICE, it.LABEL })
                .Select(it => new {
                    hoteid = it.HOTEL_ID,
                    hotelname = it.HOTEL_NAME,
                    location = it.HLOCATION,
                    picture = it.PICTURE,
                    star = it.STAR,
                    lowestprice = it.LOWEST_PRICE,
                    label = it.LABEL,
                    commentnum = SqlFunc.AggregateCount(it.USER_ID)
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
