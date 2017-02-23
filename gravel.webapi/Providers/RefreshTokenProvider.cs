using System;
using System.Threading.Tasks;
using System.Web.Http;
using gravel.core.UnitOfWorks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataHandler.Serializer;
using Microsoft.Owin.Security.Infrastructure;


namespace xLab.webapi.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {

        private IUnitOfWorkFactory _unitOfWorkFactory;
        private IUnitOfWork _unitOfWork;


        //GetByTokenGuid
        private readonly int _expireMonth = 3;

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var guid = Guid.NewGuid();

            //copy properties and set the desired lifetime of refresh token
            var refreshTokenProperties = new AuthenticationProperties(context.Ticket.Properties.Dictionary)
            {
                IssuedUtc = context.Ticket.Properties.IssuedUtc,
                ExpiresUtc = DateTime.UtcNow.AddMonths(_expireMonth)
            };

            var refreshTokenTicket = new AuthenticationTicket(context.Ticket.Identity, refreshTokenProperties);

            await StoreAsync(guid, refreshTokenTicket);
            context.SetToken(guid.ToString());
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {

            AuthenticationTicket ticket = await RemoveAsync(context);
            if (ticket != null)
            {
                context.SetTicket(ticket);
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }

        private async Task StoreAsync(Guid guid, AuthenticationTicket ticket)
        {
            TicketSerializer serializer = new TicketSerializer();
            byte[] serialize = serializer.Serialize(ticket);
            
            //....

            this._unitOfWorkFactory = (UnitOfWorkFactory)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWorkFactory));
            this._unitOfWork = this._unitOfWorkFactory.Create();
            this._unitOfWork.Context.BeginTransaction();
            try
            {
                // insert token to db....
                this._unitOfWork.SaveChange();
            }
            catch (Exception e)
            {
                this._unitOfWork.RollBack();
            }

        }

        private async Task<AuthenticationTicket> RemoveAsync(AuthenticationTokenReceiveContext context)
        {
            Guid tokenGuid = new Guid(context.Token);

            this._unitOfWorkFactory = (UnitOfWorkFactory)GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWorkFactory));
            this._unitOfWork = this._unitOfWorkFactory.Create();
            this._unitOfWork.Context.BeginTransaction();
            try
            {
                //... get from db....
                var getRefreshToken = "...";
              

                if (getRefreshToken != null)
                {
                    TicketSerializer serializer = new TicketSerializer();
                    var ticket = TextEncodings.Base64Url.Decode("refresh_token.......");
                    //....Repository.Delete(getRefreshToken);
                    //this._unitOfWork.SaveChange();
                    return serializer.Deserialize(ticket);
                }
                else
                {
                    return null;
                }

            }
            catch (Exception e)
            {
                this._unitOfWork.RollBack();
                return null;
            }

        }

    }
}
