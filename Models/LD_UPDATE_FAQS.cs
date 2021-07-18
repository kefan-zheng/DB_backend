using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_UPDATE_FAQS
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string ADMINISTRATOR_ID { get; set; }
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string QUESTION_ID { get; set; }
    }
}
