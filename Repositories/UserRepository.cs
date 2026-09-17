using FinSteady_API.Infrastructure;
using FinSteady_API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinSteady_API.Repositories
{

    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly SmartSaverDatabaseContext calistaContext;

        public UserRepository(SmartSaverDatabaseContext smartSaverDatabaseContext)
        : base(smartSaverDatabaseContext)
        {
            this.calistaContext = calistaContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await this.Find()
            .OrderByDescending(a => a.UserId)
            .ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await this.Find(d => d.UserId == id)
            .SingleOrDefaultAsync();
        }

        public async Task<User> AddUser(User user)
        {

            this.CreateEntity(user);

            await this.SaveAsync();

            return user;
        }

        public async Task<User> UpdateUser(User dbUser, User user)
        {
            user.UserId = dbUser.UserId;


            dbUser.Map(user);
            this.UpdateEntity(dbUser);

            await this.SaveAsync();
            return dbUser;
        }

        public async Task DeleteUser(User user)
        {
            this.DeleteEntity(user);

            await this.SaveAsync();
        }

    }

}
