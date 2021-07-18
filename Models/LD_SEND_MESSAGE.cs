using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_SEND_MESSAGE
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ADMINISTRATOR_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string MAILBOX_ID { get; set; }
        public string SEND_TIME { get; set; }
        public string MAIL_ID { get; set; }
    }
}
