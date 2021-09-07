using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LvDao.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace LvDao.Controllers
{
    [EnableCors("any")]
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PicController : ControllerBase
    {
        [HttpGet("{id}")]
        public string GetPic(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();

            var table = db.Queryable<LD_PIC>().Where(it => it.PIC_ID == id)
                .ToList();

            if (table.Count == 0)
            {
                return "NULL";
            }

            var pic1 = table[0];

            string pic_str = "data:image/jpg;base64," + Convert.ToBase64String(pic1.PICTURE);

            return pic_str;

        }

        //POST: api/
        [HttpPost("{id}")]
        public async Task<ActionResult<LD_PIC>> PostPic(string id)
        {
            var stream = Request.Form.Files[0].OpenReadStream();

            byte[] byteArray = new byte[6000000];
            for (int i = 0; i < byteArray.Length; i++)
            {
                byteArray[i] = 0;
            }

            int pic_len = Convert.ToInt32(stream.Length);

            stream.Read(byteArray, 0, pic_len);

            LD_PIC pic_test = new LD_PIC();
            pic_test.PICTURE = new byte[pic_len];
            for (int i = 0; i < pic_len; i++)
            {
                pic_test.PICTURE[i] = byteArray[i];
            }

            //pic_test.PIC = stream.Length;

            pic_test.PIC_ID = id;

            SqlSugar c = new();
            var db = c.GetInstance();

            await Task.Run(() => db.Insertable(pic_test).ExecuteCommand());

            return CreatedAtAction(nameof(GetPic), new { }, pic_test);
        }

        // DELETE: api/
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoment(string id)
        {
            SqlSugar c = new();
            var db = c.GetInstance();
            var res = await db.Queryable<LD_PIC>().Where(it => it.PIC_ID == id).ToListAsync();
            if (res == null)
            {
                return NotFound();
            }
            await Task.Run(() => db.Deleteable<LD_PIC>().In(id).ExecuteCommand());
            return NoContent();
        }
    }
}
