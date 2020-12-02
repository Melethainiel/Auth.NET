using Refit;

namespace Auth.NET.Models
{
    public class TokenRequest
    {
        public TokenRequest(string? code, string? redirectUri, string? clientId, string? codeVerifier,
            string? clientSecret, string? scope, string? grantType)
        {
            Code = code;
            RedirectUri = redirectUri;
            ClientId = clientId;
            CodeVerifier = codeVerifier;
            ClientSecret = clientSecret;
            Scope = scope;
            GrantType = grantType ?? "authorization_code";
        }

        [AliasAs("code")] public string? Code { get; }
        [AliasAs("redirect_uri")] public string? RedirectUri { get; }
        [AliasAs("client_id")] public string? ClientId { get; }
        [AliasAs("code_verifier")] public string? CodeVerifier { get; }
        [AliasAs("client_secret")] public string? ClientSecret { get; }
        [AliasAs("scope")] public string? Scope { get; }
        [AliasAs("grant_type")] public string? GrantType { get; }
        
    }
}