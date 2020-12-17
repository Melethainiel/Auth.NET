# Auth.NET

# How to use

## Add the package

`PM> Install-Package Auth.NET`

## In AppSettings.json

Add the following configuration :

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "{...}",
      "ClientSecret": "{...}"
    },
    "Microsoft": {
      "ClientId": "{...}",
      "ClientSecret": "{...}"
    }
  }
}
```

## In Startup.cs

Add the following lines :

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddGoogleAuth(Configuration);
    services.AddMicrosoftAuth(Configuration);
}
```
## In the page/component

```c#
@inject MicrosoftAuth MicrosoftAuth

private async Task OnMicrosoftClick()
{
    await MicrosoftAuth.Connect("openid");
}
```
