using EF.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.BLL
{
    public class BaseBLL<TEntity> where TEntity : class
    {
        BaseDAL<TEntity> baseDAL = new BaseDAL<TEntity>();
        
        public void Add(TEntity model)
        {
            baseDAL.Add(model);
        }

        public void Delete(TEntity model, bool isAttach)
        {
            baseDAL.Delete(model, isAttach);
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return baseDAL.Query(where);
        }

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            return baseDAL.QueryJoin(where, tableNames);
        }

        public void Edit(TEntity model, string[] propertyName)
        {
            baseDAL.Edit(model, propertyName);
        }

        public int SaveChanges()
        {
            return baseDAL.SaveChanges();
        }
    }
}
