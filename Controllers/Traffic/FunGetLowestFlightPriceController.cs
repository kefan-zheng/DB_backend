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
    public class FunGetLowestFlightPriceController : ControllerBase
    {
        [HttpGet("{fromWhere_toWhere_date}")]
        public List<dynamic> GetMomentInfoById(string fromWhere_toWhere_date)
        {
            //处理字符串
            string[] para = fromWhere_toWhere_date.Split(new char[] { '&' });
            string fromWhere = para[0];
            string toWhere = para[1];
            string date = para[2];

            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_TRAFFIC_TICKET, LD_VEHICLE_INFO>((t,v) => new JoinQueryInfos(
             JoinType.Inner, t.VEHICLE_ID == v.VEHICLE_ID ))
             .Select((t,v) => new{
                v.VEHICLE_ID,
                v.COMPANY_NAME,
                t.PRICE,
                v.START_AIRPORT,
                v.END_AIRPORT,
                v.START_TIME,
                v.END_TIME,
                v.START_LOCATION,
                v.END_LOCATION,
                t.REMAINING_NUM,
                t.SEAT_TYPE,
                t.FLIGHT_DATE})
             .MergeTable()
             .Where(it => it.START_LOCATION == fromWhere && it.END_LOCATION == toWhere && it.FLIGHT_DATE == date)
              .Select(it => new
              {
                  it.PRICE,
                  it.VEHICLE_ID,
                  it.COMPANY_NAME,
                  it.START_AIRPORT,
                  it.END_AIRPORT,
                  it.START_TIME,
                  it.END_TIME,
                  it.SEAT_TYPE,
                  it.REMAINING_NUM
              })
             .OrderBy(it => it.PRICE, OrderByType.Asc)
             .ToList();

            List<dynamic> res = new();
            int lowest = table[0].PRICE;
            for (int i = 0; i < table.Count; i++)
            {
                if (table[i].PRICE == table[0].PRICE)
                    res.Add(table[i]);
                else
                    break;
            }
            return res;
        }
    }
}