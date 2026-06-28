using ProductApi.Model;

namespace ProductApi.Servies.SUser
{
    public interface IRUser
    {
        public Task<User?> IsExist(string Email);
        public Task AddNewUser(User user);
        public Task SaveUser();
        public Task<User?> IsExistById(int id);
        public Task RemoveById(User user);
        public Task<List<User?>> GetAllUser();
    }
}
