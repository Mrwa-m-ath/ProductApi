namespace ProductApi.Dto.DtoUser
{
    public class AddNewUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class AddNewUserReplayDto
    {
        public string Username { get; set; }
        public string Message { get; set; }
    }
}
