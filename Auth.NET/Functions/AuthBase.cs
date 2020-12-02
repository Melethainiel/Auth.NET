using System;
using System.Collections.Specialized;
using System.Threading.Tasks;
using System.Web;
using Auth.NET.Misc;
using Auth.NET.Models;
using Auth.NET.Requests;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace Auth.NET.Functions
{
    public class AuthBase
    {
        private string _state;
        private string _codeVerifier;
        private string _codeChallenge;
        private string _codeChallengeMethod;
        protected string Route;

        protected AuthBase(ILocalStorageService localStorage, IOptions optionValue,
            IRequests requests, NavigationManager navigationManager)
        {
            LocalStorage = localStorage;
            OptionValue = optionValue;
            Requests = requests;
            NavigationManager = navigationManager;
            Parameters = HttpUtility.ParseQueryString(string.Empty);
        }

        private ILocalStorageService LocalStorage { get; }
        private IOptions OptionValue { get; }
        private IRequests Requests { get; }
        private NavigationManager NavigationManager { get; }
        protected NameValueCollection Parameters { get; }
        protected string Path { get; set; }

        public async Task Connect(string scope)
        {
            GenerateAuthData();
            Parameters["scope"] = scope;
            var url = GenerateUri();
            await StoreData();
            NavigationManager.NavigateTo(url.ToString());
        }

        private void GenerateAuthData()
        {
            _state = AuthHelper.RandomDataBase64Url(32);
            _codeVerifier = AuthHelper.RandomDataBase64Url(32);
            _codeChallenge = AuthHelper.Base64UrlEncodeNoPadding(AuthHelper.Sha256(_codeVerifier));
            _codeChallengeMethod = "S256";
        }

        protected virtual Uri GenerateUri()
        {
            Parameters["client_id"] = OptionValue.ClientId;
            Parameters["response_type"] = "code";
            Parameters["redirect_uri"] = OptionValue.RedirectUrl ?? $"{NavigationManager.BaseUri}{Route}";
            Parameters["state"] = _state;
            Parameters["code_challenge"] = _codeChallenge;
            Parameters["code_challenge_method"] = _codeChallengeMethod;


            var uriBuilder = new UriBuilder
            {
                Scheme = "https",
                Host = OptionValue.BaseAddress,
                Path = Path,
                Query = Parameters.ToString()!
            };
            return uriBuilder.Uri;
        }


        private async Task StoreData()
        {
            await LocalStorage.SetItemAsync("state", _state);
            await LocalStorage.SetItemAsync("code_verifier", _codeVerifier);
        }

        public async Task<TokenResponse> GetToken(Uri uri)
        {
            var code = await GetCode(uri);
            var token = await PerformCodeExchange(code);
            await ClearData();
            return token;
        }

        private async Task<string> GetCode(Uri uri)
        {
            var query = uri.Query;

            if (QueryHelpers.ParseQuery(query).TryGetValue("error", out var error))
                throw new Exception(error);

            if (!QueryHelpers.ParseQuery(query).TryGetValue("code", out var code) ||
                !QueryHelpers.ParseQuery(query).TryGetValue("State", out var state))
                throw new Exception("code or state is null");

            var localState = await LocalStorage.GetItemAsStringAsync("state");

            if (state != localState)
                throw new Exception("invalid state");

            return code;
        }

        private async Task<TokenResponse> PerformCodeExchange(string code)
        {
            var redirectUrl = OptionValue.RedirectUrl ?? $"{NavigationManager.BaseUri}{Route}";
            var codeVerifier = await LocalStorage.GetItemAsStringAsync("code_verifier");


            var tokenRequest = new TokenRequest(code, redirectUrl, OptionValue.ClientId, codeVerifier,
                OptionValue.ClientSecret, "", null);

            // gets the response
            var tokenResponse = await Requests.GetToken(tokenRequest);
            if (!tokenResponse.IsSuccessStatusCode)
                throw new Exception(tokenResponse.Error.Message);
            var tokenResponseContent = tokenResponse.Content;
            return tokenResponseContent;
        }

        private async Task ClearData()
        {
            await LocalStorage.RemoveItemAsync("state");
            await LocalStorage.RemoveItemAsync("code_verifier");
        }
    }
}