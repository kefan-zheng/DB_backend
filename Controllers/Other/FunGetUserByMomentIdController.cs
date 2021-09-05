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
    public class FunGetUserByMomentIdController : ControllerBase
    {
        [HttpGet("{momentId}")]
        public List<dynamic> GetMomentInfoById(string momentId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_RELEASE_MOMENT>((u, r) => new JoinQueryInfos(
             JoinType.Inner, u.USER_ID == r.USER_ID))
             .Select((u, r) => new {
                 u.USER_ID,
                 u.USER_NAME,
                 u.UPROFILE,
                 r.MOMENT_ID
             })
             .MergeTable()
             .Where(it => it.MOMENT_ID == momentId)
             .OrderBy(it => it.USER_ID, OrderByType.Asc)
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