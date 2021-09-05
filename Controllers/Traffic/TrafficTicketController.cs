﻿using System;
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
    [Authorize]
    public class TrafficTicketController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_TICKET>>> GetTrafficTicket()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_TRAFFIC_TICKET>().ToListAsync();
        }

        // GET: api/TrafficTicket/5
        [HttpGet("{vehicleId_seatType}")]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_TICKET>>> GetTrafficTicket(string vehicleId_seatType)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_seatType.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_type = para[1];
            var res = await db.Queryable<LD_TRAFFIC_TICKET>().Where(it => it.SEAT_TYPE == seat_type && it.VEHICLE_ID == vehicle_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/TrafficTicket
        [HttpPost]
        public async Task<ActionResult<LD_TRAFFIC_TICKET>> PostTrafficTicket(LD_TRAFFIC_TICKET traffic_ticket)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(traffic_ticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_TRAFFIC_TICKET>().Where(it => it.SEAT_TYPE == traffic_ticket.SEAT_TYPE && it.VEHICLE_ID==traffic_ticket.VEHICLE_ID ).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetTrafficTicket), new { seat_type = traffic_ticket.SEAT_TYPE,vehicle_id=traffic_ticket.VEHICLE_ID }, traffic_ticket);
        }


        // PUT: api/TrafficTicket/5
        [HttpPut("{vehicleId_seatId}")]
        public async Task<IActionResult> PutTrafficTicket(string vehicleId_seatId, LD_TRAFFIC_TICKET traffic_ticket)
        {
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_type = para[1];
            if (vehicle_id != traffic_ticket.VEHICLE_ID || seat_type!=traffic_ticket.SEAT_TYPE)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(traffic_ticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_TRAFFIC_TICKET>().Where(it => it.SEAT_TYPE == seat_type && it.VEHICLE_ID == vehicle_id).Any())
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

        // DELETE: api/TrafficTicket/5
        [HttpDelete("{vehicleId_seatId}")]
        public async Task<IActionResult> DeleteTrafficTicket(string vehicleId_seatId)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = vehicleId_seatId.Split(new char[] { '&' });
            string vehicle_id = para[0];
            string seat_type = para[1];
            var res = await db.Queryable<LD_TRAFFIC_TICKET>().Where(it => it.SEAT_TYPE == seat_type && it.VEHICLE_ID==vehicle_id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_TRAFFIC_TICKET>().Where(it => it.SEAT_TYPE == seat_type && it.VEHICLE_ID == vehicle_id).ExecuteCommand());
            return NoContent();
        }
    }
}
