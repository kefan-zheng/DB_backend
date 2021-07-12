using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_ROOM_TYPE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string TYPE_ID { get; set; }
        public string TYPE_NAME { get; set; }
        public int AREA { get; set; }
    }
}
