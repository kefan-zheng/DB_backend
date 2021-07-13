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

namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("any")]
    public class PurchaseAttractionTicketController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LD_PURCHASE_ATTRACTION_TICKET>>> GetPurchaseAttractionTicket()
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            return await db.Queryable<LD_PURCHASE_ATTRACTION_TICKET>().ToListAsync();
        }

        // GET: api/PurchaseAttractionTicket
        [HttpGet("{userid_attrid}")]
        public async Task<ActionResult<IEnumerable<LD_PURCHASE_ATTRACTION_TICKET>>> GetPurchaseAttractionTicket(string userid_attrid)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_PURCHASE_ATTRACTION_TICKET>().Where(it => it.USER_ID == para[0]&&it.ATTRACTION_ID == para[1]).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }

        //POST: api/PurchaseAttractionTicket
        [HttpPost]
        public async Task<ActionResult<LD_PURCHASE_ATTRACTION_TICKET>> PostPurchaseAttractionTicket(LD_PURCHASE_ATTRACTION_TICKET purchaseattractionticket)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                await Task.Run(() => db.Insertable(purchaseattractionticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (db.Queryable<LD_PURCHASE_ATTRACTION_TICKET>().Where(it => it.USER_ID == purchaseattractionticket.USER_ID&&it.ATTRACTION_ID == purchaseattractionticket.ATTRACTION_ID).Any())
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetPurchaseAttractionTicket), new { userid = purchaseattractionticket.USER_ID,attrid = purchaseattractionticket.ATTRACTION_ID }, purchaseattractionticket);
        }


        // PUT: api/PurchaseAttractionTicket
        [HttpPut("{userid_attrid}")]
        public async Task<IActionResult> PutPurchaseAttractionTicket(string userid_attrid, LD_PURCHASE_ATTRACTION_TICKET purchaseattractionticket)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            if (para[0] != purchaseattractionticket.USER_ID&&para[1] != purchaseattractionticket.ATTRACTION_ID)
            {
                return BadRequest();
            }
            SqlSugar c = new();
            var db = c.GetInstance();
            try
            {
                var result = await Task.Run(() => db.Updateable(purchaseattractionticket).ExecuteCommand());
            }
            catch (Exception)
            {
                if (!db.Queryable<LD_PURCHASE_ATTRACTION_TICKET>().Where(it => it.USER_ID == para[0]&&it.ATTRACTION_ID == para[1]).Any())
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

        // DELETE: api/PurchaseAttractionTicket
        [HttpDelete("{userid_attrid}")]
        public async Task<IActionResult> DeletePurchaseAttractionTicket(string userid_attrid)
        {
            //处理字符串
            string[] para = userid_attrid.Split(new char[] { '&' });
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_PURCHASE_ATTRACTION_TICKET>().Where(it => it.USER_ID == para[0]&&it.ATTRACTION_ID == para[1]).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_PURCHASE_ATTRACTION_TICKET>().Where(it => it.USER_ID == para[0]&&it.ATTRACTION_ID == para[1]).ExecuteCommand());
            return NoContent();
        }
    }
}
