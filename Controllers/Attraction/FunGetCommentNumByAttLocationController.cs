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
    public class FunGetCommentNumByAttLocationController:ControllerBase
    {
        [HttpGet("{city}")]
        public List<dynamic> FunGetCommentNumByAttLocation(string city)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            if(city=="全部")
            {
                var table = db.Queryable<LD_ATTRACTION, LD_COMMENT_ON_ATTRACTIONS>((a,c)=> new JoinQueryInfos(
                    JoinType.Left, a.ATTRACTION_ID == c.ATTRACTION_ID))
                        .Select((a, c) => new
                        {
                            a.ATTRACTION_ID,
                            a.ATTRACTION_NAME,
                            a.ALOCATION,
                            a.PICTURE,
                            a.OPEN_TIME,
                            a.CLOSE_TIME,
                            a.PRICE,
                            a.STAR,
                            a.LABEL,
                            c.USER_ID
                        })
                        .MergeTable()
                        .GroupBy(it => new { it.ATTRACTION_ID, it.ATTRACTION_NAME, it.ALOCATION, it.PICTURE, it.OPEN_TIME, it.CLOSE_TIME, it.PRICE, it.STAR, it.LABEL })
                        .Select(it => new {
                            attractionid = it.ATTRACTION_ID,
                            attractionname = it.ATTRACTION_NAME,
                            location = it.ALOCATION,
                            picture = it.PICTURE,
                            opentime = it.OPEN_TIME,
                            closetime = it.CLOSE_TIME,
                            price = it.PRICE,
                            star = it.STAR,
                            label = it.LABEL,
                            commentnum = SqlFunc.AggregateCount(it.USER_ID)
                        })
                        .ToList();
                for (int i = 0; i < table.Count; i++)
                {
                    list.Add(table[i]);
                }
            }
            else
            {
                var table = db.Queryable<LD_ATTRACTION, LD_COMMENT_ON_ATTRACTIONS>((a, c) => new JoinQueryInfos(
                    JoinType.Left, a.ATTRACTION_ID==c.ATTRACTION_ID))
                    .Select((a, c) => new
                    {
                        a.ATTRACTION_ID,
                        a.ATTRACTION_NAME,
                        a.ALOCATION,
                        a.PICTURE,
                        a.OPEN_TIME,
                        a.CLOSE_TIME,
                        a.PRICE,
                        a.STAR,
                        a.LABEL,
                        c.USER_ID
                    })
                    .MergeTable()
                    .Where(it=>it.ALOCATION.Contains(city))
                    .GroupBy(it => new { it.ATTRACTION_ID, it.ATTRACTION_NAME, it.ALOCATION, it.PICTURE, it.OPEN_TIME, it.CLOSE_TIME, it.PRICE, it.STAR, it.LABEL })
                    .Select(it => new {
                        attractionid = it.ATTRACTION_ID,
                        attractionname = it.ATTRACTION_NAME,
                        location = it.ALOCATION,
                        picture = it.PICTURE,
                        opentime = it.OPEN_TIME,
                        closetime = it.CLOSE_TIME,
                        price = it.PRICE,
                        star = it.STAR,
                        label = it.LABEL,
                        commentnum = SqlFunc.AggregateCount(it.USER_ID)
                    })
                    .ToList();
                for (int i = 0; i < table.Count; i++)
                {
                    list.Add(table[i]);
                }
            }
            return list;
        }
    }
}
