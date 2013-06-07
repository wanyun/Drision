using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Models
{
    [ModelRoleAttribute(Name = "用户表")]
    public class BaseUser : BaseModel
    {
        [Required(ErrorMessage = "请填写用户名")]
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string PassWord { get; set; }
        public string Email { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool DelFlag { get; set; }
        public virtual ICollection<SysFunction> SysFunctions { get; set; }
        public virtual ICollection<SysRole> SysRoles { get; set; }
        public virtual SysDepartment Department { get; set; }
        [ForeignKey("Department")]
        public int? Department_Id { get; set; }
    }
}