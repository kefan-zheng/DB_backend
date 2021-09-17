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
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FunGetFlightTicketByUidController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> Get(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_PURCHASE_TRAFFIC_TICKET,LD_VEHICLE_INFO>((p,v) => new JoinQueryInfos(
             JoinType.Inner, p.VEHICLE_ID == v.VEHICLE_ID))
             .Select((p,v) => new{
                p.ORDER_AMOUNT,
                p.ORDER_TIME,
                p.USER_ID,

                v.VEHICLE_ID,
                v.COMPANY_NAME,
                v.START_AIRPORT,
                v.END_AIRPORT,
                v.START_TIME,
                v.END_TIME,
                v.START_LOCATION,
                v.END_LOCATION,
                p.SEAT_TYPE,
                p.FLIGHT_DATE,
             })
             .MergeTable()
             .Where(it => it.USER_ID == id)
              .Select(it => new
              {
                  it.ORDER_TIME,
                  it.ORDER_AMOUNT,
                  it.FLIGHT_DATE,

                  it.VEHICLE_ID,
                  it.COMPANY_NAME,
                  it.START_LOCATION,
                  it.END_LOCATION,
                  it.START_AIRPORT,
                  it.END_AIRPORT,
                  it.START_TIME,
                  it.END_TIME,
                  it.SEAT_TYPE,
              })
             .OrderBy(it => it.FLIGHT_DATE, OrderByType.Asc)
             .ToList();

            List<dynamic> res = new();
            //int lowest = table[0].PRICE;
            for (int i = 0; i < table.Count; i++)
            {
                    res.Add(table[i]);
            }
            return res;
        }
    }
}