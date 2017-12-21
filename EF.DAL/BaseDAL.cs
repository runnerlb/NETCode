using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EF.DAL
{
    public class BaseDAL<TEntity> where TEntity : class
    {
        BaseDBContext db = new BaseDBContext();

        private DbSet<TEntity> _dbSet;

        public BaseDAL()
        {
            _dbSet = db.Set<TEntity>();
        }

        public void Add(TEntity model)
        {
            if (model == null)
            {
                throw new Exception("model必须为实体对象");
            }
            _dbSet.Add(model);
        }

        public void Delete(TEntity model, bool isAttach)
        {
            if (model == null)
            {
                throw new Exception("model必须为实体对象");
            }
            if (!isAttach)
            {
                _dbSet.Attach(model);
            }
            _dbSet.Remove(model);
        }

        public List<TEntity> Query(Expression<Func<TEntity, bool>> where)
        {
            return _dbSet.Where(where).ToList();
        }

        public List<TEntity> QueryJoin(Expression<Func<TEntity, bool>> where, string[] tableNames)
        {
            DbQuery<TEntity> query = _dbSet;

            foreach (var tableName in tableNames)
            {
                query = query.Include(tableName);
            }

            return query.Where(where).ToList();
        }

        public void Edit(TEntity model, string[] propertyName)
        {
            if (model == null)
            {
                throw new Exception("model必须为实体对象");
            }

            if (propertyName == null || propertyName.Any() == false)
            {
                throw new Exception("必须至少指定一个要修改的属性");
            }

            DbEntityEntry entityEntry = db.Entry(model);

            //2.0 将状态修改成unchagned
            entityEntry.State = EntityState.Unchanged;

            foreach (var item in propertyName)
            {
                entityEntry.Property(item).IsModified = true;
            }
        }

        public int SaveChanges()
        {
            return db.SaveChanges();
        }
    }
}
