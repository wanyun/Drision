  




using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Threading.Tasks;
using Drision.MVCFrame.Model;

namespace Drision.MVCFrame.IDAL
{

        public interface ISys_R_Group_ActionRepository : IBaseRepository<Sys_R_Group_Action>
		{

		}

	        public interface ISys_R_Role_ActionRepository : IBaseRepository<Sys_R_Role_Action>
		{

		}

	        public interface ISys_R_User_ActionRepository : IBaseRepository<Sys_R_User_Action>
		{

		}

	        public interface ISys_R_User_GroupRepository : IBaseRepository<Sys_R_User_Group>
		{

		}

	        public interface ISys_R_User_RoleRepository : IBaseRepository<Sys_R_User_Role>
		{

		}

	        public interface ISysActionRepository : IBaseRepository<SysAction>
		{

		}

	        public interface ISysGroupRepository : IBaseRepository<SysGroup>
		{

		}

	        public interface ISysRoleRepository : IBaseRepository<SysRole>
		{

		}

	        public interface ISysUserRepository : IBaseRepository<SysUser>
		{

		}

	
}