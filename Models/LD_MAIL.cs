using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_MAIL
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string MAIL_ID { get; set; }
        public string MESSAGE { get; set; }
    }
}
