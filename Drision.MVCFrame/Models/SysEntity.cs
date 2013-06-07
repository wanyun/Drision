using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Drision.MVCFrame.Models
{
    public class SysEntity : BaseModel
    {
        public string EntityName  { get; set; }
        public string ModelName { get; set; }
        public virtual ICollection<SysEntity_SysRole> sysEntity_SysRoles { get; set; }
    }
}