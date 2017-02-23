using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using gravel.core.UnitOfWorks;
using gravel.service.ServiceManager.Account;
using Microsoft.Owin.Security.OAuth;


namespace gravel.webapi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
  

        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;

        /// <summary>
        /// valid data
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return base.ValidateClientAuthentication(context);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            this._unitOfWorkFactory = (UnitOfWorkFactory)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWorkFactory));
            this._unitOfWork = this._unitOfWorkFactory.Create();
            this._unitOfWork.Context.BeginTransaction();
            try
            {
                var accountManager = (AccountManager)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IAccountManager));

                var getUserProf = accountManager.Login();
                this._unitOfWork.SaveChange();

                if (getUserProf)
                {
                    var id = new ClaimsIdentity(context.Options.AuthenticationType);
           
                    id.AddClaim(new Claim("User", "Tester"));

                    context.Validated(id);
                }
                else
                {
                    context.Rejected();
                }
            }
            catch (Exception e)
            {
            
                this._unitOfWork.RollBack();
            }



            return;
            //context.Request.Context.Authentication.SignIn(id);

        }

        public override Task GrantCustomExtension(OAuthGrantCustomExtensionContext context)
        {
    
            return base.GrantCustomExtension(context);
        }


    }
}
