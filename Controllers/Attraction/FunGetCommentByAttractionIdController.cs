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
    public class FunGetCommentByAttractionIdController : ControllerBase
    {
        [HttpGet("{id}")]
        public List<dynamic> GetCommentByAttractionId(string id)
        {
            SqlSugar cs = new();
            var db = cs.GetInstance();
            var table = db.Queryable<LD_ATTRACTION, LD_USER, LD_COMMENT_ON_ATTRACTIONS>(
                (a,u,c) => a.ATTRACTION_ID == c.ATTRACTION_ID && c.USER_ID == u.USER_ID)
                 .Select((a,u,c) => new {
                     u.USER_ID,
                     u.USER_NAME,
                     c.ACOMMENT_TIME,
                     c.GRADE,
                     c.CTEXT,
                     c.PICTURE,
                     c.VIDEO,
                     a.ATTRACTION_ID
                     }).MergeTable()
                     .Where(it => it.ATTRACTION_ID == id)
                     .Select(item => new
                     {
                         item.USER_ID,
                         item.USER_NAME,
                         item.ACOMMENT_TIME,
                         item.GRADE,
                         item.CTEXT,
                         item.PICTURE,
                         item.VIDEO,
                     })
                     .OrderBy(it => it.ACOMMENT_TIME, OrderByType.Desc)
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