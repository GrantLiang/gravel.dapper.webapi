using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gravel.core.Bases;
using gravel.core.Database;
using gravel.core.DbContextFactory;
using gravel.core.UnitOfWorks;
using gravel.service.Domain;

namespace gravel.service.Data
{
    public interface IEmployeesRepository : IRepository<Employees>
    {

    }

    public class EmployeesRepository : RepositoryBase, IEmployeesRepository
    {
      
        public EmployeesRepository(UnitOfWork uow)
            : base(uow)
        {

        }

        public IList<Employees> GetAll()
        {
            return _context.Query<Employees>("SELECT * FROM Employees").ToList();
        }

        public void Insert(Employees entity)
        {
            throw new NotImplementedException();
        }

        //public void Insert(Product product)
        //{
        //    _context.Execute("INSERT INTO Products VALUES (@Name, @Price)", product);
        //}

        public void Update(Employees entity)
        {
            _context.Execute("UPDATE Employees SET FirstName=@FirstName WHERE EmployeeID=@EmployeeID", entity);
        }

        //public Product GetById(int id)
        //{
        //    return _context.Query<Product>("SELECT * FROM Products WHERE Id=@Id", new { Id = id }).FirstOrDefault();
        //}
    }
}
