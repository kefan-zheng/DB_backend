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
    }
}
