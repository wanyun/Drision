using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;
using System.Data;
using System.Data.Entity.Infrastructure;

namespace Drision.MVCFrame.Controllers
{
    public class EntityController : BaseController
    {
        // GET: /Entity/
        [AOPFilter, HttpGet]
        public ActionResult Index(int roleId = -1)
        {
            var roles = db.SysRoles.Select(p => new { p.ID, p.RoleName })
                .ToList();
            ViewBag.Roles = new SelectList(roles, "ID", "RoleName", roleId);
            List<SelectListItem> sl = new List<SelectListItem>();
            sl.Add(new SelectListItem() { Text = "无权限", Value = "0" });
            sl.Add(new SelectListItem() { Text = "个人", Value = "1" });
            sl.Add(new SelectListItem() { Text = "部门", Value = "2" });
            sl.Add(new SelectListItem() { Text = "部门及子部门", Value = "3" });
            sl.Add(new SelectListItem() { Text = "全部权限", Value = "4" });
            ViewBag.temp = sl;
            var role = roleId == -1 ? db.SysRoles.Include("sysEntity_SysRoles").Include("sysEntity_SysRoles.SysEntity").FirstOrDefault()
                : db.SysRoles.Include("sysEntity_SysRoles").Include("sysEntity_SysRoles.SysEntity").Where(p => p.ID == roleId).FirstOrDefault();
            return View(role.sysEntity_SysRoles.ToArray());
        }
        [AOPFilter, HttpPost]
        public ActionResult Index(int roleId,List<SysEntity_SysRole> sysentity_SysRole)
        {
                foreach (var t in sysentity_SysRole)
                {
                    t.UpdateTime = DateTime.Now;
                    db.SysEntity_SysRoles.Attach(t);
                    var stateEntry = ((IObjectContextAdapter)db).ObjectContext.
            ObjectStateManager.GetObjectStateEntry(t);
                    stateEntry.SetModifiedProperty("Add");
                    stateEntry.SetModifiedProperty("Delete");
                    stateEntry.SetModifiedProperty("Update");
                    stateEntry.SetModifiedProperty("Search");
                    stateEntry.SetModifiedProperty("UpdateTime");
                }
                db.SaveChanges();
                return RedirectToAction("Index", new { roleId = roleId });
        }
    }
}
