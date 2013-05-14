using Drision.MVCFrame.IDAL;

namespace Drision.MVCFrame.EFDAL
{
    /// <summary>
    /// 简单工厂实现低耦合，数据库访问层的统一入口  尽量的去依赖接口编程
    /// </summary>
    public class RepositoryFactory
    {

        public static IUserRepository UserRepository
        {
            get
            {
                return new UserRepository();
            }
        }

        public static IRoleRepository RoleRepository
        {
            get
            {
                return new RoleRepository();
            }
        }
        public static IActionRepository ActionRepository
        {
            get
            {
                return new ActionRepository();
            }
        }
        public static IGroupRepository GroupRepository
        {
            get
            {
                return new GroupRepository();
            }
        }

    }
}
