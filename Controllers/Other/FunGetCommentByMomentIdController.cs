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
    public class FunGetCommentByMomentIdController : ControllerBase
    {
        [HttpGet("{momentId}")]
        public List<dynamic> GetMomentInfoById(string momentId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_MOMENT, LD_COMMENT_MOMENT>((u, m, c) => 
             m.MOMENT_ID == c.MOMENT_ID && u.USER_ID == c.USER_ID)
             .Select((u, m, c) => new {
                 c.USER_ID,
                 u.USER_NAME,
                 u.UPROFILE,
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