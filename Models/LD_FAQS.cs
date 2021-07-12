using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_FAQS
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string QUESTION_ID { get; set; }
        public string QUESTION_NAME { get; set; }
        public string SOLUTION { get; set; }
    }
}
