using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Configuration;

namespace LvDao_Tourism_Info_Management_System.Models
{
    public class DataAccess
    {
        public static OracleConnection DB;

        //建立数据库连接
        public static void CreateConn()
        {
            string user = "system";
            string pwd = "12345678";
            string db = "localhost/orcl";
            string conStringUser = "User ID=" + user + ";password=" + pwd + ";Data Source=" + db + ";";
            DB = new OracleConnection(conStringUser);
            DB.Open();
        }

        //关闭数据库连接
        public static void CloseConn()
        {
            DB.Close();
        }

        //用户操作
        //在User表中查询用户、密码是否错误(登录时使用)
        //密码或用户名错误返回false；密码和用户名正确返回true
        public static bool IsUserExist(string UserID, string Password)
        {
            int Count;
            OracleCommand CMD = DB.CreateCommand();
            CMD.CommandText = "select count(*) from User where UserID=:UserID and UserPassword=:Password";
            CMD.Parameters.Add(new OracleParameter(":UserID", UserID));
            CMD.Parameters.Add(new OracleParameter(":Password", Password));
            Count = Convert.ToInt32(CMD.ExecuteScalar());
            if (Count == 0)
                return false;
            else
                return true;

        }

        //向User表中增加一个新用户(注册)
        //添加成功返回UserID，添加失败返回“0”
        public static string AddUser(string UserID, string UserPassword)
        {
            OracleCommand Insert = DB.CreateCommand();
            Insert.CommandText = "insert into MUser values(:UserID,:UserPassword)";
            Insert.Parameters.Add(new OracleParameter(":UserID", UserID));
            Insert.Parameters.Add(new OracleParameter(":UserPassword", UserPassword));
            int Result = Insert.ExecuteNonQuery();
            if (Result == 1)
            {
                return UserID;
            }
            else return "0";
        }

        //查找个人信息
        public static User FindUserInfo(string UserID)
        {
            User U = new User();
            OracleCommand Search = DB.CreateCommand();
            Search.CommandText = "select * from MUser where UserID=:UserID";
            Search.Parameters.Add(new OracleParameter(":UserID", UserID));
            OracleDataReader Ord = Search.ExecuteReader();
            while (Ord.Read())
            {
                U.UserID = UserID;
                U.UserPWD = Ord.GetValue(1).ToString();
            }
            return U;
        }
    }
}
