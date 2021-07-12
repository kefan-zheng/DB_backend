using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_VEHICLE_INFO
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string VEHICLE_ID { set; get; }
        public string START_LOCATION { set; get; }
        public string END_LOCATION { set; get; }
        public string START_TIME { set; get; }
        public string END_TIME { set; get; }

    }
}
