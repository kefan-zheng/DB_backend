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
    public class FunGetCommentByHotelIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetCommentByHotelId(string id)
        {
            SqlSugar cs = new();
            var db = cs.GetInstance();
            var table = db.Queryable<LD_HOTEL, LD_USER, LD_COMMENT_ON_HOTELS>(
                (h, u, c) => h.HOTEL_ID == c.HOTEL_ID && c.USER_ID == u.USER_ID)
                 .Select((h, u, c) => new {
                     u.USER_ID,
                     u.USER_NAME,
                     c.COMMENT_TIME,
                     c.GRADE,
                     c.CTEXT,
                     c.PICTURE,
                     c.VIDEO,
                     h.HOTEL_ID
                 }).MergeTable()
                     .Where(it => it.HOTEL_ID == id)
                     .Select(item => new
                     {
                         item.USER_ID,
                         item.USER_NAME,
                         item.COMMENT_TIME,
                         item.GRADE,
                         item.CTEXT,
                         item.PICTURE,
                         item.VIDEO,
                     })
                     .OrderBy(it => it.COMMENT_TIME, OrderByType.Desc)
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