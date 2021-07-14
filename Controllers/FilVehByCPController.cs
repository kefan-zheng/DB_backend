using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Collections;

namespace LvDao.Controllers.fun
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class FilVehByCPController:ControllerBase
    {
        [HttpGet("{com_pri}")]
        public List<dynamic> FilVehByCP(string com_pri)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            string[] para = com_pri.Split(new char[] { '&' });
            string com = para[0];
            string pri = para[1];
            int pric = 0;
            pric = int.Parse(pri);
            var table = db.Queryable<LD_TRAFFIC_TICKET, LD_OFFER_TRAFFIC_SERVICE, LD_TRAFFIC_COMPANY>
               ((tt, ots, tc) => new JoinQueryInfos(
                   JoinType.Inner, tt.VEHICLE_ID == ots.VEHICLE_ID,
                   JoinType.Inner, ots.COMPANY_ID == tc.COMPANY_ID))
                   .Select((tt, ots, tc) => new
                   {
                       tt.PRICE,
                       ots.VEHICLE_ID,
                       tc.COMPANY_NAME
                   }).MergeTable()
                   .Where(it => it.PRICE <= pric && it.COMPANY_NAME == com)
                   .Select(it=>it.VEHICLE_ID)
                   .ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
