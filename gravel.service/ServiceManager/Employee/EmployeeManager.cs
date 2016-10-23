using System.Collections.Generic;
using System.Linq;
using gravel.core.Bases;
using gravel.core.Database;
using gravel.core.UnitOfWorks;
using gravel.service.Data;
using Microsoft.Practices.Unity;

namespace gravel.service.ServiceManager.Employee
{
    public interface IEmployeeManager
    {
        IList<Employees> AddEmp();
    }

    public class EmployeeManager : RepositoryBase , IEmployeeManager
    {
        [Dependency]
        public IEmployeesRepository _employeesRepository { get; set; }

        public EmployeeManager(UnitOfWork uow)
            :base (uow)
        {

        }

        public IList<Employees> AddEmp()
        {
            var getEmp = _employeesRepository.GetAll();
           
            var getFirst = getEmp.FirstOrDefault();
            getFirst.FirstName = "gravel";
            _employeesRepository.Update(getFirst);
            
            //throw new System.Exception("exception !!!!");
            
            //select again
            getEmp = _employeesRepository.GetAll();
            
            return getEmp;
        }
    }
}
