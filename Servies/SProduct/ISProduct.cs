using ProductApi.Dto.DtoProduct;
using ProductApi.Model;

namespace ProductApi.Servies.SProduct
{
    public interface ISProduct
    {
        public Task<AddNewProductReplayDto> AddNewProduct(AddNewProductDto add);
        public Task<UpdateProductReplayDto> UpdateProduct(UpdateProductDto update, int id);
        public Task<RemovedProductReplayDto> RemovedProduct(int ID);
        public Task<List<Product>> GetAllProduct(string? name, int page, int pageSize);
    }
}
