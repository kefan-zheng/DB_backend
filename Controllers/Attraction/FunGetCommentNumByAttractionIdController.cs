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
    public class FunGetCommentNumByAttractionIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetCommentNumAttractionId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_ATTRACTION,LD_COMMENT_ON_ATTRACTIONS>((a,c) => new JoinQueryInfos(
             JoinType.Inner, a.ATTRACTION_ID==c.ATTRACTION_ID))
                .Select( (a,c)=> new {   
                    a.ATTRACTION_ID,
                    a.ATTRACTION_NAME,
                    c.USER_ID
                })
                .MergeTable()
                .Where(it => it.ATTRACTION_ID == id)
                .Select(it => new { count = SqlFunc.AggregateCount(it)})
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