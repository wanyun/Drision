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
        ISysUserService _userInfoService = new SysUserService();

        public ActionResult Index()
        {
            var genres = _userInfoService.LoadEntities(n => n.DelFlag == 0);
            return View(genres);
        }



        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        public ActionResult Register()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Register(SysUser userinfo)
        {
            if (ModelState.IsValid)
            {
                //给表中的默认字段赋值
                userinfo.CreateTime = DateTime.Now;
                //在这里需要用到枚举类型，不要写0
                userinfo.DelFlag = 0;// 使用枚举值(short)DelFlagEnum.Normal;
                _userInfoService.AddEntity(userinfo);
                 return RedirectToAction("Index");  
            }
            return View(userinfo);
        }

        public ActionResult Edit(int id)
        {
            var userinfo = _userInfoService.LoadEntities(n => n.UserID == id).First<SysUser>();
            return View(userinfo);
        }

        [HttpPost]
        public ActionResult Edit(SysUser userinfo)
        {
            if (ModelState.IsValid)
            {
                bool isOK=_userInfoService.UpdateEntity(userinfo);
                if (isOK)
                    return RedirectToAction("Index");
            }
            return View(userinfo);
        }

        public ViewResult Details(int id)
        {
            var userinfo = _userInfoService.LoadEntities(n => n.UserID == id).First<SysUser>();
            return View(userinfo);
        }

        //
        // GET: /StoreManager/Delete/5

        public ActionResult Delete(int id)
        {

            var userinfo = _userInfoService.LoadEntities(n => n.UserID == id).First<SysUser>();
            bool isOK = _userInfoService.DeleteEntity(userinfo);
            return RedirectToAction("Index");
        }


    }
}
