using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;

namespace Drision.MVCFrame.Controllers
{
    public class UserPermitController : BaseController
    {
        //
        // GET: /UserPermit/
        [AOPFilter]
        public ActionResult Index()
        {
            return View();
        }
        [AOPFilter]
        public ActionResult AddRole(int id)
        {
            var userInfo = db.BaseUsers.Find(id);
            ViewBag.SelectRoles = db.SysRoles.ToList();
            return View(userInfo);
        }
        [HttpPost,AOPFilter]
        public ActionResult AddRole(BaseUser Model, string[] selectedRoles)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    BaseUser userInfo = db.BaseUsers.Find(Model.ID);
                    userInfo.SysRoles = new List<SysRole>();

                    foreach (var item in db.SysRoles.ToList())
                    {
                        if (selectedRoles.Contains(item.ID.ToString()))
                        {
                            userInfo.SysRoles.Add(item);
                        }
                    }
                    db.SaveChanges();

                }
                return RedirectToAction("Index", "User");

            }
            catch
            {
                return View();
            }
        }
        [AOPFilter]
        public ActionResult EditRole(int id)
        {
            //var userInfo = db.BaseUsers.Find(id);
            ViewBag.SelectRoles = db.SysRoles.ToList();
            var userInfo = db.BaseUsers.Include("SysRoles").Where(i => i.ID == id).SingleOrDefault();
            return View(userInfo);
        }
        [HttpPost,AOPFilter]
        public ActionResult EditRole(BaseUser Model, string[] selectedRoles)
        {
            try
            {
                BaseUser userInfo = db.BaseUsers.Find(Model.ID);

                string[] beforeSelect = userInfo.SysRoles.Select(i => i.ID.ToString()).ToArray();//得到以前选择的
                if (selectedRoles == null)
                {
                    
                    beforeSelect.ToList().ForEach(n => userInfo.SysRoles.Remove(db.SysRoles.Find(Convert.ToInt32(n))));
                }
                else
                {
                    //思路 比较以前选择的和现在选择的  先取出他们的差集   那么这部分就是可以删除的项  和添加的部分
                    beforeSelect.Except(selectedRoles).ToList().ForEach(n => userInfo.SysRoles.Remove(db.SysRoles.Find(Convert.ToInt32(n))));
                    selectedRoles.Except(beforeSelect).ToList().ForEach(n => userInfo.SysRoles.Add(db.SysRoles.Find(Convert.ToInt32(n))));
                   
                }
                db.SaveChanges();
                return RedirectToAction("Search", "User");

            }
            catch
            {
                return View();
            }
        }


        [AOPFilter]
        public ActionResult AddAction(int id)
        {
            var userInfo = db.BaseUsers.Find(id);
            ViewBag.SelectActions = db.SysFunctions.ToList();
            return View(userInfo);
        }
        [HttpPost,AOPFilter]
        public ActionResult AddAction(BaseUser Model, string[] selectActions)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    BaseUser userInfo = db.BaseUsers.Find(Model.ID);
                    userInfo.SysFunctions = new List<SysFunction>();

                    foreach (var item in db.SysFunctions.ToList())
                    {
                        if (selectActions.Contains(item.ID.ToString()))
                        {
                            userInfo.SysFunctions.Add(item);
                        }
                    }
                    db.SaveChanges();

                }
                return RedirectToAction("Index", "User");

            }
            catch
            {
                return View();
            }
        }
        [AOPFilter]
        public ActionResult EditAction(int id)
        {
            ViewBag.SelectActions = db.SysFunctions.ToList();
            var userInfo = db.BaseUsers.Include("SysFunctions").Where(i => i.ID == id).SingleOrDefault();
            return View(userInfo);
        }
        [HttpPost,AOPFilter]
        public ActionResult EditAction(BaseUser Model, string[] selectActions)
        {
            try
            {
                BaseUser userInfo = db.BaseUsers.Find(Model.ID);

                string[] beforeSelect = userInfo.SysFunctions.Select(i => i.ID.ToString()).ToArray();//得到以前选择的
                if (selectActions == null)
                {

                    beforeSelect.ToList().ForEach(n => userInfo.SysFunctions.Remove(db.SysFunctions.Find(Convert.ToInt32(n))));
                }
                else
                {
                    //思路 比较以前选择的和现在选择的  先取出他们的差集   那么这部分就是可以删除的项  和添加的部分
                    beforeSelect.Except(selectActions).ToList().ForEach(n => userInfo.SysFunctions.Remove(db.SysFunctions.Find(Convert.ToInt32(n))));
                    selectActions.Except(beforeSelect).ToList().ForEach(n => userInfo.SysFunctions.Add(db.SysFunctions.Find(Convert.ToInt32(n))));
                }
                db.SaveChanges();
                return RedirectToAction("Index", "User");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult NoPermit()
        {
            return View();
        }



    }
}
