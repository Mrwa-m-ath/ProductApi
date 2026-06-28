using ProductApi.Model;

namespace ProductApi.Servies.SCategores
{
    public interface IRCategores
    {
        public Task<Categorise?> IsExixt(string NameCategorise);
        public Task AddNewCategores(Categorise categorise);
        public Task? Save();
        public Task RemovedCatefores(Categorise c);
        public Task<Categorise?> IsExixtById(int id);
        public Task<List<Categorise>> GetAllCategores();
    }
}
