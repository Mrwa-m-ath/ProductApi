using Microsoft.EntityFrameworkCore;
using ProductApi.Configration;
using ProductApi.Model;

namespace ProductApi.Servies.SProduct
{
    public class RProduct : IRProduct
    {
        private readonly AppDbContexests appDb;

        public RProduct(AppDbContexests appDb)
        {
            this.appDb = appDb;
        }

        public async Task AddNewProduct(Product product)
        {
            await appDb.products.AddAsync(product);
        }

        public async Task<Product?> IsExist(string NameProduct)
        {
            return await appDb.products.SingleOrDefaultAsync(S => S.Name == NameProduct);
        }

        public async Task<Product?> IsExistById(int id)
        {
            return await appDb.products.SingleOrDefaultAsync(S => S.IdProduct == id);
        }

        public async Task RemoveProduct(Product product)
        {
            appDb.products.Remove(product);
        }

        public async Task SaveProduct()
        {
            await appDb.SaveChangesAsync();
        }
        public async Task<Categorise?> IsExistCategores(int id)
        {
            return await appDb.Categories.FirstOrDefaultAsync(s => s.IdCategores == id);
        }
        public async Task<List<Product>> GetAllProduct()
        {
            return await appDb.products.ToListAsync();




        }
    }
}
