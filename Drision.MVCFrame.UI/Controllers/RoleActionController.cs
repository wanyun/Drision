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
    public class RoleActionController : Controller
    {
        //实例化需要的对象
        ISys_R_Role_ActionService _roleActionService = new Sys_R_Role_ActionService();
        ISysRoleService _roleInfoService = new SysRoleService();
        ISysActionService _actionRoleInfoService = new SysActionService();

        public ActionResult Index()
        {
            dynamic viewModel = new ExpandoObject();
            var roleActionInfo = _roleActionService.LoadEntities(n => n.ID > 0);
            var roleInfo = _roleInfoService.LoadEntities(n => n.DelFlag == 0);
            var actionInfo = _actionRoleInfoService.LoadEntities(n => n.DelFlag == 0);
            viewModel = roleActionInfo.
                Join(roleInfo,
                    m => m.RoleID,
                    f => f.RoleID,
                    (m, f) =>
                        new { ID = m.ID, ActionID = m.ActionID, RoleID = m.RoleID, RoleName = f.RoleName }).
                Join(actionInfo,
                    t => t.ActionID,
                    s => s.ActionID,
                    (t, s) =>
                        new { ID = t.ID, ActionID = t.ActionID, RoleID = t.RoleID, RoleName = t.RoleName, ActionName = s.ActionName });

            return View(viewModel);
        }


        public ActionResult Details(int id)
        {

            dynamic viewModel = new ExpandoObject();
            var roleActionInfo = _roleActionService.LoadEntities(n => n.ID == id);
            var roleInfo = _roleInfoService.LoadEntities(n => n.DelFlag == 0);
            var actionInfo = _actionRoleInfoService.LoadEntities(n => n.DelFlag == 0);
            viewModel = roleActionInfo.
                Join(roleInfo,
                    m => m.RoleID,
                    f => f.RoleID,
                    (m, f) =>
                        new { ID = m.ID, ActionID = m.ActionID, RoleID = m.RoleID, RoleName = f.RoleName }).
                Join(actionInfo,
                    t => t.ActionID,
                    s => s.ActionID,
                    (t, s) =>
                        new { ID = t.ID, ActionID = t.ActionID, RoleID = t.RoleID, RoleName = t.RoleName, ActionName = s.ActionName }).FirstOrDefault();

            return View(viewModel);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Sys_R_Role_Action collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _roleActionService.AddEntity(collection);
                    return RedirectToAction("Index", "RoleAction");
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
            var roleAction = _roleActionService.LoadEntities(n => n.ID == id).FirstOrDefault();
            return View(roleAction);

        }


        [HttpPost]
        public ActionResult Edit(Sys_R_Role_Action roleAction)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool isOK = _roleActionService.UpdateEntity(roleAction);
                    if (isOK)
                        return RedirectToAction("Index", "RoleAction");
                }
                return View(roleAction);
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
            var roleInfo = _roleActionService.LoadEntities(n => n.ID == id).First();
            bool isOK = _roleActionService.DeleteEntity(roleInfo);
            return RedirectToAction("Index", "RoleAction");
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
