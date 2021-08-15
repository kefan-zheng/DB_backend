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
    public class FunGetAttractionByLabelController : ControllerBase
    {
        [HttpGet("{labels}")]
        public List<dynamic> GetAttractionByAlocation(string labels)
        {
            //处理字符串
            string[] label = labels.Split(new char[] { '_' });
            
            SqlSugar c = new();
            var db = c.GetInstance();
            List<dynamic> res = new();
            List<string> hisid = new();
            for (int i = 0;i<label.Length;i++)
            {
                string tmplabel = label[i];
                var table = db.Queryable<LD_ATTRACTION>()
                .Where(it => it.LABEL.Contains(tmplabel))
                .ToList();


                for (int j = 0; j < table.Count; j++)
                {
                    int nosame = 1;
                    for(int k = 0;k<hisid.Count;k++)
                    {
                        if(table[j].ATTRACTION_ID == hisid[k])
                        {
                            nosame = 0;
                            break;
                        }
                    }
                    if(nosame == 1)
                    {
                        res.Add(table[j]);
                        hisid.Add(table[j].ATTRACTION_ID);
                    }
                }
            }
            return res;
        }
    }
}