using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_ADMINISTRATOR
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ADMINISTRATOR_ID { get; set; }
        public string PASSWORD { get; set; }

    }
}
