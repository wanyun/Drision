  


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Drision.MVCFrame.IDAL
{
    public interface IDbSession
    {

    //----T4模板生成--- 每个表对应的实体仓储对象
	ISys_R_Role_ActionRepository  Sys_R_Role_ActionRepository { get; }

	ISys_R_User_ActionRepository  Sys_R_User_ActionRepository { get; }

	ISys_R_User_RoleRepository  Sys_R_User_RoleRepository { get; }

	ISysActionRepository  SysActionRepository { get; }

	ISysGroupRepository  SysGroupRepository { get; }

	ISysRoleRepository  SysRoleRepository { get; }

	ISysUserRepository  SysUserRepository { get; }


        //将当前应用程序跟数据库的会话内所有实体的变化更新会数据库
        int SaveChanges();

        //执行Sql语句的方法
        //EF4.0的写法
        int ExcuteSql(string strSql, ObjectParameter[] parameters);

        //EF5.0的写法
        //int ExcuteSql(string strSql, DbParameter[] parameters);

    }

}