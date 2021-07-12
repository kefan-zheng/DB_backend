using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_COMMENT_MOMENT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { set; get; }
        public string MOMENT_ID { set; get; }
        public string COMMENT_TEXT { set; get; }
        public string COMMENT_TIME { set; get; }
    }
}
