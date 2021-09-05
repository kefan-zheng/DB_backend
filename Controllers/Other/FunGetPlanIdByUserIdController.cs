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
    public class FunGetPlanIdByUserIdController:ControllerBase
    {
        [HttpGet("{userId}")]
        public List<dynamic> GetMomentInfoById(string userId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            if(db.Queryable<LD_PLAN>().Where(it => it.USER_ID == userId).Any())
            {
                var table = db.Queryable<LD_PLAN>()
                 .Where(it => it.USER_ID == userId)
                 .GroupBy(it => new { it.USER_ID })
                 .Select(it => new
                 {
                     planid = SqlFunc.AggregateCount(it.PLAN_ID)
                 })
                 .ToList();

                List<dynamic> res = new();
                for (int i = 0; i < table.Count; i++)
                {
                    res.Add(table[i]);
                }
                return res;
            }
            else
            {
                List<dynamic> res = new();
                return res;
            }
            
        }
    }
}
