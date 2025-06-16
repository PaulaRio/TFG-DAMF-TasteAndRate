using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using TasteAndRate.ViewModel;
using TasteAndRate.DTO;
using TasteAndRate.Interfaces;
using TasteAndRate.Service;
using TasteAndRate.Services;
using TasteAndRate.View;
using TasteAndRate.ViewModel;
using TasteAndRate.DTOs;

namespace TasteAndRate
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Services = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = Current.Services.GetService<MainWindow>();
            mainWindow?.Show();
        }
        public new static App Current => (App)Application.Current;
        public IServiceProvider Services { get; }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //view principal
            services.AddTransient<MainWindow>();
           

            //view viewModels
            services.AddTransient<MainViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegistrationViewModel>();
            services.AddTransient<OverviewViewModel>();
            services.AddTransient<StackPanelViewModel>();
            services.AddTransient<DataGridViewModel>();
            services.AddTransient<AddValoracionViewModel>();
            services.AddTransient<AddEtiquetaViewModel>();
            services.AddTransient<AddCriterioViewModel>();


            //Services
            services.AddSingleton<LoginDTO>();
            services.AddSingleton<User>();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton(typeof(IFileService<>), typeof(FileService<>));
            services.AddSingleton(typeof(IJsonObjectFileService<>), typeof(JsonObjectFileService<>));
            services.AddSingleton(typeof(IHttpsJsonClientProvider<>), typeof(HttpsJsonClientService<>));
            services.AddSingleton<IEtiquetaProvider, EtiquetaService>();
            services.AddSingleton<ILoginProvider, LoginService>();
            services.AddSingleton<IValoracionProvider, ValoracionService>();
            services.AddSingleton<ICriterioProvider, CriterioService>();
            services.AddSingleton<IGastroProvider, GastroService>();
            services.AddSingleton<IValoracionCriterioProvider, ValoracionCriterioService>();
            services.AddSingleton<IStringUtils, StringUtils>();
            return services.BuildServiceProvider();
        }
    }

}
