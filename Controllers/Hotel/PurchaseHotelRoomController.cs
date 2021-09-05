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
    //[Authorize]
    public class PurchaseHotelRoomController : ControllerBase
    {
        [HttpPost]
        public void purchaseRoom(LD_BOOK_ROOM obj)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var db1 = c.GetInstance();
            List<dynamic> list = new List<dynamic>();
            var table = db.Updateable<LD_ROOM>().Where(it => it.HOTEL_ID == obj.HOTEL_ID && it.ROOM_ID == obj.ROOM_ID).SetColumns(it =>it.BOOK_STATUS =="Y").ExecuteCommand();
            var t2 = db1.Insertable(obj).ExecuteCommand();
        }
     }
 }