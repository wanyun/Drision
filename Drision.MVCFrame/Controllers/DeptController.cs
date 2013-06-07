using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drision.MVCFrame.Models;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Controllers
{
    public class DeptController : BaseController
    {
        //
        // GET: /Dept/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Create(SysDepartment deptInfo)
        {
            
            deptInfo.CreateTime = DateTime.Now;
            deptInfo.CreateUserId = 1;
            deptInfo.OwnerId = 1;
            deptInfo.sortNum = 0;
            db.SysDepartments.Add(deptInfo);
            db.SaveChanges();
            //生成层级码
            var upDept = db.SysDepartments.Find(deptInfo.ID);
            if (deptInfo.Parent_ID.HasValue)
            {
                var parentDept = db.SysDepartments.Find(deptInfo.Parent_ID);
                upDept.SystemLevelCode = string.Format("{0}{1}-", parentDept.SystemLevelCode, upDept.ID);
            }
            upDept.SystemLevelCode = string.Format("{0}-", upDept.ID);
            db.SaveChanges();
            return Content("OK");
        }


        public ActionResult Edit()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Edit(SysDepartment deptInfo)
        {

            var dept = db.SysDepartments.Find(deptInfo.ID);
            dept.UpdateTime = DateTime.Now;
            dept.UpdateUserId = 1;
           
            dept.Department_Name = deptInfo.Department_Name;
            dept.Department_Code = deptInfo.Department_Code;
            dept.Parent_ID = deptInfo.Parent_ID;
            dept.SystemLevelCode = deptInfo.SystemLevelCode;

            db.SaveChanges();
            return Content("OK");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var deptInfo = db.SysDepartments.Find(id);
            db.SysDepartments.Remove(deptInfo);
            db.SaveChanges();
            return Content("OK");
        }



        [HttpPost]
        public JsonResult TreeView()
        {
            var result = from dept in db.SysDepartments
                         select new { id = dept.ID, pId = dept.Parent_ID, name = dept.Department_Name, open = true };
            return Json(result.ToList());
        }

        public ActionResult TreeView(int id)
        {
            return PartialView();
        }

        [HttpPost]
        public JsonResult DeptRetrieve(int id)
        {
            SysDepartment dept = db.SysDepartments.Find(id);

            SysDepartment pdept = (dept.Parent_ID <= 0) ? null : db.SysDepartments.Find(dept.Parent_ID);

            var result = new
            {
                deptId = dept.ID,
                deptName = dept.Department_Name,
                deptCode = dept.Department_Code,
                pid = dept.Parent_ID,
                PName = (pdept != null) ? pdept.Department_Name : "无",
                lvlcode = (pdept != null) ? pdept.ID + "-" : "0-"
            };
            return Json(result);
        }
    }
}
