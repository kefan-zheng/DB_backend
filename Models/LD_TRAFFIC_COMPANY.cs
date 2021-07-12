using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_TRAFFIC_COMPANY
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string COMPANY_ID { set; get; }
        public string COMPANY_NAME { set; get; }

    }
}
