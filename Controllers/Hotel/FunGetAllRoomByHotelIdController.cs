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
    public class FunGetAllRoomByHotelIdController:ControllerBase
    {
        [HttpGet("{hotelid}")]
        public List<dynamic> GetAllRoomByHotelId(string hotelid)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> list = new();
            var table = db.Queryable<LD_ROOM>().Where(it => it.HOTEL_ID == hotelid).ToList();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }
            return list;
        }
    }
}
