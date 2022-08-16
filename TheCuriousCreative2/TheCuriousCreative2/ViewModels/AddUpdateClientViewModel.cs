﻿using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheCuriousCreative2.Services;
using TheCuriousCreative2.Models;
using System.Collections.ObjectModel;

namespace TheCuriousCreative2.ViewModels
{
    //using our model and services together to perform the action of adding or updating
    [QueryProperty(nameof(ClientDetail), "ClientDetail")]

    public partial class AddUpdateClientViewModel : ObservableObject
    {
        //test
        public ObservableCollection<ClientModel> Clients { get; set; } = new ObservableCollection<ClientModel>();

        [ObservableProperty]
        private ClientModel _clientDetail = new ClientModel();

        private readonly IClientService _clientService;
        public AddUpdateClientViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        [ObservableProperty]
        private string _clientName;

        [ObservableProperty]
        private string _clientNotes;

        [ObservableProperty]
        private bool _priority;

        //adding clients to the list
        [RelayCommand]
        public async void GetClientList()
        {
            Clients.Clear();
            var clientList = await _clientService.GetClientList();
            if (clientList?.Count > 0)
            {
                foreach (var client in clientList)
                {
                    Clients.Add(client);
                }
            }
        }

        //add display action to assign active state


        [RelayCommand]
        public async void AddUpdateClient()
        {
            int response = -1;
            if (ClientDetail.ClientID > 0)
            {
                response = await _clientService.UpdateClient(ClientDetail);
            }
            else
            {
                response = await _clientService.AddClient(new Models.ClientModel
                {
                    ClientName = ClientDetail.ClientName,
                    ClientNotes = ClientDetail.ClientNotes,
                    Priority = ClientDetail.Priority,
                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Client Info Saved", "Record Saved", "OK");
                GetClientList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }

        }

        [RelayCommand]
        public async void DisplayAction(ClientModel clientModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("ClientDetail", clientModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateClient), navParam);
            }
            else if (response == "Delete")
            {
                var delResponse = await _clientService.DeleteClient(clientModel);
                if (delResponse > 0)
                {
                    GetClientList();
                }
            }
        }
    }
}
