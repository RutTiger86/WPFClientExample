﻿using CommunityToolkit.Mvvm.Messaging;
using CSharp.WPF.MVVM.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Configuration;
using System.Data;
using System.Windows;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Commons.Statics;
using WPFClientExample.Models.DataBase;
using WPFClientExample.Repositories;
using WPFClientExample.Services;
using WPFClientExample.ViewModels;
using WPFClientExample.ViewModels.Login;
using WPFClientExample.ViewModels.UserInfo;
using WPFClientExample.Views;
using WPFClientExample.Views.Login;
using WPFClientExample.Views.UserInfo;

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

                //Repository 등록
                services.AddScoped<IAuthRepository, AuthRepository>();
                services.AddScoped<IMenuRepository, MenuRepository>();
                services.AddScoped<IUserRepository, UserReository>();
                services.AddScoped<IServerRepository, ServerRepository>();

                //Service 등록 
                services.AddScoped<INavigationService, NavigationService>();
                services.AddScoped<IAuthService, AuthService>();
                services.AddScoped<IGameLogService, GameLogService>();
                services.AddScoped<IMonitoringService, MonitoringService>();
                services.AddScoped<IBillingService, BillingService>();
                services.AddScoped<ISettingService, SettingService>();

                // ViewModel 등록
                services.AddSingleton<IAdminSettingViewModel, AdminSettingViewModel>();
                services.AddSingleton<IBillHistoryViewModel, BillHistoryViewModel>();
                services.AddSingleton<ICcuMonitoringViewModel, CcuMonitoringViewModel>();
                services.AddSingleton<IChatLogViewModel, ChatLogViewModel>();
                services.AddSingleton<IProductInfoViewModel, ProductInfoViewModel>();
                services.AddSingleton<IUserInfoViewModel, UserInfoViewModel>();
                services.AddSingleton<ISettingsViewModel, SettingsViewModel>();
                services.AddSingleton<ICharacterInfoViewModel, CharacterInfoViewModel>();
                services.AddSingleton<IInventoryLogViewModel, InventoryLogViewModel>();

                // View 등록 
                services.AddSingleton<AdminSettingView>();
                services.AddSingleton<BillHistoryView>();
                services.AddSingleton<CcuMonitoringView>();
                services.AddSingleton<ChatLogView>();
                services.AddSingleton<ProductInfoView>();
                services.AddSingleton<SettingsView>();
                services.AddSingleton<UserInfoView>();
                services.AddSingleton<CharacterInfoView>();
                services.AddSingleton<InventoryLogView>();


                //WindowModel 등록
                services.AddSingleton<IMainWindowModel,MainWindowModel>();
                services.AddSingleton<ILoginWindowModel, LoginWindowModel>();

                //Window 등록
                services.AddSingleton<MainWindow>();
                services.AddSingleton<LoginWindow>();
            });
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TestDataFactory.InitTestData();
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

        private void LogInProcess(AuthAccount? userInfo)
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
