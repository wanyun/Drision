using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.Models;

namespace Drision.MVCFrame.FrameWork
{
    public class BaseController : Controller
    {
        public MVCFrameContext db = new MVCFrameContext();
        public GenericManager DataHelper;
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            db.Dispose();
        }
        internal static BaseUser[] users { get; set; }
        internal static SysRole[] roles { get; set; }
        internal static SysFunction[] actions { get; set; }
        internal static SysEntity_SysRole[] EntitySysRole { get; set; }
        private static string _lock { get; set; }
        public static void InitializeStaticData()
        {
            lock (_lock)
            {
                using (MVCFrameContext db = new MVCFrameContext())
                {
                    EntitySysRole = db.SysEntity_SysRoles.Include("SysEntity").Include("SysRole").ToArray();
                    users = db.BaseUsers.AsNoTracking().Include("SysFunctions").Include("SysRoles").ToArray();
                    roles = db.SysRoles.AsNoTracking().Include("SysFunctions").Include("BaseUsers").ToArray();
                    actions = db.SysFunctions.AsNoTracking().Include("BaseUsers").Include("SysRoles").ToArray();
                }
            }
        }
        static BaseController()
        {
            _lock = "yaoshi";
            System.Data.Entity.Database.SetInitializer(new SampleData());
            InitializeStaticData();
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext); 
            int userId = Convert.ToInt32(filterContext.HttpContext.Session["UID"]);
            var user = BaseController.users.Where(p => p.ID == userId).SingleOrDefault();

            DataHelper = new GenericManager(db, user);
        }

        public bool success(string controller, string action)
        {
            //最高账号  不进行判断
            if ((string)HttpContext.Session["UName"] == "admin")
                return true;
            if (HttpContext.Session["UID"] == null)
                return false;
            int userid = Convert.ToInt32(HttpContext.Session["UID"]);
            var user = users.Where(p => p.ID == userid).Single();
            int[] rolesId = user.SysRoles.Select(p => p.ID).ToArray();
            var actionss = roles.Where(p => rolesId.Contains(p.ID)).Select(p => p.SysFunctions).ToArray();
            List<SysFunction> actiontemp = new List<SysFunction>();
            foreach (var v in actionss)
            {
                foreach (var t in v)
                {
                    actiontemp.Add(t);
                }
            }
            ViewBag.currentUserAction = actiontemp.ToArray();
            return (from r in (SysFunction[])ViewBag.currentUserAction
                    where r.ActionName == action
                    && r.ControllerName == controller
                    select r).ToList().Count > 0;
        }
    }
}