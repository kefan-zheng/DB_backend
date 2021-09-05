using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FindBookedRoom : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> Book(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            var table = db.Queryable<LD_BOOK_ROOM, LD_HOTEL>
               ((tt, ots) => new JoinQueryInfos(
                   JoinType.Inner, tt.HOTEL_ID == ots.HOTEL_ID))
                   .Select((tt, ots) => new
                   {
                       ots.HOTEL_NAME,
                       ots.PICTURE,
                       tt.ORDER_AMOUNT,
                       tt.ORDER_TIME
                   }).MergeTable()
                   .ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
