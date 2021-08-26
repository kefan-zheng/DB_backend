using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_PIC
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]

        public string PIC_ID { get; set; }
        public byte[] PICTURE { get; set; }

    }
}
