using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_HOTEL
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string HOTEL_ID { get; set; }
        public string HOTEL_NAME { get; set; }
        public string HLOCATION { get; set; }
        public string PICTURE { get; set; }
        public int STAR{ get; set; }
        public int LOWEST_PRICE { get; set; }
        public string HPASSWORD { get; set; }
        public string LABEL { get; set; }
        public string OPEN_TIME { get; set; }
        public int TOTAL_NUM { get; set; }
        public string TELEPHONE { get; set; }
        public string DESCRIPTION { get; set; }
        public int IS_CHECK { get; set; }
    }
}
