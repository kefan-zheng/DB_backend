using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_BOOK_ROOM
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string HOTEL_ID { get; set; }
        public string ROOM_ID { get; set; }
        public string USER_ID { get; set; }
        public int ORDER_AMOUNT { get; set; }
        public string ORDER_TIME { get; set; }
    }
}
