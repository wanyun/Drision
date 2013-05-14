using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drision.MVCFrame.EFDAL;
using Drision.MVCFrame.Model;
using Drision.MVCFrame.IBLL;

namespace Drision.MVCFrame.BLL
{
    public class ActionService : BaseService<SysAction>, IActionService
    {
        //重写抽象方法，设置当前仓储为Role仓储
        public override void SetCurrentRepository()
        {
            //设置当前仓储为Role仓储
            CurrentRepository = RepositoryFactory.ActionRepository;
        }
    }
}
