using Auth.NET.Misc;
using Newtonsoft.Json;

namespace Auth.NET.Models
{
    public record TokenResponse (string? AccessToken, long? ExpiresIn, string? RefreshToken, string? Scope, string? TokenType, string? IdToken)
    {


        [JsonProperty("access_token")] public string? AccessToken { get; init; } = AccessToken;

        [JsonProperty("expires_in")]
        [System.Text.Json.Serialization.JsonConverter(typeof(ParseStringConverter))]
        public long? ExpiresIn { get; init; } = ExpiresIn;

        [JsonProperty("refresh_token")] public string? RefreshToken { get; init; } = RefreshToken;

        [JsonProperty("scope")] public string? Scope { get; init; } = Scope;

        [JsonProperty("token_type")] public string? TokenType { get; init; } = TokenType;

        [JsonProperty("id_token")] public string? IdToken { get; init; } = IdToken;
        
    }
}