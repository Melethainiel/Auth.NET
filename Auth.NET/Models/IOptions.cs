namespace Auth.NET.Models
{
    public interface IOptions
    {
        string ClientId { get; set; }
        string ClientSecret { get; set; }
        string ApiBaseAddress { get; set; }
        string BaseAddress { get; set; }
        string? RedirectUrl { get; set; }
    }
}