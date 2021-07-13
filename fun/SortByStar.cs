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

namespace LvDao.fun
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class SortByStar : Controller
    {
        [HttpGet]
        public List<dynamic> Sort()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var query = db.Queryable<LD_HOTEL>().OrderBy(q => q.STAR,OrderByType.Desc).ToList();
            List<dynamic> res = new List<dynamic>();
            for (int i = 0; i < query.Count; i++)
            {
                res.Add(query[i]);
            }
            return res;
        }
    }
}
