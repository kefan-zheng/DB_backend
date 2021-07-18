using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using SqlSugar;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrafficTicketTypeController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_TICKET_TYPE>>> GetTrafficTicketType()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_TRAFFIC_TICKET_TYPE>().ToListAsync();
        }

        // GET: api/TrafficTicketType/5
        [HttpGet("{id_type}")]
        public async Task<ActionResult<IEnumerable<LD_TRAFFIC_TICKET_TYPE>>> GetTrafficTicketType(string id_type)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            string[] para = id_type.Split(new char[] { '&' });
            string id = para[0];
            string type = para[1];
            var exp = Expressionable.Create<LD_TRAFFIC_TICKET_TYPE>()
                .And(it => it.COMPANY_ID  == id)
                .And(it => it.SEAT_TYPE == type).ToExpression();
            var res = await db.Queryable<LD_TRAFFIC_TICKET_TYPE>().Where(exp).ToListAsync();
            //var res = await db.Queryable<LD_TRAFFIC_TICKET_TYPE>().Where(it=> it.COMPANY_ID==id && it.SEAT_TYPE==type).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/TrafficTicketType
        [HttpPost]
        public async Task<ActionResult<LD_TRAFFIC_TICKET_TYPE>> PostTrafficTicketType(LD_TRAFFIC_TICKET_TYPE ticket_type)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(ticket_type).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_TRAFFIC_TICKET_TYPE>().Where(it => it.COMPANY_ID == ticket_type.COMPANY_ID && it.SEAT_TYPE==ticket_type.SEAT_TYPE).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetTrafficTicketType), new { id = ticket_type.COMPANY_ID,type=ticket_type.SEAT_TYPE }, ticket_type);
        }


        // PUT: api/TrafficTicketType/5
        [HttpPut("{id_type}")]
        public async Task<IActionResult> PutTrafficTicketType(string id_type, LD_TRAFFIC_TICKET_TYPE ticket_type)
        {
            string[] para = id_type.Split(new char[] { '&' });
            string id = para[0];
            string type = para[1];
            if (id != ticket_type.COMPANY_ID || type!=ticket_type.SEAT_TYPE)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(ticket_type).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_TRAFFIC_TICKET_TYPE>().Where(it => it.COMPANY_ID == id && it.SEAT_TYPE==type).Any())
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

        // DELETE: api/TrafficTicketType/5
        [HttpDelete("{id_type}")]
        public async Task<IActionResult> DeleteTrafficTicketType(string id_type)
        {
            string[] para = id_type.Split(new char[] { '&' });
            string id = para[0];
            string type = para[1];
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_TRAFFIC_TICKET_TYPE>().Where(it => it.COMPANY_ID == id && it.SEAT_TYPE ==type ).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_TRAFFIC_TICKET_TYPE>().Where(it => it.COMPANY_ID == id && it.SEAT_TYPE == type).ExecuteCommand());
            return NoContent();
        }
    }
}
