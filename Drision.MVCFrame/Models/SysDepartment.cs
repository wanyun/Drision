using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Models
{

    [ModelRoleAttribute(Name = "部门表")]
    public class SysDepartment: BaseModel
    {
        [Required(ErrorMessage = "请填写部门编码")]
        public string Department_Code { get; set; }
        [Required(ErrorMessage = "请填写部门名")]
        public string Department_Name { get; set; }
        public int? Parent_ID { get; set; }
        public string SystemLevelCode { get; set; }
        public int  sortNum  { get; set; }
        public virtual ICollection<BaseUser> BaseUsers { get; set; }
    }
}