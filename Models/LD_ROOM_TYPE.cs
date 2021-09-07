using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_ROOM_TYPE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int ORIGINAL_PRICE { get; set; }
        public string ROOM_NAME { get; set; }
        public int CUSTOMER_NUM { get; set; }
        public string BED { get; set; }
        public string DISH { get; set; }
        public string SMOKE { get; set; }
        public string WINDOW { get; set; }
        public string CANCEL { get; set; }
        public int PRICE { get; set; }
        public string COVER_IMG_URL { get; set; }
    }
}
