using Drision.MVCFrame.EFDAL;
using Drision.MVCFrame.IBLL;
using Drision.MVCFrame.Model;

namespace Drision.MVCFrame.BLL
{

    /// <summary>
    /// User业务逻辑
    /// </summary>
    public class UserService : BaseService<SysUser>, IUserService
    {
        public override void SetCurrentRepository()
        {
            CurrentRepository = RepositoryFactory.UserRepository;
        }

    }

}