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

namespace LvDao.fun
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class CityHotelController : ControllerBase
    {
        // GET: api/CityHotel
        [HttpGet("{city}")]
        public async Task<ActionResult<IEnumerable<LD_HOTEL>>> CityHotel(string city)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_HOTEL>().Where(it => it.HLOCATION.Contains(city)).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
    }
}
