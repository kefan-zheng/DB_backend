using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers.Attraction
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    //[Authorize]
    public class FunGetAttractionCommentNumByUseridController:ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetAttractionCommentNumByUserid(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_COMMENT_ON_ATTRACTIONS>()
                .Select(it => new
                {
                    it.USER_ID,
                    it.ATTRACTION_ID,
                    it.ACOMMENT_TIME
                })
                .MergeTable()
                .Where(it => it.USER_ID == id)
                .GroupBy(it => new { it.USER_ID })
                .Select(it => new {
                    userid = it.USER_ID,
                    hotelcommentnum = SqlFunc.AggregateCount(it.ATTRACTION_ID)
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
