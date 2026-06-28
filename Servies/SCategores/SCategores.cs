using AutoMapper;
using ProductApi.Dto.DtoCategores;
using ProductApi.Model;

namespace ProductApi.Servies.SCategores
{
    public class SCategores : ISCategores
    {
        private readonly IMapper mapper;
        private readonly IRCategores categores;

        public SCategores(IMapper mapper, IRCategores categores)
        {
            this.mapper = mapper;
            this.categores = categores;
        }
        public async Task<AddNewCategoriseReplayDto> AddNewCategorise(AddNewCategoriseDto add)
        {
            var IsExist = await categores.IsExixt(add.NameCategories);
            if (IsExist != null)
            {
                throw new InvalidOperationException("The Name Categores Is Exist");
            }
            var NewCategores = mapper.Map<Categorise>(add);
            await categores.AddNewCategores(NewCategores);
            await categores.Save();
            return new AddNewCategoriseReplayDto
            {
                Message = "Saccesse Add New Categores",
            };
        }

        public async Task<List<Categorise>> GetAllCategores(string? name, int page, int SizePage)
        {
            return await categores.GetAllCategores();
        }

        public async Task<RemoveCatigoresReplayDto> removeCatigores(int id)
        {
            var IsExist = await categores.IsExixtById(id); if (IsExist == null)
            {
                throw new InvalidOperationException("The Name Categores Invalid");
            }
            await categores.RemovedCatefores(IsExist);
            await categores.Save();
            return new RemoveCatigoresReplayDto
            {
                Message = "Success Remved Categores"
            };

        }
        public async Task<UpdateCategoresReplayDto> UpdateCategores(UpdateCategoresDto update, int id)
        {
            var IsExist = await categores.IsExixtById(id);
            if (IsExist == null)
            {
                throw new InvalidOperationException("The Name Categores Invalid");
            }
            IsExist.NameCategories = update.NameCategories;
            IsExist.ImageCategories = update.ImageCategories;
            await categores.Save();
            return new UpdateCategoresReplayDto
            {
                Message = "Success Update Categore"
            };
        }
    }
}
