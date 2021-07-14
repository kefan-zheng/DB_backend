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
    public class SeaVehByController
    {
        [HttpGet("{tratype_start_end_seatype}")]
        public List<dynamic> SeaVehBy(string tratype_start_end_seatype)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            //处理字符串
            string[] para = tratype_start_end_seatype.Split(new char[] { '&' });
            string tratype = para[0];
            string start = para[1];
            string end = para[2];
            string seatype = para[3];
            List<dynamic> list = new List<dynamic>();
            var table = db.Queryable<LD_TRAFFIC_TICKET, LD_VEHICLE_INFO, LD_OFFER_TRAFFIC_SERVICE, LD_TRAFFIC_COMPANY>
               ((tt, vi, ots, tc) => new JoinQueryInfos(
                   JoinType.Inner, tt.VEHICLE_ID == vi.VEHICLE_ID,
                   JoinType.Inner, tt.VEHICLE_ID == ots.VEHICLE_ID,
                   JoinType.Inner, ots.COMPANY_ID == tc.COMPANY_ID))
                   .Select((tt, vi, ots, tc) => new
                   {
                       tt.SEAT_TYPE,
                       vi,
                       ots.TRAFFIC_TYPE
                   }).MergeTable()
                   .Where(it => it.SEAT_TYPE == seatype && it.vi.START_LOCATION == start
                   && it.vi.END_LOCATION == end && it.TRAFFIC_TYPE == tratype).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
