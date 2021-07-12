using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_RELEASE_MOMENT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string MOMENT_ID { set; get; }
        public string USER_ID { set; get; }
    }
}
