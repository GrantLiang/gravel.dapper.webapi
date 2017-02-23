using System.Collections.Generic;
using System.Linq;
using gravel.core.Bases;
using gravel.core.Database;
using gravel.core.UnitOfWorks;
using gravel.service.Data;
using Microsoft.Practices.Unity;

namespace gravel.service.ServiceManager.Account
{
    public interface IAccountManager
    {
        bool Login();
    }

    public class AccountManager : RepositoryBase , IAccountManager
    {
        //[Dependency]
        //public IEmployeesRepository _employeesRepository { get; set; }

        public AccountManager(UnitOfWork uow)
            :base (uow)
        {

        }

        public bool Login()
        {
            return true;
        }
    }
}
