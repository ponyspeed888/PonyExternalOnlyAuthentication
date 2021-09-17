
This project demonstrate asp.net core app that does not keep a user identity database, but rely on external authentication provider for user authentication.  The common use case is site that allow user to comment or contact you without register an account first.  Please note it use NuGet packages that only support asp.net core mvc, and only single external ID provider only, in order to simplfied the implementation. This limit the usefulness in commerical app, but is very good as a starting learning package for external authentication, because once you have the clientID and ClientSecret right, it has very little room for error for beginner.


Currently, it support Google, Microsoft, Github providers

How to use :

1. Create a asp.net core mvc app WITHOUT authentication
2. Add project reference to PonyGoogleOnlyAuthentication
3. Add the following code to ConfigureServices

        services.AddGoogleOnlyAuthenticationBasic("Your Client ID", "Your Client Secret") ;

    or use the full option    


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

    <partial name="_LoginPartialSingleExtOnly" />

6. For other providers, use

    services.AddMicrosoftOnlyAuthenticationBasic
    services.AddGithubOnlyAuthenticationBasic



The following link explain the detail implementation

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/social-without-identity?view=aspnetcore-5.0

https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/additional-claims?view=aspnetcore-5.0


Only a few things require explanations here 

1. this line in GoogleOnlyAuthenticationExtension will force the select user account screen to appear with every login,  if it is not there, google will signin silently if the user already logged in to google previous.  This leave user no chance to change login account

               options.AuthorizationEndpoint += "?prompt=consent";

2. PonyGoogleOnlyAuthentication is implemented as Razor class library, this is the mechanism that allows your to only add code in 3 places and have a fully functional Google Authentication. Reference link:

https://docs.microsoft.com/en-us/aspnet/core/razor-pages/ui-class?view=aspnetcore-5.0&tabs=visual-studio