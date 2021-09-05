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
    public class FunDeleteMomentByMomentIdController : ControllerBase
    {
        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public bool DeleteMomentByMomentId(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();

            //删除moment
            //数据库中使用触发器级联删除
            db.Deleteable<LD_MOMENT>().
                Where(
                    it => it.MOMENT_ID == id).
                ExecuteCommand();

            return true;
        }
    }
}