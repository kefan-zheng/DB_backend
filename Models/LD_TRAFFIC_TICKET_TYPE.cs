﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SqlSugar;

namespace LvDao.Models
{
    public class LD_TRAFFIC_TICKET_TYPE //此表在数据库中是单独的表
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string COMPANY_ID { set; get; }

        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public string SEAT_TYPE { set; get; }
    }
}
