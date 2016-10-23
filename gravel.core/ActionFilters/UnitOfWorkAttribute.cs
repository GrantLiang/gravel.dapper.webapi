using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using gravel.core.UnitOfWorks;
using Microsoft.Practices.Unity;

namespace gravel.core.ActionFilters
{
    public class UnitOfWorkAttribute : ActionFilterAttribute
    {
        [Dependency]
        public IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

        private IUnitOfWork _unitOfWork;
        //public bool Transaction { get; set; }

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            this._unitOfWork = UnitOfWorkFactory.Create();
            this._unitOfWork.Context.BeginTransaction();
            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(HttpActionExecutedContext filterContext)
        {
            if (filterContext.Exception == null)
            {
                this._unitOfWork.SaveChange();
            }
            else
            {
                this._unitOfWork.RollBack();
            }
            base.OnActionExecuted(filterContext);
        }
    }
}
