using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models;
using WPFClientExample.Repositories;
using WPFClientExample.Services;
using WPFClientExample.ViewModels;
using WPFClientExample.ViewModels.Login;
using WPFClientExample.Views;
using WPFClientExample.Views.Login;

namespace WPFClientExample
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, IRecipient<ProgramShutDownMessage>, IRecipient<LogoutMessage>, IRecipient<LoginMessage>
    {
        private static Mutex? mutex;
        private readonly IHost host;
        private const string appMutextName = "WPFClientExample_Mutext";

        public App()
        {
            mutex = new Mutex(true, appMutextName, out bool isNewInstance);

            if (!isNewInstance)
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
                services.AddSingleton<MainViewModel>();

                //Repository 등록
                services.AddSingleton<IUserRepository, UserRepository>();
                services.AddSingleton<IMenuRepository, MenuRepository>();

                //Service 등록 
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<IAuthService, AuthService>();

                // ViewModel 등록
                services.AddSingleton<ILoginWindowModel, LoginWindowModel>();
                services.AddSingleton<SearchViewModel>();
                services.AddSingleton<DetailViewModel>();
                services.AddSingleton<ReportViewModel>();
                services.AddSingleton<SettingsViewModel>();
                services.AddSingleton<NotificationLogViewModel>();
                services.AddSingleton<AdminViewModel>();

                // View 등록 (싱글톤)
                services.AddSingleton<SearchView>();
                services.AddSingleton<DetailView>();
                services.AddSingleton<ReportView>();
                services.AddSingleton<SettingsView>();
                services.AddSingleton<NotificationLogView>();
                services.AddSingleton<AdminView>();

                //Window 등록
                services.AddSingleton<MainWindow>();
                services.AddSingleton<LoginWindow>();
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            SettingMessage();
            LogOutProcess();
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            mutex?.ReleaseMutex();
            await host.StopAsync();
            base.OnExit(e);
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<ProgramShutDownMessage>(this);
            WeakReferenceMessenger.Default.Register<LogoutMessage>(this);
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(ProgramShutDownMessage message)
        {
            ProgramShutdownProcess();
        }

        public void Receive(LogoutMessage message)
        {
            LogOutProcess();
        }

        public void Receive(LoginMessage message)
        {
            LogInProcess(message.Value);
        }

        private void LogInProcess(UserInfo? userInfo)
        {
            host.Services.GetRequiredService<LoginWindow>().Hide();
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Visible;
        }

        private void LogOutProcess()
        {
            host.Services.GetRequiredService<MainWindow>().Visibility = Visibility.Hidden;
            host.Services.GetRequiredService<LoginWindow>().Show();
        }

        private void ProgramShutdownProcess()
        {
            host.Services.GetRequiredService<LoginWindow>().Close();
            host.Services.GetRequiredService<MainWindow>().Close();
            Application.Current.Shutdown();
        }
    }

}
