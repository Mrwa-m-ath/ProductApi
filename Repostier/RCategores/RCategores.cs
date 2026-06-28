using Microsoft.EntityFrameworkCore;
using ProductApi.Configration;
using ProductApi.Model;

namespace ProductApi.Servies.SCategores
{
    public class RCategores : IRCategores
    {
        private readonly AppDbContexests app1;

        public RCategores(AppDbContexests app1)
        {
            this.app1 = app1;
        }
        public async Task<Categorise?> IsExixt(string NameCategorise)
        {
            return await app1.Categories.FirstOrDefaultAsync(a => a.NameCategories == NameCategorise);
        }
        public async Task AddNewCategores(Categorise categorise)
        {
            await app1.Categories.AddAsync(categorise);
        }
        public async Task? Save()
        {
            await app1.SaveChangesAsync();
        }
        public async Task<Categorise?> IsExixtById(int id)
        {
            return await app1.Categories.FirstOrDefaultAsync(s => s.IdCategores == id);
        }
        public async Task RemovedCatefores(Categorise c)
        {
            app1.Categories.Remove(c);
        }

        public async Task<List<Categorise>> GetAllCategores()
        {
            return await app1.Categories.ToListAsync();
        }
    }
}
