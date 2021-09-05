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
    public class FunGetAttractionInfoByUserIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetAttractionInfoByUserId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var table = db.Queryable<LD_USER,LD_ATTRACTION, LD_PURCHASE_ATTRACTION_TICKET>(
                (u, a, p) => u.USER_ID==p.USER_ID && p.ATTRACTION_ID == a.ATTRACTION_ID)
                .Select((u, a, p) => new {
                    p.ORDER_TIME,
                    p.PRICE,
                    a.ATTRACTION_NAME,
                    a.PICTURE,
                    u.USER_ID,
                    a.ALOCATION,
                    a.ATTRACTION_ID
                })
                .MergeTable()
                .Where(it => it.USER_ID == id)
                .Select(it => new {
                    it.ORDER_TIME,
                    it.PRICE,
                    it.ATTRACTION_NAME,
                    it.PICTURE,
                    it.ALOCATION,
                    it.ATTRACTION_ID
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