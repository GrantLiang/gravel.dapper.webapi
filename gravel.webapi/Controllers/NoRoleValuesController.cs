using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using gravel.core.ActionFilters;
using gravel.core.Database;
using gravel.service.ServiceManager.Employee;
using Microsoft.Practices.Unity;

namespace gravel.webapi.Controllers
{
    [Authorize]
    public class NoRoleValuesController : ApiController
    {
        [Dependency]
        public IEmployeeManager _employeeRepository { get; set; }

        // GET api/values

        [UnitOfWork]
        public IList<Employees> Get()
        {
            return _employeeRepository.AddEmp();
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
