using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_TRAFFIC_TICKET //此表在数据库中是单独的表
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string VEHICLE_ID { set; get; }
        public string SEAT_ID { set; get; }
        public string SEAT_TYPE { set; get; }
        public int PRICE { set; get; }
        public string TRADE_STATUS{set;get;}
    }
}
