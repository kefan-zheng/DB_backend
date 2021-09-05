using System.Collections.Generic;
using System.Linq;
using LvDao.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SortByPriceSController : ControllerBase
    {
        [HttpGet]
        public List<dynamic> Sort()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var query = db.Queryable<LD_HOTEL>().OrderBy(q => q.LOWEST_PRICE).ToList();
            List<dynamic> res = new List<dynamic>();
            for(int i=0;i<query.Count;i++)
            {
                res.Add(query[i]);
            }
            return res;
        }
    }
}


