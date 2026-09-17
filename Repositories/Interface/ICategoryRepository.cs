using FinSteady_API.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories.Interface
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// Gets all categories.
        /// </summary>
        /// <returns>A list of categories.</returns>
        Task<IEnumerable<Category>> GetCategories();

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category.</param>
        /// <returns>The category with the specified ID.</returns>
        Task<Category> GetCategoryById(int id);

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">The category to add.</param>
        /// <returns>The added category.</returns>
        Task<Category> AddCategory(Category category);

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="dbCategory">The existing category in the database.</param>
        /// <param name="category">The new category data.</param>
        /// <returns>The updated category.</returns>
        Task<Category> UpdateCategory(Category dbCategory, Category category);

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="category">The category to delete.</param>
        Task DeleteCategory(Category category);
    }
}
