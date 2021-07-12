using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_MOMENT
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string MOMENT_ID { set; get; }
        public string MOMENT_TIME { set; get; }
        public string MOMENT_LOCATION { set; get; }
        public string TEXT { set; get; }
        public string PICTURE { set; get; }
        public string VEDIO { set; get; }
    }
}
