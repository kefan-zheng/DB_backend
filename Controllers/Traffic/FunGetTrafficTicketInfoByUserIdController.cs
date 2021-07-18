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
    [Authorize]
    public class FunGetTrafficTicketInfoByUserIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> FunGetTrafficTicketInfoByUserId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER, LD_PURCHASE_TRAFFIC_TICKET, LD_TRAFFIC_TICKET,LD_VEHICLE_INFO>(
                (u, p, t, v) => u.USER_ID == p.USER_ID && p.VEHICLE_ID == t.VEHICLE_ID && t.VEHICLE_ID == v.VEHICLE_ID)
                .Select((u, p ,t, v) => new {
                    p.ORDER_TIME,
                    p.ORDER_AMOUNT,
                    p.VEHICLE_ID,
                    t.SEAT_TYPE,
                    v.START_LOCATION,
                    v.END_LOCATION,
                    u.USER_ID
                })
                .MergeTable()
                .Where(it => it.USER_ID == id)
                .Select(it => new {
                    it.ORDER_TIME,
                    it.ORDER_AMOUNT,
                    it.VEHICLE_ID,
                    it.SEAT_TYPE,
                    it.START_LOCATION,
                    it.END_LOCATION
                })
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