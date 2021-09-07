using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_PORTRAIT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]

        public string USER_ID { get; set; }
        public byte[] PIC { get; set; }
    }
}
