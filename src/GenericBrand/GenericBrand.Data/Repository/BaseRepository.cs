using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GenericBrand.Data.DAL;
using Microsoft.EntityFrameworkCore;

namespace GenericBrand.Data.Repository
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        protected ApplicationDbContext dbContext;
        protected DbSet<TEntity> dbSet;

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository(ApplicationDbContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<TEntity>();
        }
        #endregion

        #region Get Items
        /// <summary>
        /// Get list of items
        /// </summary>
        /// <param name="filter">e => e.Id == id</param>
        /// <param name="orderBy">q => q.OrderBy(e => e.Id).ThenBy(f => f.Name)</param>
        /// <param name="includeProperties">List of properties separated by commas to include in each item</param>
        /// <param name="take">Take Top x Records</param>
        /// <param name="noTracking">Set to false to turn off No Tracking</param>
        /// <returns></returns>
        public IEnumerable<TEntity> Items(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int take = 0, bool noTracking = true)
        {
            IQueryable<TEntity> query = noTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (take != 0)
                {
                    return orderBy(query).Take(take).ToList();
                }
                else
                {
                    return orderBy(query).ToList();
                }
            }

            if (take != 0)
            {
                return query.Take(take).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Get list of items Async
        /// </summary>
        /// <param name="filter">e => e.Id == id</param>
        /// <param name="orderBy">q => q.OrderBy(e => e.Id).ThenBy(f => f.Name)</param>
        /// <param name="includeProperties">List of properties separated by commas to include in each item</param>
        /// <param name="take">Take Top x Records</param>
        /// <param name="noTracking">Set to false to turn off No Tracking</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> ItemsAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", int take = 0, bool noTracking = true)
        {
            IQueryable<TEntity> query = noTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                if (take != 0)
                {
                    return await orderBy(query).Take(take).ToListAsync();
                }
                else
                {
                    return await orderBy(query).ToListAsync();
                }
            }

            if (take != 0)
            {
                return await query.Take(take).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }
        #endregion

        #region Get Items Paged
        /// <summary>
        /// Number of pages
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public int Pages(int pageSize, Expression<Func<TEntity, bool>> filter = null)
        {
            // Get number of records
            int records = Items(filter).Count();
            int pages = records / pageSize;

            if ((pages * pageSize) < records) { pages = pages + 1; }

            return pages;
        }

        /// <summary>
        /// Number of pages Async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<int> PagesAsync(int pageSize, Expression<Func<TEntity, bool>> filter = null)
        {
            // Get number of records
            int records = (await ItemsAsync(filter)).Count();
            int pages = records / pageSize;

            if ((pages * pageSize) < records) { pages = pages + 1; }

            return pages;
        }

        /// <summary>
        /// Get Paged Items
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <param name="noTracking">Set to false to turn off No Tracking</param>
        /// <returns></returns>
        public IEnumerable<TEntity> ItemsPaged(int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool noTracking = true)
        {
            int pages = 0;
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1) { pageSize = 1; }

            pages = Pages(pageSize, filter);

            if (pageNumber > pages) { pageNumber = pages; }

            IQueryable<TEntity> query = noTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return query.Skip((pages - 1) * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Get Paged Items Async
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="filter"></param>
        /// <param name="noTracking">Set to false to turn off No Tracking</param>
        /// <returns></returns>
        public async Task<IEnumerable<TEntity>> ItemsPagedAsync(int pageNumber, int pageSize,
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "", bool noTracking = true)
        {
            int pages = 0;
            if (pageNumber < 1) { pageNumber = 1; }
            if (pageSize < 1) { pageSize = 1; }

            pages = await PagesAsync(pageSize, filter);

            if (pageNumber > pages) { pageNumber = pages; }

            IQueryable<TEntity> query = noTracking ? dbSet.AsNoTracking() : dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            }

            return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }
        #endregion

        #region Save Changes
        /// <summary>
        /// Save Changes
        /// </summary>
        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Save Changes Async
        /// </summary>
        /// <returns></returns>
        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
