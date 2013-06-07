using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;

namespace Drision.MVCFrame.Models
{
    public class MVCFrameContext : DbContext
    {
        public DbSet<BaseUser> BaseUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysFunction> SysFunctions { get; set; }
        public DbSet<SysDepartment> SysDepartments { get; set; }
        public DbSet<SysEntity> SysEntities { get; set; }
        public DbSet<SysEntity_SysRole> SysEntity_SysRoles { get; set; }
    }
}