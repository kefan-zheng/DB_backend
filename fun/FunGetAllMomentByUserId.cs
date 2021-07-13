﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class FunGetAllMomentByUserIdController : ControllerBase
    {
        [HttpGet("{userId}")]
        public List<dynamic> GetMomentInfoById(string userId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_RELEASE_MOMENT,LD_MOMENT>((u,r,m) => new JoinQueryInfos(
             JoinType.Inner, u.USER_ID == r.USER_ID,
             JoinType.Inner, m.MOMENT_ID == r.MOMENT_ID))
             .Select((u,r, m) => new {
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
             .OrderBy(it=>it.MOMENT_TIME,OrderByType.Desc)
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