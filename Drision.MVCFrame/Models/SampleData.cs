using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Drision.MVCFrame.Common;
using Drision.MVCFrame.Migrations;
using System.Reflection;
using Drision.MVCFrame.FrameWork;

namespace Drision.MVCFrame.Models
{
    public class SampleData : CreateDatabaseIfNotExists<MVCFrameContext>
    {
        protected override void Seed(MVCFrameContext context)
        {
            var user = new BaseUser { UserName = "Administrator", NickName = "", PassWord = MyMD5Helper.GetMD532("1"), Email = "test@qq.com", DelFlag = false, CreateTime = DateTime.Parse("2013-05-21"), SysFunctions = new List<SysFunction>(), SysRoles = new List<SysRole>() };
            context.BaseUsers.Add(user); 
            context.SaveChanges();

            var roles = new List<SysRole>           
            {                
                new SysRole { RoleName = "管理员", RoleDescription="High",RoleOrder=0, DelFlag = false }
            };
            roles.ForEach(s => context.SysRoles.Add(s));
            context.SaveChanges();
            #region 添加实体角色数据对应权限数据表
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
            #endregion
            //var actions = new List<SysFunction>           
            //{                
            //    new SysFunction { ControllerName = "User", ActionName="Index",MenuName="主页", DelFlag = false,IsMenu=true }
            //};
            //actions.ForEach(s => context.SysFunctions.Add(s));
            //context.SaveChanges();



        }
    }
}