using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_HOTEL_LICENSE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]

        public string HOTEL_ID { get; set; }
        public byte[] PIC { get; set; }
    }
}

