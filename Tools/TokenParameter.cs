using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LvDao.Tools
{
    public class TokenParameter
    {
        public const string Issuer = "LvDao";//颁发者
        public const string Audience = "yyy";//接收者
        public const string Secret = "qwertyuiopasdfghjklzxcvbnm";//签名密钥
        public const int AccessExpiration = 30;//Token过期时间
    }
}
