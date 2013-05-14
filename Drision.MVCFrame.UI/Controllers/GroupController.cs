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
    public class GroupController : Controller
    {
        //实例化需要的对象
        IGroupService _groupInfoService = new GroupService();

        //
        // GET: /Role/

        public ActionResult Index()
        {
            var groups = _groupInfoService.LoadEntities(n => n.DelFlag == 0);
            return View(groups);
        }

        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            var groupInfo = _groupInfoService.LoadEntities(n => n.GroupID == id).First();
            return View(groupInfo);
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
        public ActionResult Create(SysGroup groupInfo)
        {


            try
            {

                if (ModelState.IsValid)
                {
                    _groupInfoService.AddEntity(groupInfo);
                    return RedirectToAction("Index", "Group");
                }
                return View(groupInfo);
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
            var roleInfo = _groupInfoService.LoadEntities(n => n.GroupID == id).First();
            return View(roleInfo);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(SysGroup groupInfo)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    bool isOK = _groupInfoService.UpdateEntity(groupInfo);
                    if (isOK)
                        return RedirectToAction("Index", "Group");
                }
                return View(groupInfo);
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
            var groupInfo = _groupInfoService.LoadEntities(n => n.GroupID == id).First();
            bool isOK = _groupInfoService.DeleteEntity(groupInfo);
            return RedirectToAction("Index", "Group");
        }
    }
}
