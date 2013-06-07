using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Drision.MVCFrame.Models;
using System.Linq.Expressions;
using System.Data;

namespace Drision.MVCFrame.FrameWork
{
    public class GenericManager
    {
        private MVCFrameContext _context;
        internal BaseUser _currentUser;
        public GenericManager(MVCFrameContext _ctx, BaseUser _user)
        {
            this._context = _ctx;
            this._currentUser = _user;
            if (_currentUser == null)
            {
                return;
            }
            int[] roleIds = _currentUser.SysRoles.Select(p => p.ID).ToArray();
            CurrentUserQX = BaseController.EntitySysRole
                .Where(p => roleIds.Contains(p.SysRole.ID))
                .OrderByDescending(p => p.Add).ToArray();
        }
        static GenericManager()
        {
        }
        private SysEntity_SysRole[] CurrentUserQX { get; set; }
        public void Add(object entity)
        {
            string ModelName = entity.GetType().Name;
            int[] roleIds = _currentUser.SysRoles.Select(p => p.ID).ToArray();
            var QX = CurrentUserQX.Where(p => ModelName.StartsWith(p.SysEntity.ModelName))
                .OrderByDescending(p => p.Add).ToList();
            if (QX.Count == 0 || QX[0].Add == 0)
            {
                throw new Exception("当前用户不允许执行此操作");
            }
            ((BaseModel)entity).OwnerId = _currentUser.ID;
            Type t = entity.GetType().BaseType.Name == "BaseModel" ? entity.GetType() : entity.GetType().BaseType;
            _context.Set(t).Add(entity);
        }
        public void Update(BaseModel entity)
        {
            string ModelName = entity.GetType().Name;
            int[] roleIds = _currentUser.SysRoles.Select(p => p.ID).ToArray();
            var QX = CurrentUserQX.Where(p => ModelName.StartsWith(p.SysEntity.ModelName))
                .OrderByDescending(p => p.Update).ToList();
            if (QX.Count == 0 || QX[0].Update == 0)
            {
                throw new Exception("当前用户不允许执行此操作");
            }
            int order = QX[0].Update;
            bool flag = false;
            switch (order)
            {
                case 1:
                    flag = _currentUser.ID == entity.OwnerId;
                    break;
                case 2:
                    var a = BaseController.users.Where(p => p.Department_Id == _currentUser.Department_Id).Select(p => p.ID);
                    flag = a.Contains(entity.OwnerId);
                    break;
                case 3:
                    string leveldemartmentId = _currentUser.Department.ID + "-";
                    int[] departmentsId = _context.SysDepartments.Where(p => p.SystemLevelCode.Contains(leveldemartmentId))
                        .Select(p => p.ID).ToArray();
                    var usersId = _context.BaseUsers.Where(p => departmentsId.Contains(p.Department_Id.Value)).Select(p => p.ID).ToArray();
                    flag = usersId.Contains(entity.OwnerId);
                    break;
                case 4:
                    flag = true;
                    break;
            }
            if (flag)
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }
            else
            { 
                throw new Exception("当前用户不允许执行此操作");
            }
        }
        public void Delete(BaseModel entity)
        {
            string ModelName = entity.GetType().Name;
            int[] roleIds = _currentUser.SysRoles.Select(p => p.ID).ToArray();
            var QX = CurrentUserQX.Where(p => ModelName.StartsWith(p.SysEntity.ModelName)).OrderByDescending(p => p.Delete).ToList();
            if (QX.Count == 0 || QX[0].Delete == 0)
            {
                throw new Exception("当前用户不允许执行此操作");
            }
            int order = QX[0].Delete;
            bool flag = false;
            switch (order)
            {
                case 1:
                    flag = _currentUser.ID == entity.OwnerId;
                    break;
                case 2:
                    var a = BaseController.users.Where(p => p.Department_Id == _currentUser.Department_Id).Select(p => p.ID);
                    flag = a.Contains(entity.OwnerId);
                    break;
                case 3:
                    string leveldemartmentId = _currentUser.Department.ID + "-";
                    int[] departmentsId = _context.SysDepartments.Where(p => p.SystemLevelCode.Contains(leveldemartmentId))
                        .Select(p => p.ID).ToArray();
                    var usersId = _context.BaseUsers.Where(p => departmentsId.Contains(p.Department_Id.Value)).Select(p => p.ID).ToArray();
                    flag = usersId.Contains(entity.OwnerId);
                    break;
                case 4:
                    flag = true;
                    break;
            }
            if (flag)
            {
                Type t = entity.GetType().BaseType.Name == "BaseModel" ? entity.GetType() : entity.GetType().BaseType;
                _context.Set(t).Remove(entity); 
            }
            else
            {
                throw new Exception("当前用户不允许执行此操作");
            }
        }
        public IQueryable<T> Where<T>(Expression<Func<T, bool>> ac) where T : BaseModel, new()
        {
            T t = new T();
            string ModelName = t.GetType().Name;
            int[] roleIds = _currentUser.SysRoles.Select(p => p.ID).ToArray();
            var QX = CurrentUserQX.Where(p => ModelName.StartsWith(p.SysEntity.ModelName)).OrderByDescending(p => p.Search).ToList();
            if (QX.Count == 0 || QX[0].Search == 0)
            {
                throw new Exception("当前用户不允许执行此操作");
            }
            int order = QX[0].Search;
            IQueryable<T> temp = null;
            switch (order)
            {
                    //个人
                case 1:
                    temp = _context.Set<T>().Where(p => p.OwnerId == _currentUser.ID);
                    break;
                //本部门
                case 2:
                    //如果没有部门 则查询自己
                    if (_currentUser.Department_Id == null)
                    {
                        temp = _context.Set<T>().Where(p => p.OwnerId == _currentUser.ID);
                        break;
                    }
                    int[] usersId = _currentUser.Department.BaseUsers.Select(p => p.ID).ToArray();
                    temp = _context.Set<T>().Where(p => usersId.Contains(p.OwnerId));
                    break;
                    //本部门及子部门
                case 3:
                    //如果没有部门 则查询自己
                    if (_currentUser.Department_Id == null)
                    {
                        temp = _context.Set<T>().Where(p => p.OwnerId == _currentUser.ID);
                        break;
                    }
                    string leveldemartmentId = _currentUser.Department.ID + "-";
                    int[] departmentsId = _context.SysDepartments.Where(p => p.SystemLevelCode.Contains(leveldemartmentId))
                        .Select(p => p.ID).ToArray();
                    usersId = _context.BaseUsers.Where(p => departmentsId.Contains(p.Department_Id.Value)).Select(p => p.ID).ToArray();
                    temp = _context.Set<T>().Where(p => usersId.Contains(p.OwnerId));
                    break;
                    //所有
                case 4:
                    temp = _context.Set<T>().AsQueryable();
                    break;
                default:
                    return null;
            }
            return temp.Where(ac);
        }

        public int SaveChange()
        {
            return _context.SaveChanges();
        }
    }
}