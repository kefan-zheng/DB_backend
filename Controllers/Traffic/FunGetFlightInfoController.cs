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
    public class FunGetFlightInfoController : ControllerBase
    {
        [HttpGet("{fromWhere_toWhere_date}")]
        public List<dynamic> GetFlightInfo(string fromWhere_toWhere_date)
        {
            //处理字符串
            string[] para = fromWhere_toWhere_date.Split(new char[] { '&' });
            string fromWhere = para[0];
            string toWhere = para[1];
            string date = para[2];

            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_TRAFFIC_TICKET, LD_VEHICLE_INFO>(
                (t, v) => t.VEHICLE_ID == v.VEHICLE_ID)
                .Select((t, v) => new
                {
                    t.VEHICLE_ID,
                    v.COMPANY_NAME,
                    v.START_LOCATION,
                    v.END_LOCATION,
                    v.START_AIRPORT,
                    v.END_AIRPORT,
                    v.START_TIME,
                    v.END_TIME,
                    t.SEAT_TYPE,
                    t.PRICE,
                    t.REMAINING_NUM,
                    t.FLIGHT_DATE
                })
                .MergeTable()
                .Where(it => it.START_LOCATION == fromWhere && it.END_LOCATION == toWhere && it.FLIGHT_DATE == date)
                .Select(it => new
                {
                    it.VEHICLE_ID,
                    it.COMPANY_NAME,
                    it.START_AIRPORT,
                    it.END_AIRPORT,
                    it.START_TIME,
                    it.END_TIME,
                    it.SEAT_TYPE,
                    it.PRICE,
                    it.REMAINING_NUM
                })
                .OrderBy(it => it.START_TIME, OrderByType.Desc)
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