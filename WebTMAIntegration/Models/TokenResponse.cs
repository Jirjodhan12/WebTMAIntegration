namespace WebTMAIntegration.Models
{
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string UserName { get; set; }
    }
}
