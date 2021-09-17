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
    public class FunGetTrainTicketByUidController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> Get(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_PURCHASE_TRAIN_TICKET>()
             .Where(it => it.USER_ID == id)
              .Select(it => new
              {
                  it.ORDER_TIME,
                  it.ORDER_AMOUNT,
                  it.TRAIN_DATE,

                  it.VEHICLE_ID,
                  it.START_LOCATION,
                  it.END_LOCATION,
                  it.SEAT_TYPE,
              })
             .OrderBy(it => it.TRAIN_DATE, OrderByType.Asc)
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