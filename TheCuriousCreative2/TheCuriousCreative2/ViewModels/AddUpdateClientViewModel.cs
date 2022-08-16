using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheCuriousCreative2.Services;
using TheCuriousCreative2.Models;

namespace TheCuriousCreative2.ViewModels
{
    //using our model and services together to perform the action of adding or updating
    [QueryProperty(nameof(ClientDetail), "ClientDetail")]

    public partial class AddUpdateClientViewModel : ObservableObject
    {
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
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }

        }
    }
}

