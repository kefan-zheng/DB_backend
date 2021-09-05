using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_COMMENT_ON_ATTRACTIONS
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ATTRACTION_ID { get; set; }
        public string ACOMMENT_TIME { get; set; }
        public double GRADE { get; set; }
        public string CTEXT { get; set; }
        public string PICTURE { get; set; }
        public string VIDEO { get; set; }
    }
}
