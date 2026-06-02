namespace WebTMAIntegration.Services.Interfaces
{
    public interface ITokenService
    {
        Task<string> GetTokenAsync();
        //Task GenerateTokenAsync();
    }
}
