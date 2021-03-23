namespace Epsic.Authx.Models
{
    public class AuthResponse
    {
        public string Token { get; set; }
        public bool Result { get; set; }
        public string Message { get; set; }
    }
}