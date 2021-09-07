using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PurchaseTrafficTicketController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_PURCHASE_TRAFFIC_TICKET>>> GetPurchaseTrafficTicket()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_PURCHASE_TRAFFIC_TICKET>().ToListAsync();
        }

        // GET: api/PurchaseTrafficTicket/5
        [HttpGet("{vehicleId_userId_date}")]
        public async Task<ActionResult<IEnumerable<LD_PURCHASE_TRAFFIC_TICKET>>> GetPurchaseTrafficTicket(string vehicleId_userId_date)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_userId_date.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string user_id = para[1];
            string date = para[2];

            var res = await db.Queryable<LD_PURCHASE_TRAFFIC_TICKET>().Where(it => it.USER_ID == user_id && it.VEHICLE_ID==vehicle_id && it.FLIGHT_DATE == date).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/PurchaseTrafficTicket
        [HttpPost]
        public async Task<ActionResult<LD_PURCHASE_TRAFFIC_TICKET>> PostPurchaseTrafficTicket(LD_PURCHASE_TRAFFIC_TICKET purchase_traffic_ticket)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(purchase_traffic_ticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_PURCHASE_TRAFFIC_TICKET>().Where(it => it.USER_ID == purchase_traffic_ticket.USER_ID && it.VEHICLE_ID==purchase_traffic_ticket.VEHICLE_ID && it.FLIGHT_DATE == purchase_traffic_ticket.FLIGHT_DATE).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetPurchaseTrafficTicket), new { seat_id = purchase_traffic_ticket.USER_ID,vehicle_id=purchase_traffic_ticket.VEHICLE_ID,flight_date = purchase_traffic_ticket.FLIGHT_DATE }, purchase_traffic_ticket);
        }


        // PUT: api/PurchaseTrafficTicket/5
        [HttpPut("{vehicleId_seatId_date}")]
        public async Task<IActionResult> PutPurchaseTrafficTicket(string vehicleId_seatId, LD_PURCHASE_TRAFFIC_TICKET purchase_traffic_ticket)
        {
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_id = para[1];
            string date = para[2];

            if (seat_id != purchase_traffic_ticket.USER_ID || vehicle_id!=purchase_traffic_ticket.VEHICLE_ID || date!=purchase_traffic_ticket.FLIGHT_DATE)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(purchase_traffic_ticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_PURCHASE_TRAFFIC_TICKET>().Where(it => it.USER_ID == seat_id && it.VEHICLE_ID==vehicle_id && it.FLIGHT_DATE==date).Any())
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

        // DELETE: api/PurchaseTrafficTicket/5
        [HttpDelete("{vehicleId_seatId_date}")]
        public async Task<IActionResult> DeletePurchaseTrafficTicket(string vehicleId_userId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_userId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string user_id = para[1];
            string date = para[2];

            var res = await db.Queryable<LD_PURCHASE_TRAFFIC_TICKET>().Where(it => it.USER_ID == user_id && it.VEHICLE_ID==vehicle_id && it.FLIGHT_DATE == date).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_PURCHASE_TRAFFIC_TICKET>().Where(it => it.USER_ID == user_id && it.VEHICLE_ID == vehicle_id && it.FLIGHT_DATE == date).ExecuteCommand());
            return NoContent();
        }
    }
}
