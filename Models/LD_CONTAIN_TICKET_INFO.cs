using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_CONTAIN_TICKET_INFO
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string VEHICLE_ID { set; get; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string SEAT_ID { set; get; }
    }
}
