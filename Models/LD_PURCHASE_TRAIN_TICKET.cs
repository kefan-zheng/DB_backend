using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_PURCHASE_TRAIN_TICKET
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string VEHICLE_ID { set; get; }

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { set; get; }
        public string SEAT_TYPE { set; get; }
        public int ORDER_AMOUNT { set; get; }
        public string ORDER_TIME { set; get; }
        public string TELEPHONE { get; set; }
        public string TRAIN_DATE { get; set; }
        public string START_LOCATION { set; get; }
        public string END_LOCATION { set; get; }
    }
}
