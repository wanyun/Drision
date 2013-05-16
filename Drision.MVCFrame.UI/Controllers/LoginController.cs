using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.IBLL;
using Drision.MVCFrame.BLL;
using Drision.MVCFrame.Model;
using Drision.MVCFrame.Common;
using System;

namespace Drision.MVCFrame.UI.Controllers
{
    public class LoginController : Controller
    {
        //实例化需要的对象
        ISysUserService _userInfoService = new SysUserService();


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
        public ActionResult CheckUserInfo(SysUser userInfo, string Code)
        {

            //首先我们拿到系统的验证码

            string sessionCode = Session["ValidateCode"].ToString();
            //this.TempData["ValidateCode"] == null ? new Guid().ToString() : this.TempData["ValidateCode"].ToString();

            //然后我们就将验证码去掉，避免了暴力破解
            this.TempData["ValidateCode"] = new Guid();

            //判断用户输入的验证码是否正确
            if (!sessionCode.ToLower().Equals(Code.ToLower()))
            {
                return Content("验证码输入不正确");
            }

            //调用业务逻辑层（BLL）去校验用户是否正确

            var loginUserInfo = _userInfoService.CheckUserInfo(userInfo);

            if (loginUserInfo != null)
            {
                return Content("OK");
            }
            else
            {
                return Content("用户名密码错误");
            }
        }
    }
}
