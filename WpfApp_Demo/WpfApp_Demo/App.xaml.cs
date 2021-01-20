using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Infra.Repositories;
using Entities.Interface;
using System.IO;
using Microsoft.Extensions.Configuration;


namespace WpfApp_Demo
{
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }

        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            var connectionString = "DataSource = .\\WpfAppDb.db";
            services.AddScoped<IListPropertyRepository>(lpr => new ListPropertyRepository(connectionString));
            services.AddScoped<IItemPropertyRepository>(lpr => new ItemPropertyRepository(connectionString));
            services.AddSingleton<MainWindow>();
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var mainWindow = serviceProvider.GetService<MainWindow>();
            mainWindow.Show();
        }
    }
}

