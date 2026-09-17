namespace FinSteady_API.Repositories.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepositoryBase<T>
    {
        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns>
        /// List of entities.
        /// </returns>
        IQueryable<T> Find();

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <returns>
        /// List of entities.
        /// </returns>
        IQueryable<T> Find(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void CreateEntity(T entity);

        /// <summary>
        /// Creates multiple entity
        /// </summary>
        /// <param name="entities"></param>
        void CreateEntityRange(IEnumerable<T> entities);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void UpdateEntity(T entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void DeleteEntity(T entity);

        /// <summary>
        /// Saves the asynchronous.
        /// </summary>
        /// <returns>
        /// Task
        /// </returns>
        Task SaveAsync();

    }

}
