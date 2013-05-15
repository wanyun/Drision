  



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drision.MVCFrame.EFDAL;
using Drision.MVCFrame.Model;
using Drision.MVCFrame.IBLL;

namespace Drision.MVCFrame.BLL
{

        public partial class Sys_R_Group_ActionService : BaseService<Sys_R_Group_Action>, ISys_R_Group_ActionService
		{
			//重写抽象方法，设置当前仓储为Sys_R_Group_Action仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.Sys_R_Group_ActionRepository;
			}

		}

	        public partial class Sys_R_Role_ActionService : BaseService<Sys_R_Role_Action>, ISys_R_Role_ActionService
		{
			//重写抽象方法，设置当前仓储为Sys_R_Role_Action仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.Sys_R_Role_ActionRepository;
			}

		}

	        public partial class Sys_R_User_ActionService : BaseService<Sys_R_User_Action>, ISys_R_User_ActionService
		{
			//重写抽象方法，设置当前仓储为Sys_R_User_Action仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.Sys_R_User_ActionRepository;
			}

		}

	        public partial class Sys_R_User_GroupService : BaseService<Sys_R_User_Group>, ISys_R_User_GroupService
		{
			//重写抽象方法，设置当前仓储为Sys_R_User_Group仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.Sys_R_User_GroupRepository;
			}

		}

	        public partial class Sys_R_User_RoleService : BaseService<Sys_R_User_Role>, ISys_R_User_RoleService
		{
			//重写抽象方法，设置当前仓储为Sys_R_User_Role仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.Sys_R_User_RoleRepository;
			}

		}

	        public partial class SysActionService : BaseService<SysAction>, ISysActionService
		{
			//重写抽象方法，设置当前仓储为SysAction仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.SysActionRepository;
			}

		}

	        public partial class SysGroupService : BaseService<SysGroup>, ISysGroupService
		{
			//重写抽象方法，设置当前仓储为SysGroup仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.SysGroupRepository;
			}

		}

	        public partial class SysRoleService : BaseService<SysRole>, ISysRoleService
		{
			//重写抽象方法，设置当前仓储为SysRole仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.SysRoleRepository;
			}

		}

	        public partial class SysUserService : BaseService<SysUser>, ISysUserService
		{
			//重写抽象方法，设置当前仓储为SysUser仓储
			public override void SetCurrentRepository()
			{
				CurrentRepository = _DbSession.SysUserRepository;
			}

		}

	
}