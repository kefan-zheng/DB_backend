using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using SqlSugar;
using LvDao.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers.Attraction
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    //[Authorize]
    public class FunGetAllAttractionOrderByUseridController:ControllerBase
    {
        [HttpGet("{userid_attid}")]
        public List<dynamic> GetAllAttractionOrderByUserid(string userid_attid)
        {
            string[] para = userid_attid.Split(new char[] { '&' });
            string userid = para[0];
            string attid = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new();
            var table = db.Queryable<LD_PURCHASE_ATTRACTION_TICKET, LD_ATTRACTION>(
                (p, a) => p.USER_ID == userid && p.ATTRACTION_ID == attid && p.ATTRACTION_ID == a.ATTRACTION_ID)
                .Select((p, a) => new
                {
                    p.USER_ID,
                    a.ATTRACTION_ID,
                    a.ATTRACTION_NAME,
                    p.ORDER_TIME,
                    p.PRICE
                }).MergeTable()
                .Select(it => new {
                    userid = it.USER_ID,
                    attractionid = it.ATTRACTION_ID,
                    attractionname = it.ATTRACTION_NAME,
                    ordertime = it.ORDER_TIME,
                    price = it.PRICE
                })
                .ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
