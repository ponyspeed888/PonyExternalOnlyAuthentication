using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;


namespace PonyGoogleOnlyAuthentication
{
    public static class GoogleOnlyAuthenticationExtension
    {
  
        public static AuthenticationBuilder AddGoogleOnlyAuthenticationBasic(this IServiceCollection services, string ClientID, string ClientSecret)
        {

            return services.AddGoogleOnlyAuthentication(options =>
            {
                options.ClientId = ClientID;
                options.ClientSecret = ClientSecret;


            });

        }
        public static AuthenticationBuilder AddGoogleOnlyAuthentication(this IServiceCollection services, Action<GoogleOptions> configureOptions)
        {

            Action<GoogleOptions> opt = options => {


                configureOptions.Invoke(options);
                if (options.ClientId == null || options.ClientSecret == null)
                    throw new Exception("Must include clientId and ClientSecret");

                options.AuthorizationEndpoint += "?prompt=consent";

                options.ClaimActions.Clear();
                options.ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id");
                options.ClaimActions.MapJsonKey(ClaimTypes.Name, "name");
                options.ClaimActions.MapJsonKey(ClaimTypes.GivenName, "given_name");
                options.ClaimActions.MapJsonKey(ClaimTypes.Surname, "family_name");
                options.ClaimActions.MapJsonKey("urn:google:profile", "link");
                options.ClaimActions.MapJsonKey(ClaimTypes.Email, "email");
                options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                options.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");


            };


            AuthenticationBuilder builder = services.AddAuthentication(options =>
            {
                options.DefaultScheme = Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = Microsoft.AspNetCore.Authentication.Google.GoogleDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {


            })
            .AddGoogle(opt)


            ;

            return builder;

        }
    }

}
