
This project is a demonstrate asp.net core app that does not keep a user identity database, but rely on external authentication provider for user authentication.  The common use case is site that allow user to comment or contact you without register an account first.  This demon only show google authentication.  It does not produce nuget package because it only support asp.net core mvc, if you use Razor Page or many likely usage (like add Facebook provider) scenerios require modification of the source code, 

How to use :

1. Create a asp.net core mvc app WITHOUT authentication
2. Add project reference to PonyGoogleOnlyAuthentication
3. Add the following code to ConfigureServices

        services.AddGoogleOnlyAuthentication( options =>
            {
                // options is GoogleOptions class, exactly the same options as        .AddGoogle(opt)
                options.ClientId = "YourClientId";
                options.ClientSecret = "YourClientSecret";


            });
4. Add the following to Configure

          app.UseRouting();

          app.UseAuthentication();

5. Add this line to Views/Shared/_Layout.cshtml  to show the login/logout ui

    <partial name="_LoginPartialGoogle" />


The following link explain the detail implementation

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/social-without-identity?view=aspnetcore-5.0

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims?view=aspnetcore-5.0


Only a few things require explanations here 

1. this line in GoogleOnlyAuthenticationExtension will force the select user account screen to appear with every login,  if it is not there, google will signin silently if the user already logged in to google previous.  This leave user no chance to change login account

               options.AuthorizationEndpoint += "?prompt=consent";

2. PonyGoogleOnlyAuthentication is implemented as Razor class library, this is the mechanism that allows your to only add code in 3 places and have a fully functional Google Authentication. Reference link:

https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-5.0&tabs=visual-studio