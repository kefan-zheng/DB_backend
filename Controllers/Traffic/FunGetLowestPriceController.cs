using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
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
    public class FunGetLowestPriceController : ControllerBase
    {
        [HttpGet("{city}")]
        public List<dynamic> FunGetLowestPrice(string city)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_VEHICLE_INFO, LD_TRAFFIC_TICKET>((v, t) => new JoinQueryInfos(
                JoinType.Inner, v.VEHICLE_ID == t.VEHICLE_ID))
               .Select((v, t) => new
               {
                   VEHICLE=v.VEHICLE_ID,
                   START_LOCATION=v.START_LOCATION,
                   END_LOCATION=v.END_LOCATION,
                   START_TIME=v.START_TIME,
                   END_TIME=v.END_TIME,
                   SEAT_ID=t.SEAT_ID,
                   SEAT_TYPE=t.SEAT_TYPE,
                   PRICE=t.PRICE,
                   TRADE_STATUS=t.TRADE_STATUS
               })
               .MergeTable()
               .Where( it=>it.START_LOCATION == city && it.TRADE_STATUS == "未售")
               .OrderBy(it => it.PRICE)
               .ToList();
            int i = 1;
            List<string> alcity = new List<string>();
            alcity.Add(table[0].END_LOCATION);
            List<dynamic> res = new();
            res.Add(table[0]);
            while (i < table.Count)
            {
                bool exists = alcity.Exists(o => o == table[i].END_LOCATION);
                if (exists) { }
                else {
                    alcity.Add(table[i].END_LOCATION);
                    res.Add(table[i]);
                }
                i++;
            }
            return res;
        }
    }
}
