using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_FAVOURITES_CONTENTS
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string FAVOR_ID { get; set;}
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string LINK_ID { get; set; }
        public string MERCHANT_LINK { get; set; }
    }
}
