using AutoMapper;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProductApi.Dto.DtoUser;
using ProductApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApi.Servies.SUser
{
    public class SUsers : ISUser
    {
        private readonly IRUser users;
        private readonly JWT jwt;
        private readonly IMapper mapper;
        public SUsers(IRUser users, IOptions<JWT> jwt, IMapper mapper)
        {
            this.users = users;
            this.jwt = jwt.Value;
            this.mapper = mapper;
        }
        private string GenrationToken(User user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var Cords = new SigningCredentials(key: Key, SecurityAlgorithms.HmacSha256);
            var Claims = new[]
            {
                new Claim(ClaimTypes.Role,user.Role),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
            };
            var Token = new JwtSecurityToken
                (
                audience: jwt.Audience,
                issuer: jwt.Issuer,
                expires: DateTime.UtcNow.AddHours(jwt.Expires),
                claims: Claims,
                signingCredentials: Cords

                );
            return new JwtSecurityTokenHandler().WriteToken(token: Token);
        }
        private string RefreshToken()
        {
            var red = new byte[32];
            var randomByte = System.Security.Cryptography.RandomNumberGenerator.Create();
            randomByte.GetBytes(red);
            return Convert.ToBase64String(red);
        }
        public async Task<AddNewUserReplayDto> AddNewUserDto(AddNewUserDto add)
        {
            var IsExist = await users.IsExist(add.Email);
            if (IsExist != null)
            {
                throw new InvalidOperationException("The User Already Is Exist");

            }
            var NewUser = mapper.Map<User>(add);
            NewUser.Password = BCrypt.Net.BCrypt.HashPassword(NewUser.Password);
            await users.AddNewUser(NewUser);
            await users.SaveUser();
            return new AddNewUserReplayDto
            {
                Message = "Success Add New User",
                Username = NewUser.Username,
            };
        }

        public async Task<DeleteUserByIdReplayDto> RemovedUserById(int id)
        {
            var IsExists = await users.IsExistById(id);
            if (IsExists == null)
            {
                throw new InvalidOperationException("The User Invalid");
            }
            await users.RemoveById(IsExists);
            await users.SaveUser();
            return new DeleteUserByIdReplayDto
            {
                Message = "Success Remove User"
            };
        }
        public async Task<SingInUserReplayDto> SingInUser(SingInUserDto singIn)
        {
            var IsExist = await users.IsExist(singIn.Email);
            if (IsExist == null)
            {
                throw new InvalidOperationException("The User Is Invalid");

            }
            var VerifyPassword = BCrypt.Net.BCrypt.Verify(singIn.Password, IsExist.Password);
            if (!VerifyPassword)
            {
                throw new InvalidOperationException("The Password Is Invalid");
            }
            var Token = GenrationToken(IsExist);
            var RefreshTokens = RefreshToken();
            IsExist.Token = Token;
            IsExist.RefresgToken = RefreshTokens;
            users.SaveUser();
            return new SingInUserReplayDto
            {
                Email = IsExist.Email,
                Token = Token,
                Message = "Success Login"

            };
        }
        public async Task<UpdateUserDtoReplay> UpdateUser(UpdateUserDto userDto, int Id)
        {
            var IsExist = await users.IsExistById(Id);
            if (IsExist == null)
            {
                throw new InvalidOperationException("The User Invalid");
            }
            IsExist.Username = userDto.Username;
            IsExist.Email = userDto.Email;
            IsExist.Role = userDto.Role;

            IsExist.Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password);
            await users.SaveUser();
            return new UpdateUserDtoReplay
            {
                Messages = "Success Update "
            };

        }
        public async Task<List<User>> GetUsers()
        {
            return await users.GetAllUser();
        }
    }
}
