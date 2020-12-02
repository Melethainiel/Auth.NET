using System;

namespace Auth.NET.Models
{
    public class MicrosoftOptions : IOptions
    {
        public string ClientId { get; set; } = null!;
        public string ClientSecret { get; set; } = null!;
        public string ApiBaseAddress { get; set; } = "graph.microsoft.com";
        public string BaseAddress { get; set; } = "login.microsoftonline.com";
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