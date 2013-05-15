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
    public class ActionController : Controller
    {
        //实例化需要的对象
        ISysActionService _userRoleInfoService = new SysActionService();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            var actions = _userRoleInfoService.LoadEntities(n => n.DelFlag == 0);
            return View(actions);
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            var actionInfo = _userRoleInfoService.LoadEntities(n => n.ActionID == id).First();
            return View(actionInfo);
        }

        //
        // GET: /Role/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Role/Create

        [HttpPost]
        public ActionResult Create(SysAction actionInfo)
        {


            try
            {

                if (ModelState.IsValid)
                {
                    _userRoleInfoService.AddEntity(actionInfo);
                    return RedirectToAction("Index", "Action");
                }
                return View(actionInfo);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id)
        {
            var actionInfo = _userRoleInfoService.LoadEntities(n => n.ActionID == id).First();
            return View(actionInfo);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(SysAction actionInfo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool isOK = _userRoleInfoService.UpdateEntity(actionInfo);
                    if (isOK)
                        return RedirectToAction("Index", "Action");
                }
                return View(actionInfo);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Role/Delete/5

        public ActionResult Delete(int id)
        {
            var actionInfo = _userRoleInfoService.LoadEntities(n => n.ActionID == id).First();
            bool isOK = _userRoleInfoService.DeleteEntity(actionInfo);
            return RedirectToAction("Index", "Action");
        }
    }
}
