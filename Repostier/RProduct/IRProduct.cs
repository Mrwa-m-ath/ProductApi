using ProductApi.Model;

namespace ProductApi.Servies.SProduct
{
    public interface IRProduct
    {
        public Task<Product?> IsExist(string NameProduct);
        public Task<Product?> IsExistById(int id);
        public Task AddNewProduct(Product product);
        public Task RemoveProduct(Product product);
        public Task SaveProduct(); public Task<Categorise?> IsExistCategores(int id);
        public Task<List<Product>> GetAllProduct();
    }
}
