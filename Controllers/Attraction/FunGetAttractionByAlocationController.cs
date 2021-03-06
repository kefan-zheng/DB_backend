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
    public class FunGetAttractionByAlocationController : ControllerBase
    {
        [HttpGet("{location}")]
        public List<dynamic> GetAttractionByAlocation(string location)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_ATTRACTION>()
                .Where(it => it.ALOCATION.Contains(location))
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