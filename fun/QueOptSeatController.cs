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
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class QueOptSeatController:ControllerBase
    {
        [HttpGet("{vehid_seatype}")]
        public List<dynamic> QueOptSeat(string vehid_seatype)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            //处理字符串
            string[] para = vehid_seatype.Split(new char[] { '&' });
            string vehid = para[0];
            string seatype = para[1];
            List<dynamic> list = new List<dynamic>();
            var table = db.Queryable<LD_TRAFFIC_TICKET, LD_VEHICLE_INFO, LD_OFFER_TRAFFIC_SERVICE, LD_TRAFFIC_COMPANY>
               ((tt,vi,ots,tc) => new JoinQueryInfos(
                   JoinType.Inner, tt.VEHICLE_ID == vi.VEHICLE_ID,
                   JoinType.Inner, tt.VEHICLE_ID == ots.VEHICLE_ID,
                   JoinType.Inner, ots.COMPANY_ID == tc.COMPANY_ID))
                   .Select((tt,vi,ots,tc) => new
                   {
                       tt,vi,tc.COMPANY_ID
                   }).MergeTable()
                   .Where(it=>it.tt .VEHICLE_ID==vehid&&it.tt.SEAT_TYPE==seatype).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
