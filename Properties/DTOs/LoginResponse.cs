namespace ToCarAPI.DTOs
{
    public class LoginResponse
    {
        public string UserId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}