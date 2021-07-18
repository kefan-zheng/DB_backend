using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using System.Collections;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SortByPriceController:ControllerBase
    {
        [HttpGet]
        public List<dynamic> SortByPrice()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            var table = db.Queryable<LD_TRAFFIC_TICKET>().OrderBy(it => it.PRICE,OrderByType.Asc).Select(it => it.VEHICLE_ID).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
