using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class FunGetCommentByMomentIdController : ControllerBase
    {
        [HttpGet("{momentId}")]
        public List<dynamic> GetMomentInfoById(string momentId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_MOMENT, LD_COMMENT_MOMENT>((m, c) => new JoinQueryInfos(
             JoinType.Inner, m.MOMENT_ID == c.MOMENT_ID))
             .Select((m, c) => new {
                 c.USER_ID,
                 c.MOMENT_ID,
                 c.COMMENT_TIME,
                 c.COMMENT_TEXT,
             })
             .MergeTable()
             .Where(it => it.MOMENT_ID == momentId)
             .OrderBy(it => it.COMMENT_TIME, OrderByType.Desc)
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