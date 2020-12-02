using System.Threading;
using System.Threading.Tasks;
using Auth.NET.Models;
using Refit;

namespace Auth.NET.Requests
{
    public interface IMicrosoftRequests : IRequests
    {
        [Post("/common/oauth2/v2.0/token")]
        new Task<ApiResponse<TokenResponse>> GetToken([Body(BodySerializationMethod.UrlEncoded)] TokenRequest content, CancellationToken cancellationToken = default);
        
        
    }
}