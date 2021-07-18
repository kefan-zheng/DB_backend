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
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SeaVehByController:ControllerBase
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
               ((tt, vi, ots, tc) => tt.VEHICLE_ID == vi.VEHICLE_ID && tt.VEHICLE_ID == ots.VEHICLE_ID && ots.COMPANY_ID == tc.COMPANY_ID)
                   .Distinct().Select((tt, vi, ots, tc) => new
                   {
                       tt.SEAT_TYPE,
                       vi.VEHICLE_ID,
                       vi.START_LOCATION,
                       vi.END_LOCATION,
                       vi.START_TIME,
                       vi.END_TIME,
                       ots.TRAFFIC_TYPE,
                       tc.COMPANY_NAME
                   }).MergeTable()
                   .Where(it => it.SEAT_TYPE == seatype && it.START_LOCATION == start
                   && it.END_LOCATION == end && it.TRAFFIC_TYPE == tratype).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
