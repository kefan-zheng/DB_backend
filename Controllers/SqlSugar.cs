using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;

namespace LvDao.Controllers
{
    public class SqlSugar
    {
        /// 读取json配置文件
        private static IConfigurationRoot Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

        /// 读取配置文件下的连接字符串
        readonly string connectionString = Configuration.GetSection("ConnectionStrings").GetSection("Oracle").Value;

        public SqlSugarClient GetInstance()
        {
            SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                //数据库类型
                DbType = DbType.Oracle,
                IsAutoCloseConnection = true,
            }
                );
            return db;
        }
    }
}
