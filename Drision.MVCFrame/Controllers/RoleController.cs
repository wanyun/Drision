using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;
using System.Data.Entity.Infrastructure;
using System.Data;
using Drision.MVCFrame.WebControls;


namespace Drision.MVCFrame.Controllers
{
    public class RoleController : BaseController
    {


        //
        // GET: /Role/
        [AOPFilter]
        public ActionResult Index()
        {
            var roles = db.SysRoles.Where(t => t.DelFlag == false).ToList();
            return View(roles);
        }
        [HttpGet, AOPFilter]
        public ViewResult Search(string sortOrder, string searchString, int page = 1)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.indexSortParm = sortOrder == "asc" ? "desc" : "asc";
            var users = from s in db.BaseUsers
                        select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    users = users.OrderByDescending(s => s.UserName);
                    break;
                case "Date":
                    users = users.OrderBy(s => s.CreateTime);
                    break;
                case "Date desc":
                    users = users.OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    users = users.OrderBy(s => s.UserName);
                    break;
            }
            int pageSize = 2;
            int pageIndex = page;
            ViewBag.Filter = searchString;
            return View(users.ToPagedList(pageIndex, pageSize));

        }
        [HttpPost, AOPFilter]
        public ViewResult Search(string sortOrder, string searchString)
        {
            return Search(sortOrder, searchString, 1);
        }
        //
        // GET: /Role/Details/5

        public ActionResult Details(int id)
        {
            var roleInfo = db.SysRoles.Find(id);
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

            if (ModelState.IsValid)
            {
                db.SysRoles.Add(roleInfo);
                    roleInfo.sysEntity_SysRoles = new List<SysEntity_SysRole>();
                foreach (var entity in db.SysEntities)
                {
                    roleInfo.sysEntity_SysRoles.Add(new SysEntity_SysRole()
                    {
                        SysEntity = entity,
                    });
                }
                db.SaveChanges();
                BaseController.InitializeStaticData();
                return RedirectToAction("Index", "Role");
            }
            return View(roleInfo);
        }

        //
        // GET: /Role/Edit/5

        public ActionResult Edit(int id)
        {
            var roleInfo = db.SysRoles.Find(id);
            return View(roleInfo);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost]
        public ActionResult Edit(SysRole roleInfo)
        {
            if (ModelState.IsValid)
            {

                DbEntityEntry<SysRole> entry = db.Entry<SysRole>(roleInfo);
                entry.State = EntityState.Unchanged;
                entry.Property("RoleName").IsModified = true;
                entry.Property("RoleDescription").IsModified = true;
                entry.Property("RoleOrder").IsModified = true;
                db.SaveChanges();
                BaseController.InitializeStaticData();
                return RedirectToAction("Index", "Role");
            }
            return View(roleInfo);

        }

        //
        // POST: /Role/Delete/5

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var roleInfo = db.SysRoles.Find(id);
            DbEntityEntry<SysRole> entry = db.Entry<SysRole>(roleInfo);
            entry.State = EntityState.Unchanged;
            entry.Property("DelFlag").IsModified = true;
            bool delStatus = db.SaveChanges() > 0;

            BaseController.InitializeStaticData();
            // Display the confirmation message
            var results = new
            {
                Message = Server.HtmlEncode(roleInfo.RoleName) + (delStatus ? " 已被删除" : "未成功"),
                DeleteId = roleInfo.ID,
                ItemCount = delStatus ? 1 : 0
            };
            return Json(results);
        }
    }
}
