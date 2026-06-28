using Microsoft.EntityFrameworkCore;
using ProductApi.Configration;
using ProductApi.Model;

namespace ProductApi.Servies.SUser
{
    public class RUser : IRUser
    {
        private readonly AppDbContexests appDb;

        public RUser(AppDbContexests appDb)
        {
            this.appDb = appDb;
        }
        public async Task<User?> IsExist(string Email)
        {
            return await appDb.users.FirstOrDefaultAsync(s => s.Email == Email);
        }
        public async Task AddNewUser(User user)
        {
            appDb.users.Add(user);
            await appDb.SaveChangesAsync();
        }
        public async Task SaveUser()
        {
            _ = appDb.SaveChangesAsync();
        }
        public async Task<User?> IsExistById(int id)
        {
            return await appDb.users.FirstOrDefaultAsync(s => s.Id == id);
        }
        public async Task RemoveById(User user)
        {
            appDb.users.Remove(user);
        }
        public async Task<List<User?>> GetAllUser()
        {
            return await appDb.users.ToListAsync();
        }

    }
}
