using Drision.MVCFrame.IDAL;

namespace Drision.MVCFrame.EFDAL
{
    /// <summary>
    /// 简单工厂实现低耦合，数据库访问层的统一入口  尽量的去依赖接口编程
    /// </summary>
    public class RepositoryFactory
    {

        public static ISysUserRepository UserRepository
        {
            get
            {
                return new SysUserRepository();
            }
        }

        public static ISysRoleRepository RoleRepository
        {
            get
            {
                return new SysRoleRepository();
            }
        }
        public static ISysActionRepository ActionRepository
        {
            get
            {
                return new SysActionRepository();
            }
        }
        public static ISysGroupRepository GroupRepository
        {
            get
            {
                return new SysGroupRepository();
            }
        }

    }
}
