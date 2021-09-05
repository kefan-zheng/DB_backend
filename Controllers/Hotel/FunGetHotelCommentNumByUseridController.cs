using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers.Other
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    //[Authorize]
    public class FunGetHotelCommentNumByUseridController
    {
        [HttpGet("{id}")]
        public List<dynamic> GetHotelCommentNumByUserid(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_COMMENT_ON_HOTELS>()
                .Select(it => new
                {
                    it.USER_ID,
                    it.HOTEL_ID,
                    it.COMMENT_TIME
                })
                .MergeTable()
                .Where(it => it.USER_ID == id)
                .GroupBy(it => new { it.USER_ID })
                .Select(it => new {
                    userid = it.USER_ID,
                    hotelcommentnum = SqlFunc.AggregateCount(it.HOTEL_ID)
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
