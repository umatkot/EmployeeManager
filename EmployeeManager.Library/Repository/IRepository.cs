using EmployeeManager.DataLayer.RepositoryDataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Library.Repository
{
    public interface IRepository <T> where T : class
    {
        IQueryable<T> GetAll();
        
        /*
        IQueryable<T> Find(int id);
        IQueryable<T> Find(string name);
        */

        void Add(T entity);
        
        /*
        void Update(T entity);

        void Delete(T entity);
        */

        void DeleteAll();
        
    }
}
