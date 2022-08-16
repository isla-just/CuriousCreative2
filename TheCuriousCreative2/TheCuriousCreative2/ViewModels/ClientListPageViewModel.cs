using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Input;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;

namespace TheCuriousCreative2.ViewModels
{
	public partial class ClientListPageViewModel : ObservableObject
    {
        //using our model and services together to perform the action of deleting, showing and editing
        public ObservableCollection<ClientModel> Clients { get; set; } = new ObservableCollection<ClientModel>();

        private readonly IClientService _clientService;
        public ClientListPageViewModel(IClientService clientService)
        {
            _clientService = clientService;
        }

        //adding students to the list
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

        [RelayCommand]
        public async void AddUpdateClient()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateClient));
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

