using EmployeeManager.DataLayer.RepositoryDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Library
{
    internal static class DbContextExtensions
    { 
        public static IQueryable<object> Set(this EmployeeDbEntities _context, Type t)
        {
            return (IQueryable<object>)_context.GetType().GetMethod("Set").MakeGenericMethod(t).Invoke(_context, null);
        }
    }
}
