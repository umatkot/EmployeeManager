using EmployeeManager.Library.Repository;
using JetBrains.Annotations;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Main.ViewModel
{
    public class ViewModelBase : BindableBase
    {
        /// <summary>
        /// ViewModel version
        /// </summary>
        protected Guid Id { get; }

        //public ModelRepository repository;
        public ViewModelBase(string childModelName)
        {
            Id = Guid.NewGuid();
            Debug.Write($"Loaded child {childModelName}");
        }
    }
}
