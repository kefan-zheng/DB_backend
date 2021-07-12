using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_FAVORITES
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string FAVOR_ID { get; set; }
        public string FAVOR_NAME { get; set; }
        public int FAVOR_CONTENT_NUM { get; set; }
    }
}
