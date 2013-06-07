using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;

namespace Drision.MVCFrame.Controllers
{
    public class RolePermitController : BaseController
    {
        //
        // GET: /RolePermit/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult AddAction(int id)
        {
            var roleInfo = db.SysRoles.Find(id);
            ViewBag.SelectActions = db.SysFunctions.ToList();
            return View(roleInfo);
        }
        [HttpPost]
        public ActionResult AddAction(SysRole Model, string[] selectActions)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    SysRole roleInfo = db.SysRoles.Find(Model.ID);
                    roleInfo.SysFunctions = new List<SysFunction>();

                    foreach (var item in db.SysFunctions.ToList())
                    {
                        if (selectActions.Contains(item.ID.ToString()))
                        {
                            roleInfo.SysFunctions.Add(item);
                        }
                    }
                    db.SaveChanges();

                }
                return RedirectToAction("Index", "Role");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult EditAction(int id)
        {
            ViewBag.SelectActions = db.SysFunctions.ToList();
            var roleInfo = db.SysRoles.Include("SysFunctions").Where(i => i.ID == id).SingleOrDefault();
            return View(roleInfo);
        }
        [HttpPost]
        public ActionResult EditAction(SysRole Model, string[] selectActions)
        {
            try
            {
                SysRole roleInfo = db.SysRoles.Find(Model.ID);
                string[] beforeSelect = roleInfo.SysFunctions.Select(i => i.ID.ToString()).ToArray();//得到以前选择的
                if (selectActions == null)
                {

                    beforeSelect.ToList().ForEach(n => roleInfo.SysFunctions.Remove(db.SysFunctions.Find(Convert.ToInt32(n))));
                }
                else
                {
                    //思路 比较以前选择的和现在选择的  先取出他们的差集   那么这部分就是可以删除的项  和添加的部分

                    beforeSelect.Except(selectActions).ToList().ForEach(n => roleInfo.SysFunctions.Remove(db.SysFunctions.Find(Convert.ToInt32(n))));
                    selectActions.Except(beforeSelect).ToList().ForEach(n => roleInfo.SysFunctions.Add(db.SysFunctions.Find(Convert.ToInt32(n))));
                }
                db.SaveChanges();
                return RedirectToAction("Index", "Role");

            }
            catch
            {
                return View();
            }
        }
    }
}
