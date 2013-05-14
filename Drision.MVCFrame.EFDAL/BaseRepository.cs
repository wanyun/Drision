using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drision.MVCFrame.Model;
using Drision.MVCFrame.IDAL;


namespace Drision.MVCFrame.EFDAL
{
    public  class BaseRepository<T> where T : class
    {

        //实例化EF框架
        //获取的实当前线程内部的上下文实例，而且保证了线程内上下文实例唯一
        private DrisionMVCFrameEntities db = new DrisionMVCFrameEntities();

        // 实现对数据库的添加功能
        public T AddEntity(T entity)
        {
            db.CreateObjectSet<T>().AddObject(entity);
            db.SaveChanges();
            return entity;
        }



        //实现对数据库的修改功能
        public bool UpdateEntity(T entity)
        {
            db.CreateObjectSet<T>().AddObject(entity);
            db.ObjectStateManager.ChangeObjectState(entity, EntityState.Modified);
            return db.SaveChanges() > 0;
        }



        //实现对数据库的删除功能
        public bool DeleteEntity(T entity)
        {
            db.CreateObjectSet<T>().DeleteObject(entity);
            db.ObjectStateManager.ChangeObjectState(entity, EntityState.Deleted);
            return db.SaveChanges() > 0;
        }



        //实现对数据库的查询  --简单查询
        public IQueryable<T> LoadEntities(Func<T, bool> whereLambda)
        {
            return db.CreateObjectSet<T>().Where<T>(whereLambda).AsQueryable();

        }



        /// <summary>
        /// 实现对数据的分页查询
        /// </summary>
        /// <typeparam name="S">按照某个类进行排序</typeparam>
        /// <param name="pageIndex">当前第几页</param>
        /// <param name="pageSize">一页显示多少条数据</param>
        /// <param name="total">总条数</param>
        /// <param name="whereLambda">取得排序的条件</param>
        /// <param name="isAsc">如何排序，根据倒叙还是升序</param>
        /// <param name="orderByLambda">根据那个字段进行排序</param>
        /// <returns></returns>

        public IQueryable<T> LoadPageEntities<S>(int pageIndex, int pageSize, out  int total, Func<T, bool> whereLambda,bool isAsc, Func<T, S> orderByLambda)
        {
            var temp = db.CreateObjectSet<T>().Where<T>(whereLambda);

            total = temp.Count(); //得到总的条数

            //排序,获取当前页的数据
            if (isAsc)
            {

                temp = temp.OrderBy<T, S>(orderByLambda)
                     .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                     .Take<T>(pageSize).AsQueryable(); //取出多少条

            }
            else
            {

                temp = temp.OrderByDescending<T, S>(orderByLambda)
                    .Skip<T>(pageSize * (pageIndex - 1)) //越过多少条
                    .Take<T>(pageSize).AsQueryable(); //取出多少条

            }
            return temp.AsQueryable();

        }

    }
}
