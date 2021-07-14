using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContainTicketInfoController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_CONTAIN_TICKET_INFO>>> GetContainTicketInfo()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_CONTAIN_TICKET_INFO>().ToListAsync();
        }

        // GET: api/ContainTicketInfo/5
        [HttpGet("{vehicleId_seatId}")]
        public async Task<ActionResult<IEnumerable<LD_CONTAIN_TICKET_INFO>>> GetContainTicketInfo(string vehicleId_seatId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_id = para[1];
            var res = await db.Queryable<LD_CONTAIN_TICKET_INFO>().Where(it => it.SEAT_ID == seat_id && it.VEHICLE_ID ==vehicle_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/ContainTicketInfo
        [HttpPost]
        public async Task<ActionResult<LD_CONTAIN_TICKET_INFO>> PostContainTicketInfo(LD_CONTAIN_TICKET_INFO contain_ticket_info)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(contain_ticket_info).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_CONTAIN_TICKET_INFO>().Where(it => it.SEAT_ID == contain_ticket_info.SEAT_ID && it.VEHICLE_ID==contain_ticket_info.VEHICLE_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetContainTicketInfo), new { seat_id = contain_ticket_info.SEAT_ID,vehicle_id=contain_ticket_info.VEHICLE_ID }, contain_ticket_info);
        }


        // PUT: api/ContainTicketInfo/5
        [HttpPut("{vehicleId_seatId}")]
        public async Task<IActionResult> PutContainTicketInfo(string vehicleId_seatId, LD_CONTAIN_TICKET_INFO contain_ticket_info)
        {
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_id = para[1];
            if (seat_id != contain_ticket_info.SEAT_ID || vehicle_id!=contain_ticket_info.VEHICLE_ID )
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(contain_ticket_info).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_CONTAIN_TICKET_INFO>().Where(it => it.SEAT_ID == seat_id && it.VEHICLE_ID==vehicle_id).Any())
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/ContainTicketInfo/5
        [HttpDelete("{vehicleId_seatId}")]
        public async Task<IActionResult> DeleteContainTicketInfo(string vehicleId_seatId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_id = para[1];
            var res = await db.Queryable<LD_CONTAIN_TICKET_INFO>().Where(it => it.SEAT_ID == seat_id && it.VEHICLE_ID==vehicle_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_CONTAIN_TICKET_INFO>().Where(it => it.SEAT_ID == seat_id && it.VEHICLE_ID == vehicle_id).ExecuteCommand());
            return NoContent();
        }
    }
}
