using System.Threading;
using System.Threading.Tasks;
using Auth.NET.Models;
using Refit;

namespace Auth.NET.Requests
{
    public interface IRequests
    {
        [Post("/")]
        Task<ApiResponse<TokenResponse>> GetToken([Body(BodySerializationMethod.UrlEncoded)] TokenRequest content, CancellationToken cancellationToken = default);
    }
}