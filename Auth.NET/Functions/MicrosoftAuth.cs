using System;
using Auth.NET.Models;
using Auth.NET.Requests;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Auth.NET.Functions
{
    public class MicrosoftAuth : AuthBase
    {
        public MicrosoftAuth(ILocalStorageService localStorage, IOptions<MicrosoftOptions> optionValue, IMicrosoftRequests requests, NavigationManager navigationManager) 
            : base(localStorage, optionValue.Value, requests, navigationManager)
        {
            Route = "signin-microsoft";
        }

        protected override Uri GenerateUri()
        {
            Parameters["response_mode"] = "query";
            Parameters["prompt"] = "select_account";
            Path = "common/oauth2/v2.0/authorize";
            return base.GenerateUri();
        }
    }
}