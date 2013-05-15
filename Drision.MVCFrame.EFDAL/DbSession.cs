  


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drision.MVCFrame.IDAL;
using System.Data.Objects;

namespace Drision.MVCFrame.EFDAL
{

    //代表应用程序跟数据库之间的一次会话，也是数据库访问层的统一入口
    public class DbSession : IDbSession 
    {

    //----T4模板生成--- 每个表对应的实体仓储对象

		public ISys_R_Group_ActionRepository  Sys_R_Group_ActionRepository
        {
            get 
            {
                return new Sys_R_Group_ActionRepository(); 
            }
        }


		public ISys_R_Role_ActionRepository  Sys_R_Role_ActionRepository
        {
            get 
            {
                return new Sys_R_Role_ActionRepository(); 
            }
        }


		public ISys_R_User_ActionRepository  Sys_R_User_ActionRepository
        {
            get 
            {
                return new Sys_R_User_ActionRepository(); 
            }
        }


		public ISys_R_User_GroupRepository  Sys_R_User_GroupRepository
        {
            get 
            {
                return new Sys_R_User_GroupRepository(); 
            }
        }


		public ISys_R_User_RoleRepository  Sys_R_User_RoleRepository
        {
            get 
            {
                return new Sys_R_User_RoleRepository(); 
            }
        }


		public ISysActionRepository  SysActionRepository
        {
            get 
            {
                return new SysActionRepository(); 
            }
        }


		public ISysGroupRepository  SysGroupRepository
        {
            get 
            {
                return new SysGroupRepository(); 
            }
        }


		public ISysRoleRepository  SysRoleRepository
        {
            get 
            {
                return new SysRoleRepository(); 
            }
        }


		public ISysUserRepository  SysUserRepository
        {
            get 
            {
                return new SysUserRepository(); 
            }
        }


        //代表：当前应用程序跟数据库的会话内所有的实体的变化，更新会数据库
        public int SaveChanges()
        {
            //调用EF上下文的SaveChanges方法
            return EFContextFactory.GetCurrentDbContext().SaveChanges();
        }

        //执行Sql脚本的方法
        public int ExcuteSql(string strSql, ObjectParameter[] parameters)
        {

            //Ef4.0的执行方法 ObjectContext
            //封装一个执行SQl脚本的代码
            return EFContextFactory.GetCurrentDbContext().ExecuteFunction(strSql, parameters);

            //throw new NotImplementedException();

        }

    }

}
