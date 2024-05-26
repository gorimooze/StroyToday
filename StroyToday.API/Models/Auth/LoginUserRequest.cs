namespace StroyToday.API.Models.Auth
{
    public class LoginUserRequest
    {
        public string Email { get; set;}
        public string Password { get; set;}
        public string TimeZone { get; set;}
    }
}
