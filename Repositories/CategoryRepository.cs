using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinSteady_API.Repositories
{
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        private readonly SmartSaverDatabaseContext databaseContext;

        public CategoryRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
            : base(smartSaverDatabaseContext)
        {
            this.databaseContext = smartSaverDatabaseContext;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await this.Find()
                .OrderByDescending(a => a.CategoryId)
                .ToListAsync();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await this.Find(d => d.CategoryId == id)
                .SingleOrDefaultAsync();
        }

        public async Task<Category> AddCategory(Category category)
        {
            this.CreateEntity(category);
            await this.SaveAsync();
            return category;
        }

        public async Task<Category> UpdateCategory(Category dbCategory, Category category)
        {
            category.CategoryId = dbCategory.CategoryId;

            dbCategory.Map(category);
            this.UpdateEntity(dbCategory);

            await this.SaveAsync();
            return dbCategory;
        }

        public async Task DeleteCategory(Category category)
        {
            this.DeleteEntity(category);
            await this.SaveAsync();
        }
    }
}
