using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFClientExample.Services;
using WPFClientExample.ViewModels;
using WPFClientExample.Views;

namespace WPFClientExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static Mutex? mutex;
        private readonly IHost host;
        private const string appMutextName = "WPFClientExample_Mutext";

        public App()
        {
            mutex = new Mutex(true, appMutextName, out bool isNewInstance);

            if(!isNewInstance)
            {
                MessageBox.Show("The apllication is already running", "Duplicate Execution Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }

            var hostBuilder = Host.CreateDefaultBuilder();
            SetConfigureServices(hostBuilder);
            host = hostBuilder.Build();
        }

        private void SetConfigureServices(IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureServices((context, services) =>
            {
                //ViewModel 등록
                services.AddSingleton<MainVeiwModel>();

                //Service 등록 
                services.AddSingleton<INavigationService, NavigationService>();

                // ViewModel 등록
                services.AddSingleton<LoginViewModel>();
                services.AddSingleton<SearchViewModel>();
                services.AddSingleton<DetailViewModel>();
                services.AddSingleton<ReportViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<NotificationLogViewModel>();
                services.AddSingleton<AdminViewModel>();

                // View 등록 (싱글톤)
                services.AddSingleton<LoginView>();
                services.AddSingleton<SearchView>();
                services.AddSingleton<DetailView>();
                services.AddSingleton<ReportView>();
                services.AddSingleton<SettingsView>();
                services.AddSingleton<NotificationLogView>();
                services.AddSingleton<AdminView>();

                //MainWindow 등록
                services.AddSingleton<MainWindow>();
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            mutex?.ReleaseMutex();
            base.OnExit(e);
        }
    }

}
