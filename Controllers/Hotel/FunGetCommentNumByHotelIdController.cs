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
    public class FunGetCommentNumByHotelIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetCommentNumAttractionId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_HOTEL, LD_COMMENT_ON_HOTELS>((h, c) => new JoinQueryInfos(
              JoinType.Inner, h.HOTEL_ID == c.HOTEL_ID))
                .Select((h, c) => new {
                    h.HOTEL_ID,
                    h.HOTEL_NAME,
                    c.USER_ID
                })
                .MergeTable()
                .Where(it => it.HOTEL_ID == id)
                .Select(it => new { count = SqlFunc.AggregateCount(it) })
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