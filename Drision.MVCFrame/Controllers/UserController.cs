using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.Models;
using Drision.MVCFrame.FrameWork;
using System.Data.Entity.Infrastructure;
using System.Data;
using Drision.MVCFrame.Common;
//using Webdiyer.WebControls.Mvc;
using Drision.MVCFrame.WebControls;

namespace Drision.MVCFrame.Controllers
{
    public class UserController : BaseController
    {
        [HttpGet, AOPFilter]
        public ViewResult Search(string sortOrder, string searchString, int page = 1)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "Name desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "Date desc" : "Date";
            var users = DataHelper.Where<BaseUser>(p =>p.DelFlag!=true);
            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(s => s.UserName.ToUpper().Contains(searchString.ToUpper()));
            }
            switch (sortOrder)
            {
                case "Name desc":
                    break;
                case "Date":
                    users = users.OrderBy(s => s.CreateTime);
                    break;
                case "Date desc":
                    users = users.OrderByDescending(s => s.CreateTime);
                    break;
                default:
                    users = users.OrderByDescending(s => s.CreateTime);
                    break;
            }
            int pageSize = 10;
            int pageIndex = page;
            ViewBag.Filter = searchString;
            return View(users.ToPagedList(pageIndex, pageSize));

        }
        [HttpPost, AOPFilter]
        public ViewResult Search(string sortOrder, string searchString)
        {
            return Search(sortOrder, searchString, 1);
        }
        /// <summary>
        /// 注册用户
        /// </summary>
        /// <param name="userinfo"></param>
        /// <returns></returns>
        [AOPFilter]
        public ActionResult Create()
        {
            var depts = db.SysDepartments.ToList();
            ViewBag.Department = new SelectList(depts, "ID", "Department_Name");
            return View();
        }

        [HttpPost, AOPFilter]
        public ActionResult Create(BaseUser userinfo)
        {
            var depts = db.SysDepartments.ToList();
            ViewBag.Department = new SelectList(depts, "ID", "Department_Name");

            try
            {
                if (ModelState.IsValid)
                {
                    //给表中的默认字段赋值
                    userinfo.CreateTime = DateTime.Now;
                    userinfo.DelFlag = false;
                    userinfo.PassWord = MyMD5Helper.GetMD532(@"1");
                    DataHelper.Add(userinfo);
                    DataHelper.SaveChange();
                    BaseController.InitializeStaticData();
                    //return Search(null, null);
                    return Content(userinfo.ID.ToString());
                }
                else
                {
                    return Content("error");
                }
            }
            catch (Exception)
            {

                return Content("error");
            }
        }
        [AOPFilter]
        public ActionResult Edit(int id)
        {
            var userinfo = db.BaseUsers.Find(id);
            var depts = db.SysDepartments.ToList();
            ViewBag.Department = new SelectList(depts, "ID", "Department_Name", userinfo.Department_Id);
            return View(userinfo);
        }

        [HttpPost, AOPFilter]
        public ActionResult Edit(BaseUser userinfo)
        {
            var depts = db.SysDepartments.ToList();
            ViewBag.Department = new SelectList(depts, "ID", "Department_Name", userinfo.Department_Id);

            try
            {
                if (ModelState.IsValid)
                {
                    var entry = db.BaseUsers.Find(userinfo.ID);
                    entry.Department_Id = userinfo.Department_Id;
                    entry.NickName = userinfo.NickName;
                    entry.Email = userinfo.Email;
                    db.SaveChanges();
                    BaseController.InitializeStaticData();
                    return Content("ok");
                }
                else
                {
                    return Content("error");
                }
            }
            catch (Exception)
            {
                return Content("error");
            }
        }
        [AOPFilter]
        public ViewResult Details(int id)
        {
            var userInfo = db.BaseUsers.Find(id);
            return View(userInfo);
        }

        [HttpPost, AOPFilter]
        public ActionResult Delete(int id)
        {
            var userInfo = db.BaseUsers.Find(id);
            userInfo.DelFlag = true;
            bool delStatus = db.SaveChanges() > 0;
            BaseController.InitializeStaticData();

            // Display the confirmation message
            var results = new
            {
                Message = Server.HtmlEncode(userInfo.UserName) + (delStatus ? " 已被删除" : "未成功"),
                DeleteId = userInfo.ID,
                ItemCount = delStatus ? 1 : 0
            };
            return Json(results);
        }


    }
}
