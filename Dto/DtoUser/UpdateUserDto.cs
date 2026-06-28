namespace ProductApi.Dto.DtoUser
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class UpdateUserDtoReplay
    {
        public string Messages { get; set; }

    }
}
