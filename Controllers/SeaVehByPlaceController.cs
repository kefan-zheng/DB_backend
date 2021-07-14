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
    public class SeaVehByPlaceController:ControllerBase
    {
        [HttpGet("{dep_arr}")]
        public List<dynamic> SeaVehByPlace(string dep_arr)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            string[] para = dep_arr.Split(new char[] { '&' });
            string dep = para[0];
            string arr = para[1];
            var table = db.Queryable<LD_VEHICLE_INFO>().Where(it => it.START_LOCATION == dep && it.END_LOCATION == arr).Select(it => it.VEHICLE_ID).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
