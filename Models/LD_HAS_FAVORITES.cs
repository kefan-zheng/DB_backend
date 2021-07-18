using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_HAS_FAVORITES
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string FAVOR_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { get; set; }
    }
}
