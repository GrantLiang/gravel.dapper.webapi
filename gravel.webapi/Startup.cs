using System;
using System.Threading.Tasks;
using System.Web.Http;
using gravel.core.Providers;
using gravel.webapi.App_Start;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Practices.Unity.WebApi;
using Owin;

[assembly: OwinStartup(typeof(gravel.webapi.Startup))]

namespace gravel.webapi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app);
           
            app.UseWebApi(config);
            
        }

        public void ConfigureOAuth(IAppBuilder app)
        {

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //HTTPS is allowed only AllowInsecureHttp = false
                AllowInsecureHttp = true,

                TokenEndpointPath = new PathString("/Auth"),

                AccessTokenExpireTimeSpan = TimeSpan.FromDays(30),
                
                Provider = new ApplicationOAuthProvider(),
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

        }
    }
}
