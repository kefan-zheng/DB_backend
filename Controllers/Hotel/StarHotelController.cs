using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class StarHotelController : ControllerBase
    {
        // GET: api/StarHotel
        [HttpGet("{star}")]
        public async Task<ActionResult<IEnumerable<LD_HOTEL>>> StarHotel(string star)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            int star1 = int.Parse(star);
            var res = await db.Queryable<LD_HOTEL>().Where(it => it.STAR == star1).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
    }
}
