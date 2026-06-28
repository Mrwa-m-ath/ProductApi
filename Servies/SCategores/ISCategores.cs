using ProductApi.Dto.DtoCategores;
using ProductApi.Model;

namespace ProductApi.Servies.SCategores
{
    public interface ISCategores
    {
        public Task<RemoveCatigoresReplayDto> removeCatigores(int id);
        public Task<AddNewCategoriseReplayDto> AddNewCategorise(AddNewCategoriseDto add);
        public Task<UpdateCategoresReplayDto> UpdateCategores(UpdateCategoresDto update, int id);
        public Task<List<Categorise>> GetAllCategores(string? name, int page, int SizePage);
    }
}