using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_PURCHASE_ATTRACTION_TICKET
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ATTRACTION_ID { get; set; }
        public string ORDER_TIME { get; set; }
        public int PRICE { get; set; }
    }
}
