namespace Drision.MVCFrame.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Reflection;
    using Drision.MVCFrame.Models;
    using Drision.MVCFrame.FrameWork;

    internal sealed class Configuration : DbMigrationsConfiguration<Drision.MVCFrame.Models.MVCFrameContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }
        protected override void Seed(Drision.MVCFrame.Models.MVCFrameContext context)
        {
            Type[] types = Assembly.Load("Drision.MVCFrame").GetTypes();
            var setypes = types.Where(p => p.GetCustomAttributes(typeof(ModelRoleAttribute), true).Length > 0);
            var names = context.SysEntities.Select(p => p.ModelName).ToList();
            setypes = setypes.Where(p => !names.Contains(p.Name));
            var Roles = context.SysRoles.ToList();
            foreach (Type type in setypes)
            {
                var entity = new SysEntity()
                {
                    OwnerId = 1,
                    EntityName = ((ModelRoleAttribute)type.GetCustomAttributes(typeof(ModelRoleAttribute), true)[0]).Name,
                    UpdateUserId = 1,
                    ModelName = type.Name
                };
                foreach (var role in Roles)
                {
                    SysEntity_SysRole entityRole = new SysEntity_SysRole();
                    entityRole.SysEntity = entity;
                    entityRole.SysRole = role;
                    context.SysEntity_SysRoles.Add(entityRole);
                }
            }
            context.SaveChanges();
        }
    }
}