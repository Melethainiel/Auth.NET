using System;

namespace Auth.NET.Models
{
    public class GoogleOptions : IOptions
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string ApiBaseAddress { get; set; } = "https://www.googleapis.com/oauth2";
        public string BaseAddress { get; set; }  = "accounts.google.com";
        public string? RedirectUrl { get; set; }


        public void AssertIsValid()
        {
            foreach (var property in GetType().GetProperties())
            {
                var propertyValue = property.GetValue(this);
                if(!(propertyValue is {}))
                    throw new ArgumentNullException(property.Name);
            }
        }
    }
}