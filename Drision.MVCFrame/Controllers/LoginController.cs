using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;
using Drision.MVCFrame.Common;
using System.Collections.Generic;
using System.Reflection;

namespace Drision.MVCFrame.Controllers
{
    public class LoginController : BaseController
    {


        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 验证码的实现
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckCode()
        {

            //首先实例化验证码的类
            ValidateCode validateCode = new ValidateCode();

            //生成验证码指定的长度
            string code = validateCode.CreateVerifyCode(4);

            //将验证码赋值给Session变量
            Session["ValidateCode"] = code;

            //创建验证码的图片
            byte[] bytes = validateCode.CreateImageOnPage(code);

            //最后将验证码返回
            return File(bytes, @"image/jpeg");

        }

        //判断用户输入的信息是否正确
        [HttpPost]
        public ActionResult CheckUserInfo(BaseUser userInfo, string Code)
        {

            //首先我们拿到系统的验证码
            string sessionCode = Session["ValidateCode"].ToString();

            //判断用户输入的验证码是否正确
            if (!sessionCode.ToLower().Equals(Code.ToLower()))
            {
                return Content("验证码输入不正确");
            }

            //调用业务逻辑层（BLL）去校验用户是否正确
            string md5PW = MyMD5Helper.GetMD532(userInfo.PassWord);
            var loginUserInfo = db.BaseUsers.Where(p => p.UserName == userInfo.UserName && p.PassWord.Equals(md5PW)).FirstOrDefault();

            if (loginUserInfo != null)
            {
                loginUserInfo.LastLoginTime = DateTime.Now;
                db.SaveChanges();
                //用户信息和状态
                Session["UID"] = loginUserInfo.ID;
                Session["UName"] = loginUserInfo.UserName;
                Session["Status"] = "Online";
                return Content("OK");
            }
            else
            {
                return Content("用户名密码错误");
            }
        }

   
        public ActionResult LogStatus()
        {
            int id=0;
            if(Session["UID"]==null)
            {
                id=0;
            }
            else
            {
                int.TryParse(Session["UID"].ToString(), out id);
            }
            return PartialView(DataHelper._currentUser);
        }


        [HttpPost]
        public ActionResult Logout(int id)
        {
            try
            {
                Session["Status"] = "Offline";
                Session.Abandon();
                return Content("OK");
            }
            catch (Exception)
            {
                return Content("注销失败！");
            }
 
        }
    }
}
