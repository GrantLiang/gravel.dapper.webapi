using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gravel.core.DbContextFactory;
using gravel.core.UnitOfWorks;

namespace gravel.core.Bases
{
    public class RepositoryBase
    {
        
        public readonly UnitOfWork _unitOfwork;
        public readonly DapperContext _context;
        public RepositoryBase(UnitOfWork unitOfwork)
        {
            _unitOfwork = unitOfwork;
            _context = unitOfwork.Context;
        }
    }
}
