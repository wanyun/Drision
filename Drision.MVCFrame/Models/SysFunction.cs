using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Models
{
    [ModelRoleAttribute(Name = "菜单表")]
    public class SysFunction : BaseModel
    {
        [Required(ErrorMessage = "请填写Controller名")]
        public string ControllerName { get; set; }
        [Required(ErrorMessage = "请填写Action名")]
        public string ActionName { get; set; }
        [Required(ErrorMessage = "请填写Menu名")]
        public string MenuName { get; set; }
        public virtual ICollection<SysRole> SysRoles { get; set; }
        public virtual ICollection<BaseUser> BaseUsers { get; set; }
        public virtual SysFunction ParentFunction { get; set; }
        [ForeignKey("ParentFunction")]
        public int? ParentFunction_Id { get; set; }
        public bool IsParent { get; set; }
    }
}