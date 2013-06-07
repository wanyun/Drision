using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Models
{

    [ModelRole(Name = "角色表")]
    public class SysRole : BaseModel
    {
        [Required(ErrorMessage = "请填写角色名")]
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public int RoleOrder { get; set; }
        public bool DelFlag { get; set; }
        public virtual ICollection<BaseUser> BaseUsers { get; set; }
        public virtual ICollection<SysFunction> SysFunctions { get; set; }
        public virtual ICollection<SysEntity_SysRole> sysEntity_SysRoles { get; set; }
    }
}