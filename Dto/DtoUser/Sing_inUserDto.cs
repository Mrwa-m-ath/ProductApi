namespace ProductApi.Dto.DtoUser
{
    public class SingInUserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class SingInUserReplayDto
    {
        public string Email { get; set; }

        public string Message { get; set; }
        public string Token { get; set; }

    }
}
