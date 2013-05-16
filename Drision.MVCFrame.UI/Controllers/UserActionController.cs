using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.IBLL;
using Drision.MVCFrame.BLL;
using System.Dynamic;
using Drision.MVCFrame.Model;

namespace Drision.MVCFrame.UI.Controllers
{
    public class UserActionController : Controller
    {
        //实例化需要的对象
        ISys_R_User_ActionService _userActionService = new Sys_R_User_ActionService();
        ISysUserService _userService = new SysUserService();
        ISysActionService _actionService = new SysActionService();

        public ActionResult Index()
        {
            dynamic viewModel = new ExpandoObject();
            var userActionInfo = _userActionService.LoadEntities(n => n.ID > 0);
            var userInfo = _userService.LoadEntities(n => n.DelFlag == 0);
            var actionInfo = _actionService.LoadEntities(n => n.DelFlag == 0);
            viewModel = userActionInfo.
                Join(userInfo,
                    m => m.UserID,
                    f => f.UserID,
                    (m, f) =>
                        new { ID = m.ID, ActionID = m.ActionID, UserID = m.UserID, UserName = f.UserName }).
                Join(actionInfo,
                    t => t.ActionID,
                    s => s.ActionID,
                    (t, s) =>
                        new { ID = t.ID, ActionID = t.ActionID, UserID = t.UserID, UserName = t.UserName, ActionName = s.ActionName });

            return View(viewModel);
        }


        public ActionResult Details(int id)
        {

            dynamic viewModel = new ExpandoObject();
            var userActionInfo = _userActionService.LoadEntities(n => n.ID == id);
            var userInfo = _userService.LoadEntities(n => n.DelFlag == 0);
            var actionInfo = _actionService.LoadEntities(n => n.DelFlag == 0);
            viewModel = userActionInfo.
                Join(userInfo,
                    m => m.UserID,
                    f => f.UserID,
                    (m, f) =>
                        new { ID = m.ID, ActionID = m.ActionID, UserID = m.UserID, UserName = f.UserName }).
                Join(actionInfo,
                    t => t.ActionID,
                    s => s.ActionID,
                    (t, s) =>
                        new { ID = t.ID, ActionID = t.ActionID, UserID = t.UserID, UserName = t.UserName, ActionName = s.ActionName }).FirstOrDefault();

            return View(viewModel);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Sys_R_User_Action collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _userActionService.AddEntity(collection);
                    return RedirectToAction("Index", "UserAction");
                }
                return View(collection);
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Edit(int id)
        {
            var userAction = _userActionService.LoadEntities(n => n.ID == id).FirstOrDefault();
            return View(userAction);

        }


        [HttpPost]
        public ActionResult Edit(Sys_R_User_Action userAction)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool isOK = _userActionService.UpdateEntity(userAction);
                    if (isOK)
                        return RedirectToAction("Index", "UserAction");
                }
                return View(userAction);
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /RoleAction/Delete/5

        public ActionResult Delete(int id)
        {
            var userAction = _userActionService.LoadEntities(n => n.ID == id).First();
            bool isOK = _userActionService.DeleteEntity(userAction);
            return RedirectToAction("Index", "UserAction");
        }

        //
        // POST: /RoleAction/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
