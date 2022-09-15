using BlazorEksiSozluk.Api.Application.Interfaces.Repositories;
using BlazorEksiSozluk.Api.Domain.Models;
using BlazorEksiSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BlazorEksiSozluk.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly DbContext dbContext;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
        protected DbSet<TEntity> entity => dbContext.Set<TEntity>();

        #region insert methods
        public virtual int Add(TEntity entity)
        {
            this.entity.Add(entity);
            return dbContext.SaveChanges();
        }

        public virtual int Add(IEnumerable<TEntity> entities)
        {
            this.entity.AddRange(entities);
            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddAsync(TEntity entity)
        {
            await this.entity.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public virtual async Task<int> AddAsync(IEnumerable<TEntity> entities)
        {
            await this.entity.AddRangeAsync(entities);
            return await dbContext.SaveChangesAsync();
        }
        #endregion insert methods

        #region add or update methods
        public virtual int AddOrUpdate(TEntity entity)
        {
            if(!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
                dbContext.Update(entity);

            return dbContext.SaveChanges();
        }

        public virtual async Task<int> AddOrUpdateAsync(TEntity entity)
        {
            if(!this.entity.Local.Any(x => EqualityComparer<Guid>.Default.Equals(x.Id, entity.Id)))
                dbContext.Update(entity);

            return await dbContext.SaveChangesAsync();
        }
        #endregion add or update methods

        #region get methods
        public virtual IQueryable<TEntity> AsQueryable() => entity.AsQueryable();

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = entity.AsQueryable();

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return query;
        }

        public virtual async Task<List<TEntity>> GetAll(bool noTracking = true)
        {
            IQueryable<TEntity> query = entity;

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetByIdAsync(Guid id, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            TEntity found = await entity.FindAsync(id);

            if(found == null)
                return null;

            if(noTracking)
                dbContext.Entry(found).State = EntityState.Detached;

            foreach(var include in includes)
            {
                dbContext.Entry(found).Reference(include).Load();
            }

            return found;
        }

        public virtual async Task<List<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if(predicate != null)
                query = query.Where(predicate);

            foreach(var item in includes)
            {
                query = query.Include(item);
            }

            if(orderBy != null)
                query = orderBy(query);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.ToListAsync();
        }

        public virtual async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate, bool noTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = entity;

            if(predicate != null)
                query = query.Where(predicate);

            query = ApplyIncludes(query, includes);

            if(noTracking)
                query = query.AsNoTracking();

            return await query.SingleOrDefaultAsync();
        }
        #endregion get methods

        #region bulk methods
        public virtual async Task BulkAdd(IEnumerable<TEntity> entities)
        {
            if(entities != null && !entities.Any())
                await Task.CompletedTask;

            await entity.AddRangeAsync(entities);

            await dbContext.SaveChangesAsync();
        }

        public virtual Task BulkDelete(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual Task BulkDelete(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public virtual Task BulkDeleteById(IEnumerable<Guid> ids)
        {
            if(ids != null && !ids.Any())
                return Task.CompletedTask;

            dbContext.RemoveRange(entity.Where(x => ids.Contains(x.Id)));

            return dbContext.SaveChangesAsync();
        }

        public virtual Task BulkUpdate(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
        #endregion bulk methods

        #region delete methods
        public virtual int Delete(TEntity entity)
        {
            if(dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return dbContext.SaveChanges();
        }

        public virtual int Delete(Guid id)
        {
            var entity = this.entity.Find(id);

            return Delete(entity);
        }

        public virtual async Task<int> DeleteAsync(TEntity entity)
        {
            if(dbContext.Entry(entity).State == EntityState.Detached)
            {
                this.entity.Attach(entity);
            }

            this.entity.Remove(entity);

            return await dbContext.SaveChangesAsync();
        }

        public virtual Task<int> DeleteAsync(Guid id)
        {
            var entity = this.entity.Find(id);

            return DeleteAsync(entity);
        }

        public virtual bool DeleteRange(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));

            return dbContext.SaveChanges() > 0;
        }

        public virtual async Task<bool> DeleteRangeAsync(Expression<Func<TEntity, bool>> predicate)
        {
            dbContext.RemoveRange(entity.Where(predicate));

            return await dbContext.SaveChangesAsync() > 0;
        }
        #endregion delete methods


        #region update methods
        public virtual int Update(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return dbContext.SaveChanges();
        }

        public virtual async Task<int> UpdateAsync(TEntity entity)
        {
            this.entity.Attach(entity);
            dbContext.Entry(entity).State = EntityState.Modified;

            return await dbContext.SaveChangesAsync();
        }
        #endregion update methods

        private static IQueryable<TEntity> ApplyIncludes(IQueryable<TEntity> query, params Expression<Func<TEntity, object>>[] includes)
        {
            if(includes != null)
            {
                foreach(var item in includes)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        #region savechanges methods
        public int SaveChanges()
        {
            return dbContext.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return dbContext.SaveChangesAsync();
        }
        #endregion savechanges methods
    }
}
