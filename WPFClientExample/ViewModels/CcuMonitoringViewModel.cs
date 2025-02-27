using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WPFClientExample.Commons.Extensions;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface ICcuMonitoringViewModel
    {
        ObservableCollection<KeyValuePair<long, string>> Servers { get; }
        long SelectedServer { get; set; }
        DateTime SelectedDate { get; set; }
        int SelectedHour { get; set; }
        int SelectedMinute { get; set; }
        int SelectedSecond { get; set; }
        bool? RealTimeChecked { get; set; }
        SeriesCollection CcuSeries { get; }
        List<string> TimeLables { get; }
        IRelayCommand SearchCommand { get; }

        int SelectedServerMaxCcu { get; set; }
        int SelectedServerMinCcu { get; set; }
        int SelectedServerLastCcu { get; set; }
        string? SelectedServerLastTime { get; set; }
    }

    public partial class CcuMonitoringViewModel : ObservableObject, ICcuMonitoringViewModel, IRecipient<LoginMessage>
    {
        private readonly IMonitoringService monitoringService;
        [ObservableProperty]
        DateTime selectedDate;

        [ObservableProperty]
        int selectedHour;

        [ObservableProperty]
        int selectedMinute;

        [ObservableProperty]
        int selectedSecond;

        [ObservableProperty]
        SeriesCollection ccuSeries = [];

        [ObservableProperty]
        List<string> timeLables = [];

        [ObservableProperty]
        bool? realTimeChecked = false;

        [ObservableProperty]
        int selectedServerMaxCcu;

        [ObservableProperty]
        int selectedServerMinCcu;

        [ObservableProperty]
        int selectedServerLastCcu;

        [ObservableProperty]
        string? selectedServerLastTime;

        [ObservableProperty]
        ObservableCollection<KeyValuePair<long, string>> servers = [];

        [ObservableProperty]
        long selectedServer;

        private const int maxDataPoint = 60;
        private readonly DispatcherTimer realTimeCcuTimer;
        private Dictionary<long, ChartValues<int>> serverCcuData = [];

        public CcuMonitoringViewModel(IMonitoringService monitoringService)
        {
            this.monitoringService = monitoringService;
            realTimeCcuTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            realTimeCcuTimer.Tick += (s, e) => UpdateCCUData();
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            InitSetting();
        }

        private void InitSetting()
        {
            CcuSeries?.Clear();
            Servers?.Clear();
            TimeLables?.Clear();
            SelectedDate = DateTime.Now;
            SelectedHour = 0;
            SelectedMinute = 0;
            SelectedSecond = 0;
            RealTimeChecked = false;
            SelectedServer = 0;
            SelectedServerMinCcu = 0;
            SelectedServerMaxCcu = 0;
            SelectedServerLastCcu = 0;
            SelectedServerLastTime = string.Empty;
        }


        [RelayCommand]
        private void Search()
        {
            DateTime searchDate = new(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
                                    SelectedHour, SelectedMinute, SelectedSecond);
            DateTime endDate = searchDate.AddHours(1);
            if (endDate > DateTime.Now)
            {
                endDate = DateTime.Now;
            }

            SetCcuChart(searchDate, endDate);
        }

        private async void SetCcuChart(DateTime startDate, DateTime endDate)
        {
            CcuSeries = [];
            TimeLables = [];
            serverCcuData = [];

            var getServerListTask = Task.Run(() => monitoringService.GetServers());
            var getCcuInfosTask = Task.Run(() => monitoringService.GetCcuSeries(startDate, endDate));

            await Task.WhenAll(getServerListTask, getCcuInfosTask);

            var serverList = getServerListTask.Result;
            var ccuInfos = getCcuInfosTask.Result;

            if (ccuInfos.Count == 0)
            {
                return;
            }

            var ccuGroups = ccuInfos.GroupBy(p => p.ServerId);
            var baseGroup = ccuGroups.First();

            Application.Current.Dispatcher.Invoke(() =>
            {
                TimeLables.AddRange(baseGroup.OrderBy(p => p.CcuValue.Key)
                                             .Select(data => data.CcuValue.Key.ToString("yy-MM-dd HH:mm:ss")));

                foreach (var ccuGroup in ccuGroups)
                {
                    var ccuValues = new ChartValues<int>(ccuGroup.Select(p => p.CcuValue.Value));
                    serverCcuData[ccuGroup.Key] = ccuValues;

                    CcuSeries.Add(new LineSeries
                    {
                        Name = $"Server{ccuGroup.Key}",
                        Title = $"Server ID : {ccuGroup.Key}",
                        Values = ccuValues,
                        PointGeometrySize = 10,
                        StrokeThickness = 2
                    });
                }

                if (serverList.Count > 0)
                {
                    Servers.Clear();
                    Servers.Add(new KeyValuePair<long, string>(0, "ALL"));
                    Servers.AddRange(serverList.Select(server => new KeyValuePair<long, string>(server.Id, server.ServerName)));

                    SelectedServer = Servers.First().Key;
                    OnPropertyChanged(nameof(SelectedServer));
                }
            });
        }


        private void UpdateCCUData()
        {
            var newData = monitoringService.GetCcuSeries(DateTime.Now.AddMinutes(-1), DateTime.Now);

            if (newData == null || newData.Count == 0) return;

            var groupedData = newData.GroupBy(c => c.ServerId);

            foreach (var group in groupedData)
            {
                if (!serverCcuData.TryGetValue(group.Key, out var values)) continue;

                int latestCcu = group.Last().CcuValue.Value;

                values.Add(latestCcu);
                if (values.Count > maxDataPoint) values.RemoveAt(0);
            }

            TimeLables.Add(DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
            if (TimeLables.Count > maxDataPoint) TimeLables.RemoveAt(0);
        }

        partial void OnRealTimeCheckedChanged(bool? value)
        {
            if (value.HasValue && value == true)
            {
                SetCcuChart(DateTime.Now.AddHours(-1), DateTime.Now);
                realTimeCcuTimer.Start();
            }
            else
            {
                realTimeCcuTimer.Stop();
            }
        }

        partial void OnSelectedServerChanged(long value)
        {
            if (serverCcuData.TryGetValue(value, out var values))
            {
                SelectedServerMinCcu = serverCcuData[value].Min();
                SelectedServerMaxCcu = serverCcuData[value].Max();
                SelectedServerLastCcu = serverCcuData[value].Last();
                SelectedServerLastTime = TimeLables?.LastOrDefault();

                foreach (LineSeries item in CcuSeries)
                {
                    item.Visibility = item.Name.Equals($"Server{value}") ? Visibility.Visible : Visibility.Collapsed;
                }
            }
            else
            {
                SelectedServerMinCcu = serverCcuData != null ? serverCcuData.Values.SelectMany(p => p).Max() : 0;
                SelectedServerMaxCcu = serverCcuData != null ? serverCcuData.Values.SelectMany(p => p).Max() : 0;
                SelectedServerLastCcu = 0;
                SelectedServerLastTime = TimeLables?.LastOrDefault();

                foreach (LineSeries item in CcuSeries)
                {
                    item.Visibility = Visibility.Visible;
                }
            }

        }

    }
}
