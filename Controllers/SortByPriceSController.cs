using System.Collections.Generic;
using System.Linq;
using LvDao.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
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

//public class momentInfo { public USER_ID,USER};
/*[HttpGet("{moment_id}")]
public List<dynamic> GetFaqs(string moment_id)
{
    SqlSugar c = new();
    var db = c.GetInstance();
            / namic resType;

    var table = db.Queryable<LD_USER, LD_RELEASE_MOMENT, LD_MOMENT>((u, r, m) => new JoinQueryInfos(
     JoinType.Inner, u.USER_ID == r.USER_ID,
     JoinType.Inner, m.MOMENT_ID == r.MOMENT_ID
     ))
     .Select((u, r, m) => new
     {
         m.MOMENT_ID,
         u.USER_ID,
         u.UPROFILE
     })
     .MergeTable()
     .Where(it => it.MOMENT_ID == moment_id)
     .ToList();

    List<dynamic> res = new List<dynamic>();
    int b = table.Capacity;
    for (int i = 0; i < table.Count; i++)
    {
        res.Add(table[i]);
    }
    return res;
}*/
