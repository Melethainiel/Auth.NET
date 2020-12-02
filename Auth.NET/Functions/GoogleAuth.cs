using System;
using Auth.NET.Models;
using Auth.NET.Requests;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;

namespace Auth.NET.Functions
{
    public class GoogleAuth : AuthBase
    {
        public GoogleAuth(ILocalStorageService localStorage, IOptions<GoogleOptions> optionValue, IGoogleRequests requests, NavigationManager navigationManager) 
            : base(localStorage, optionValue.Value, requests, navigationManager)
        {
            Route = "signin-google";
        }

        protected override Uri GenerateUri()
        {
            Path = "/o/oauth2/v2/auth";
            return base.GenerateUri();
        }
    }
}