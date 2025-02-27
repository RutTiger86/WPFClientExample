using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Threading;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.Monitoring;
using WPFClientExample.Services;

namespace WPFClientExample.ViewModels
{
    public interface ICcuMonitoringViewModel
    {
        ObservableCollection<KeyValuePair<long, string>>? Servers { get; set; }
        long SelectedServer { get; set; }

        DateTime SelectedDate { get; set; }
        int SelectedHour { get; set; }
        int SelectedMinute { get; set; }
        int SelectedSecond { get; set; }
        bool? RealTimeChecked { get; set; }
        SeriesCollection? CcuSeries { get; set; }
        List<string>? TimeLables { get; set; }
        IAsyncRelayCommand SearchCommand { get; }

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
        SeriesCollection? ccuSeries;

        [ObservableProperty]
        List<string>? timeLables;

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
        private Dictionary<long, ChartValues<int>> serverCcuData; // 서버별 CCU 데이터 저장
        public CcuMonitoringViewModel(IMonitoringService monitoringService)
        {
            this.monitoringService = monitoringService;
            realTimeCcuTimer = new DispatcherTimer { Interval = TimeSpan.FromMinutes(1) };
            realTimeCcuTimer.Tick += async (s, e) => await UpdateCCUData();
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
            SelectedDate = DateTime.Now;
            SelectedHour = 0;
            SelectedMinute = 0;
            SelectedSecond = 0;
            CcuSeries?.Clear();
            RealTimeChecked = false;
            Servers.Clear();
            SelectedServer = 0;
            SelectedServerMinCcu = 0;
            SelectedServerMaxCcu = 0;
            SelectedServerLastCcu = 0;
            SelectedServerLastTime = string.Empty;
        }


        [RelayCommand]
        private async Task Search()
        {
            DateTime searchDate = new DateTime(SelectedDate.Year, SelectedDate.Month, SelectedDate.Day,
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
            CcuSeries = new SeriesCollection();  // 새로운 SeriesCollection 인스턴스 생성
            TimeLables = new List<string>();
            serverCcuData = new Dictionary<long, ChartValues<int>>();


            var serverList = await Task.Run(() => monitoringService.GetServers()
            ).ConfigureAwait(false);

            List<CcuInfo> ccuInfos = await Task.Run(() => monitoringService.GetCcuSeriesAsync(startDate, endDate)
            ).ConfigureAwait(false);

            if (ccuInfos.Count == 0)
            {
                return;
            }

            var ccuGroups = ccuInfos.GroupBy(p => p.ServerId);

            var baseGroup = ccuGroups.First();

            Application.Current.Dispatcher.Invoke(() =>
            {
                foreach (var data in baseGroup.OrderBy(p => p.CcuValue.Key))
                {
                    TimeLables.Add(data.CcuValue.Key.ToString("yy-MM-dd HH:mm:ss"));
                }

                foreach (var ccuGroup in ccuGroups)
                {
                    ChartValues<int> ccuValues = new ChartValues<int>(ccuGroup.Select(p => p.CcuValue.Value).ToList());
                    serverCcuData[ccuGroup.Key] = ccuValues;

                    LineSeries lineSeries = new LineSeries()
                    {
                        Name = $"Server{ccuGroup.Key}",
                        Title = $"Server ID : {ccuGroup.Key}",
                        Values = ccuValues,
                        PointGeometrySize = 10,
                        StrokeThickness = 2
                    };
                    CcuSeries.Add(lineSeries);
                }

                if (serverList.Count > 0)
                {
                    Servers.Clear();
                    Servers.Add(new KeyValuePair<long, string>(0, "ALL"));
                    foreach (var server in serverList)
                    {
                        Servers.Add(new KeyValuePair<long, string>(server.Id, server.ServerName));
                    }
                    SelectedServer = Servers.First().Key;
                    OnPropertyChanged(nameof(SelectedServer));
                }
            });
        }

        private async Task UpdateCCUData()
        {
            var newData = await monitoringService.GetCcuSeriesAsync(DateTime.Now.AddMinutes(-1), DateTime.Now);
            if (newData != null && newData.Count > 0)
            {
                var groupedData = newData.GroupBy(c => c.ServerId);

                foreach (var group in groupedData)
                {
                    long serverId = group.Key;
                    int latestCcu = group.Last().CcuValue.Value;

                    if (serverCcuData.ContainsKey(serverId))
                    {
                        serverCcuData[serverId].Add(latestCcu);
                        if (serverCcuData[serverId].Count > maxDataPoint)
                        {
                            serverCcuData[serverId].RemoveAt(0);
                        }
                    }
                }

                // 시간 라벨 추가 및 삭제
                TimeLables.Add(DateTime.Now.ToString("yy-MM-dd HH:mm:ss"));
                if (TimeLables.Count > maxDataPoint)
                {
                    TimeLables.RemoveAt(0);
                }
            }
        }

        partial void OnRealTimeCheckedChanged(bool? isChecked)
        {
            if (isChecked.HasValue && isChecked == true)
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
            if (serverCcuData.ContainsKey(value))
            {
                SelectedServerMinCcu = serverCcuData[value].Min();
                SelectedServerMaxCcu = serverCcuData[value].Max();
                SelectedServerLastCcu = serverCcuData[value].Last();
                SelectedServerLastTime = TimeLables?.Last();
                if (CcuSeries != null)
                {
                    foreach (LineSeries item in CcuSeries)
                    {
                        if (item.Name.Equals($"Server{value}"))
                        {
                            item.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            item.Visibility = Visibility.Collapsed;
                        }

                    }
                }
            }
            else
            {
                SelectedServerMinCcu = serverCcuData != null ? serverCcuData.Values.SelectMany(p => p).Max() : 0;
                SelectedServerMaxCcu = serverCcuData != null ? serverCcuData.Values.SelectMany(p => p).Max() : 0;
                SelectedServerLastCcu = 0;
                SelectedServerLastTime = TimeLables?.Last();

                if (CcuSeries != null)
                {
                    foreach (LineSeries item in CcuSeries)
                    {
                        item.Visibility = Visibility.Visible;
                    }
                }
            }

        }

    }
}
