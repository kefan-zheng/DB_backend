using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_OFFER_TRAFFIC_SERVICE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string VEHICLE_ID { set; get; }
        public string COMPANY_ID { set; get; }
        public string TRAFFIC_TYPE { set; get; }
    }
}
