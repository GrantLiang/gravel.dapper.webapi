using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gravel.core.DbContextFactory;
using Microsoft.Practices.Unity;
using System.Web.Http;

namespace gravel.core.UnitOfWorks
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IUnitOfWork _unitOfWork;

        public IUnitOfWork Create()
        {
            if (this._unitOfWork != null)
            {
                return this._unitOfWork;
            }
            this._unitOfWork = GlobalConfiguration.Configuration.DependencyResolver.GetService(typeof(IUnitOfWork)) as IUnitOfWork;
            return this._unitOfWork;
        }
    }
}
