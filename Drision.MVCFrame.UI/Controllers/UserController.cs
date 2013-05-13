using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.IBLL;
using Drision.MVCFrame.BLL;
using Drision.MVCFrame.Model;

namespace Drision.MVCFrame.UI.Controllers
{
    public class UserController : Controller
    {

        //实例化需要的对象
        IUserService _userInfoService = new UserService();
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public ActionResult Regist(SysUser userinfo)
        {
            //给表中的默认字段赋值
            userinfo.CreateTime = DateTime.Now;

            //在这里需要用到枚举类型，不要写0
            userinfo.DelFlag = 0;// (short)DelFlagEnum.Normal;

            _userInfoService.AddEntities(userinfo);
            return Content("OK");

        }
        
    }
}
