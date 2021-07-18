using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_OFFER_ROOM
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string HOTEL_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ROOM_ID { get; set; }
        public string TYPE_ID { get; set; }
        public int PRICE { get; set; }
    }
}
