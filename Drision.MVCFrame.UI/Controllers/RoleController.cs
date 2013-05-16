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
    public class RoleController : Controller
    {

        //实例化需要的对象
        ISysRoleService _roleInfoService = new SysRoleService();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            var roles = _roleInfoService.LoadEntities(n => n.DelFlag == 0);
            return View(roles);
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            var roleInfo = _roleInfoService.LoadEntities(n => n.RoleID == id).First();
            return View(roleInfo);
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
        public ActionResult Create(SysRole roleInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _roleInfoService.AddEntity(roleInfo);
                    return RedirectToAction("Index","Role");
                }
                return View(roleInfo);
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
            var roleInfo = _roleInfoService.LoadEntities(n => n.RoleID == id).First();
            return View(roleInfo);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(SysRole roleInfo)
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    bool isOK = _roleInfoService.UpdateEntity(roleInfo);
                    if (isOK)
                        return RedirectToAction("Index", "Role");
                }
                return View(roleInfo);
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
            var roleInfo = _roleInfoService.LoadEntities(n => n.RoleID == id).First();
            bool isOK = _roleInfoService.DeleteEntity(roleInfo);
            return RedirectToAction("Index", "Role");
        }

        //
        // POST: /Role/Delete/5

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
