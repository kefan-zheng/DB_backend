using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_AUDIT_MOMENT_INFO
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string MOMENT_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ADMINISTRATOR_ID { get; set; }
        public string MOMENT_AUDIT_RESULT { get; set; }
    }
}
