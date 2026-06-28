using AutoMapper;
using ProductApi.Dto.DtoProduct;
using ProductApi.Model;

namespace ProductApi.Servies.SProduct
{
    public class SProduct : ISProduct
    {
        private readonly IMapper mapper;
        private readonly IRProduct product;

        public SProduct(IMapper mapper, IRProduct product)
        {
            this.mapper = mapper;
            this.product = product;
        }

        public async Task<AddNewProductReplayDto> AddNewProduct(AddNewProductDto add)
        {
            var IsExist = await product.IsExist(add.Name);
            if (IsExist != null)
            {
                throw new Exception("The Product Alreade Id Exist");
            }
            var IsExistCategores = await product.IsExistCategores(add.IdCategores);
            if (IsExistCategores == null)
            {
                throw new Exception("The Categord Invalid");
            }
            var NewProduct = mapper.Map<Product>(add);
            await product.AddNewProduct(NewProduct);
            await product.SaveProduct();
            return new AddNewProductReplayDto
            {
                Message = "Success Add New Product"
            };
        }

        public async Task<List<Product>> GetAllProduct(string? name, int page, int pageSize)
        {
            return await product.GetAllProduct();
        }

        public async Task<RemovedProductReplayDto> RemovedProduct(int ID)
        {
            var IsExist = await product.IsExistById(ID);
            if (IsExist == null)
            {
                throw new Exception("The Product invalid");
            }
            await product.RemoveProduct(IsExist);
            await product.SaveProduct();
            return new RemovedProductReplayDto
            {
                Message = "Success Removed Product"
            };
        }

        public async Task<UpdateProductReplayDto> UpdateProduct(UpdateProductDto update, int id)
        {
            var IsExist = await product.IsExistById(id);
            if (IsExist == null)
            {
                throw new Exception("The Product invalid");
            }
            IsExist.Quantity = update.Quantity;
            IsExist.Price = update.Price;
            IsExist.Name = update.Name;
            IsExist.Description = update.Description;
            IsExist.IdCategores = update.IdCategores;
            await product.SaveProduct();
            return new UpdateProductReplayDto { Message = "Success Update" };
        }

    }
}
