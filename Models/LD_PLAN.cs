using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_PLAN
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { get; set; }
        public int PLAN_ID { get; set; }
        public string PLAN { get; set; }
        public int PLAN_STAR { get; set; }
    }
}
