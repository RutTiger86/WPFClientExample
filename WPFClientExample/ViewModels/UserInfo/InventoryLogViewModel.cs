﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFClientExample.Commons.Messages.UserInfo;
using WPFClientExample.Commons.Messages;
using WPFClientExample.Models.GameLog;
using WPFClientExample.Services;
using System.Net.NetworkInformation;
using System.Windows;

namespace WPFClientExample.ViewModels.UserInfo
{
    public interface IInventoryLogViewModel
    {
        DateTime SearchStartDate { get; set; }
        DateTime SearchEndDate { get; set; }
        List<InventoryHistoryLogInfo>? TargetInventoryHistory { get; set; }
        IAsyncRelayCommand SearchCommand { get; }

    }
    public partial class InventoryLogViewModel:ObservableObject, IInventoryLogViewModel,IRecipient<LoginMessage>, IRecipient<SelectedCharacterMessage>
    {
        private readonly IGameLogService gameLogService;

        [ObservableProperty]
        DateTime searchStartDate;

        [ObservableProperty]
        DateTime searchEndDate;

        [ObservableProperty]
        List<InventoryHistoryLogInfo>? targetInventoryHistory;

        private CharacterInfo? selectedCharacterInfo; 

        public InventoryLogViewModel(IGameLogService gameLogService)
        {
            this.gameLogService = gameLogService;
            SettingMessage();
        }

        private void SettingMessage()
        {
            WeakReferenceMessenger.Default.Register<SelectedCharacterMessage>(this);
            WeakReferenceMessenger.Default.Register<LoginMessage>(this);
        }

        public void Receive(LoginMessage message)
        {
            InitData();
        }


        public void Receive(SelectedCharacterMessage message)
        {
            InitData();
            selectedCharacterInfo = message.Value;
        }

        public void InitData()
        {
            SearchStartDate = DateTime.Now.AddDays(-1);
            SearchEndDate = DateTime.Now.AddDays(1); ;
            TargetInventoryHistory = null;
            selectedCharacterInfo = null;
        }

        [RelayCommand]
        private async Task Search()
        {
            try
            {
                if (selectedCharacterInfo != null)
                {
                    TargetInventoryHistory = await gameLogService.GetInventoryHistoryLogAsync(selectedCharacterInfo.CharacterId, SearchStartDate, SearchEndDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
