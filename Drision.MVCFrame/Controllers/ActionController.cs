using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.FrameWork;
using Drision.MVCFrame.Models;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Reflection;

namespace Drision.MVCFrame.Controllers
{
    public class ActionController : BaseController
    {
        // GET: /Role/
        [AOPFilter]
        public ActionResult Index()
        {
            var actions = db.SysFunctions.OrderBy(p => p.IsParent).ToList();
            return View(actions);
        }

        // GET: /Role/Details/5
        [AOPFilter]
        public ActionResult Details(int id)
        {
            var actionInfo = db.SysFunctions.Find(id);
            return View(actionInfo);
        }

        //
        // GET: /Role/Create
        [AOPFilter]
        public ActionResult Create()
        {
            List<String> controlersName = GetControllerNames();
            controlersName.Remove("Base");
            ViewBag.ControlNames = new SelectList(controlersName);
            ViewBag.ActionNames = new SelectList(GetSubMethods(Type.GetType("Drision.MVCFrame.Controllers." + controlersName[0] + "Controller")));
            ViewBag.ParentFunc = new SelectList(BaseController.actions.Where(p => p.IsParent), "ID", "MenuName");
            return View();
        }
        public JsonResult GetActions(string controlName)
        {
            var methods = GetSubMethods(Type.GetType("Drision.MVCFrame.Controllers." + controlName + "Controller"));
            return Json(methods, JsonRequestBehavior.AllowGet);
        }
        //
        // POST: /Role/Create

        [HttpPost, AOPFilter]
        public ActionResult Create(SysFunction actionInfo)
        {
            if (ModelState.IsValid || actionInfo.IsParent)
            {
                db.SysFunctions.Add(actionInfo);
                db.SaveChanges();
                BaseController.InitializeStaticData();
                return RedirectToAction("Index", "Action");
            }
            return View(actionInfo);
        }

        //
        // GET: /Role/Edit/5
        [AOPFilter]
        public ActionResult Edit(int id)
        {
            var actionInfo = db.SysFunctions.Find(id);
            List<String> controlersName = GetControllerNames();
            controlersName.Remove("Base");
            ViewBag.ControlNames = new SelectList(controlersName);
            ViewBag.ActionNames = new SelectList(GetSubMethods(Type.GetType("Drision.MVCFrame.Controllers." + controlersName[0] + "Controller")));
            ViewBag.ParentFunc = new SelectList(BaseController.actions.Where(p => p.IsParent), "ID", "MenuName");
            return View(actionInfo);
        }

        //
        // POST: /Role/Edit/5

        [HttpPost, AOPFilter]
        public ActionResult Edit(SysFunction actionInfo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var oldaction = db.SysFunctions.Find(actionInfo.ID);
                    ////先将实体附加到实体上下文中
                    //db.SysFunctions.Attach(actionInfo);
                    ////手动修改实体的状态
                    //db.SysFunctions.ObjectStateManager.ChangeObjectState(actionInfo, EntityState.Modified);
                    ////保存回数据库
                    //db.SaveChanges();

                    DbEntityEntry<SysFunction> entry = db.Entry<SysFunction>(actionInfo);
                    entry.State = EntityState.Unchanged;
                    entry.Property(p => p.ControllerName).IsModified = true;
                    entry.Property(p => p.ActionName).IsModified = true;
                    entry.Property(p => p.MenuName).IsModified = true;
                    entry.Property(p => p.IsParent).IsModified = true;
                    db.SaveChanges();
                    BaseController.InitializeStaticData();
                    return RedirectToAction("Index", "Action");
                }
                return View(actionInfo);
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Menu()
        {
            int userId = Convert.ToInt32(Session["UID"]);
            List<SysFunction> ac = new List<SysFunction>();
            if (Session["UName"].Equals("admin"))
            {
                ac = BaseController.actions.ToList();
            }
            else
            {
                var actionInfo = BaseController.users.Single(p => p.ID == userId).SysRoles.Select(p => p.SysFunctions).ToList();
                actionInfo.ForEach(p => ac.AddRange(p));
            }
            return PartialView(ac);
        }

        [HttpPost,AOPFilter]
        public ActionResult Delete(int id)
        {
            var actionInfo = db.SysFunctions.Find(id);
            DataHelper.Delete(actionInfo);
            bool delStatus = db.SaveChanges() > 0;
            //重新加载内存中的缓存
            BaseController.InitializeStaticData();
            // Display the confirmation message
            var results = new
            {
                Message = Server.HtmlEncode(actionInfo.MenuName) + (delStatus ? " 已被删除" : "未成功"),
                DeleteId = actionInfo.ID,
                ItemCount = delStatus ? 1 : 0
            };
            return Json(results);
        }
        

        public List<string> GetControllerNames()
        {

            List<string> controllerNames = new List<string>();

            foreach (var t in GetSubClasses<Controller>())
            {
                controllerNames.Add(t.Name.Remove(t.Name.IndexOf("Controller")));
            }

            return controllerNames;

        }
        //获取某一个程序集下所有的类
        private static List<Type> GetSubClasses<T>()
        {

            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();

        }
        //获取Controller下的action
        private static List<string> GetSubMethods(Type t)
        {
            return t.GetMethods().Where(m => (m.ReturnType == typeof(ActionResult) || m.ReturnType == typeof(ViewResult)) && m.IsPublic == true).Select(p => p.Name).Distinct().ToList();
        }
    }
}
