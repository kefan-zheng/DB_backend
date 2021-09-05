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
    public class FunGetAttractionInfoByNameController:ControllerBase
    {
        [HttpGet("{name}")]
        public List<dynamic> GetAttractionInfoByName(string name)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new();
            var table = db.Queryable<LD_ATTRACTION, LD_COMMENT_ON_ATTRACTIONS>((a, c) => new JoinQueryInfos(
                JoinType.Left, a.ATTRACTION_ID == c.ATTRACTION_ID))
                .Where(a=>a.ATTRACTION_NAME.Contains(name))
                .Select((a, c) => new
                {
                    a.ATTRACTION_ID,
                    a.ATTRACTION_NAME,
                    a.ALOCATION,
                    a.PICTURE,
                    a.OPEN_TIME,
                    a.CLOSE_TIME,
                    a.STAR,
                    a.PRICE,
                    a.LABEL,
                    c.USER_ID
                }).MergeTable()
                .GroupBy(it => new { it.ATTRACTION_ID, it.ATTRACTION_NAME, it.ALOCATION, it.PICTURE, it.OPEN_TIME,
                                     it.CLOSE_TIME, it.STAR, it.PRICE, it.LABEL })
                .Select(it => new {
                    attractionid = it.ATTRACTION_ID,
                    attractionname = it.ATTRACTION_NAME,
                    location = it.ALOCATION,
                    picture = it.PICTURE,
                    opentime = it.OPEN_TIME,
                    closetime = it.CLOSE_TIME,
                    star = it.STAR,
                    price = it.PRICE,
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
