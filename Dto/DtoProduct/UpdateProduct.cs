namespace ProductApi.Dto.DtoProduct
{
    public class UpdateProductDto
    {
        public int IdCategores { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
    public class UpdateProductReplayDto
    {
        public string Message { get; set; }
    }
}
