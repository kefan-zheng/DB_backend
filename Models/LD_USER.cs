using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvDao.Models
{
    public class LD_USER
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string ID_NUMBER { get; set; }
        public string TELE_NUMBER { get; set; }
        public string MAILBOX_ID { get; set; }
        public string UPROFILE { get; set; }
        public string UPASSWORD { get; set; }
        public string GENDER { get; set; }
        public string ULOCATION { get; set; }
        public string MOTTO { get; set; }
    }
}
