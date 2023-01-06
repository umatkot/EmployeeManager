using Autofac;
using EmployeeManager.Main.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace EmployeeManager.Main.Startup
{
    public class Bootstrapper
    {
        public static IContainer Bootstrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>()
                .AsSelf();

            builder.RegisterType<EmployeeViewModel>()
                .AsSelf().SingleInstance();

            builder.RegisterType<EmployeeDetailViewModel>()
                .AsSelf();

            return builder.Build();
        }
    }
}
