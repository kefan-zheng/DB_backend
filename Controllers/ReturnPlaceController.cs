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

namespace LvDao.Controllers.fun
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnPlaceController:ControllerBase
    {
        [HttpGet("{deporarr}")]
        public List<dynamic> QueOptSeat(string deporarr)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();            
            var table = db.Queryable<LD_VEHICLE_INFO>().Distinct().Select(it => it.START_LOCATION).ToList();
            if(deporarr=="终点")
            {
                table = db.Queryable<LD_VEHICLE_INFO>().Distinct().Select(it => it.END_LOCATION).ToList();
            }
               
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
