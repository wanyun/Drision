using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Drision.MVCFrame.Models
{
    public class SysEntity_SysRole : BaseModel
    {
        public SysEntity_SysRole()
        {
            Add = Delete = Update = Search = 0;
        }
        public virtual SysEntity SysEntity { get; set; }
        public virtual SysRole SysRole { get; set; }
        public int Add { get; set; }
        public int Delete { get; set; }
        public int Update { get; set; }
        public int Search { get; set; }
    }

    public enum SysEntity_SysRoleEnum
    {
        /// <summary>
        /// 没权限
        /// </summary>
        NoAuthority = 0,
        /// <summary>
        /// 个人
        /// </summary>
        Personal = 1,
        /// <summary>
        /// 部门
        /// </summary>
        Department = 2,
        /// <summary>
        /// 本部门及子部门
        /// </summary>
        SubSector = 3,
        /// <summary>
        /// 全部权限
        /// </summary>
        AllAuthority = 4
    }
}