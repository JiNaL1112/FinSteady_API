namespace FinSteady_API.Repositories
{
    using FinSteady_API.Repositories.Interface;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public abstract class RepositoryBase<T> : IRepositoryBase<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        protected RepositoryBase(DbContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        private DbContext Context { get; }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>
        /// List of entities.
        /// </returns>
        public IQueryable<T> Find()
        {
            return this.Context.Set<T>();
        }

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// List of entities.
        /// </returns>
        public IQueryable<T> Find(Expression<Func<T, bool>> expression)
        {
            return this.Context.Set<T>()
                .Where(expression);
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void CreateEntity(T entity)
        {
            this.Context.Set<T>().Add(entity);
        }

        /// <summary>
        /// Creates multiple entity
        /// </summary>
        /// <param name="entities"></param>
        public void CreateEntityRange(IEnumerable<T> entities)
        {
            this.Context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateEntity(T entity)
        {
            this.Context.Set<T>().Update(entity);
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteEntity(T entity)
        {
            this.Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns>
        /// Task
        /// </returns>
        public async Task SaveAsync()
        {
            await this.Context.SaveChangesAsync();
        }

    }
}
