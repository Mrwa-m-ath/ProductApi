using ProductApi.Dto.DtoUser;
using ProductApi.Model;

namespace ProductApi.Servies.SUser
{
    public interface ISUser
    {
        public Task<AddNewUserReplayDto> AddNewUserDto(AddNewUserDto add);
        public Task<SingInUserReplayDto> SingInUser(SingInUserDto singIn);
        public Task<DeleteUserByIdReplayDto> RemovedUserById(int id);
        public Task<UpdateUserDtoReplay> UpdateUser(UpdateUserDto userDto, int Id);
        public Task<List<User>> GetUsers();
    }
}
