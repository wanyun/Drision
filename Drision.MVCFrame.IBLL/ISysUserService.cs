using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Drision.MVCFrame.IDAL;
using Drision.MVCFrame.Model;

namespace Drision.MVCFrame.IBLL
{
    public partial interface ISysUserService 
    {
        //在这里添加一个用户登录信息的约束

        SysUser CheckUserInfo(SysUser userInfo);
    }
}
