using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_ATTRACTION
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ATTRACTION_ID { get; set; }
        public string ATTRACTION_NAME { get; set; }
        public string ALOCATION { get; set; }
        public string PICTURE { get; set; }
        public string OPEN_TIME { get; set; }
        public string CLOSE_TIME { get; set; }
        public int STAR { get; set; }
        public int PRICE { get; set; }
        public string LABEL { get; set; }
    }
}
