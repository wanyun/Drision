using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drision.MVCFrame.Model;
using Drision.MVCFrame.IBLL;

namespace Drision.MVCFrame.BLL
{
    public partial class SysUserService
    {
        public SysUser CheckUserInfo(SysUser userInfo)
        {

            //在这里会去数据库检查是否有数据，如果没有的话就会返回一个空值
            return _DbSession.SysUserRepository.LoadEntities(u => u.UserName == userInfo.UserName && u.Password == userInfo.Password).FirstOrDefault();

        }
    }
}
