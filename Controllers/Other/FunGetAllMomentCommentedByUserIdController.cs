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
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FunGetAllMomentCommentedByUserIdController : ControllerBase
    {
        [HttpGet("{userId}")]
        public List<dynamic> GetMomentInfoById(string userId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_COMMENT_MOMENT, LD_MOMENT>((u, c, m) => new JoinQueryInfos(
              JoinType.Inner, u.USER_ID == c.USER_ID,
              JoinType.Inner, m.MOMENT_ID == c.MOMENT_ID))
             .Select((u, c, m) => new {
                 m.MOMENT_ID,
                 m.MOMENT_TIME,
                 m.MOMENT_LOCATION,
                 m.TEXT,
                 m.PICTURE,
                 m.VEDIO,
                 u.USER_ID
             })
             .MergeTable()
             .Where(it => it.USER_ID == userId)
             .OrderBy(it => it.MOMENT_TIME, OrderByType.Desc)
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