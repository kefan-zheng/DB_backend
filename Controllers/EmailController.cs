using LvDao;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.IO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using LvDao.Tools;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using MimeKit;

//用于注册时邮箱验证
namespace LvDao.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        public static string LogVeriCode = "000000";
        public static string RegVeriCode = "000000";
        public static string FinVeriCode = "000000";

        [HttpPost("{address_type}")]
        public async Task<IActionResult> SendMailCode(string address_type)
        {
            //字符串分割
            string[] para = address_type.Split(new char[] { '&' });
            string address = para[0];
            string type = para[1];
            //收件人邮箱
            string mailName = address;
            //发送的标题
            string title = "验证码";
            //发送的内容
            Random random = new();
            string Vericode = random.Next(100000, 999999).ToString();
            string bobyText = Vericode;
            if (type == "register")
            {
                bobyText = "注册验证码:" + Vericode + "，请勿向任何单位或个人泄露。若非本人操作,请忽略该邮件。感谢您对旅道的支持。".ToString();
                RegVeriCode = Vericode;
            }
            else if(type == "findpwd")
            {
                bobyText = "重置登录密码验证码:" + Vericode + "，请勿向任何单位或个人泄露。若非本人操作，请忽略该邮件。感谢您对旅道的支持。".ToString();
                FinVeriCode = Vericode;
            }
            else if(type == "login")
            {
                bobyText = "邮箱登录验证码:" + Vericode + "，请勿向任何单位或个人泄露，若非本人操作，请忽略该邮件。感谢您对旅道的支持。".ToString();
                LogVeriCode = Vericode;
            }
            // 邮件服务器smtp.163.com表示163邮箱服务器    
            string host = "smtp.163.com";
            // 发送端账号   
            string userName = "kefan_zheng@163.com";
            // 发送端授权码，需要在邮箱获取授权码
            string pwd = "GLGNEEPQQKEMSXMB";
            MimeMessage message = new();
            //发件人
            message.From.Add(new MailboxAddress("旅道", userName));
            //收件人
            message.To.Add(new MailboxAddress(title, mailName));
          

            //标题
            message.Subject = title;
            //正文内容，发送
            message.Body = new BodyBuilder
            {
                HtmlBody = bobyText
            }.ToMessageBody();
            try
            {
                using var client = new MailKit.Net.Smtp.SmtpClient();
                //Smtp服务器
                client.Connect(host, 25, false);
                //登录，发送
                client.Authenticate(userName, pwd);
                await Task.Run(()=> client.Send(message));
                //断开
                client.Disconnect(true);

            }
            catch (Exception)
            {
                throw;
            }
            return Ok("OK");
        }
    }
}
