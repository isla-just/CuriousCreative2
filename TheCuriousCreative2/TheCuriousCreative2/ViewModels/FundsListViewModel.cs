using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Input;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;

namespace TheCuriousCreative2.ViewModels
{
	public partial class FundsListViewModel : ObservableObject
    {
        //using our model and services together to perform the action of deleting, showing and editing
        public ObservableCollection<FundsModel> Funds { get; set; } = new ObservableCollection<FundsModel>();

        private readonly IFundsService _fundsService;
        public FundsListViewModel(IFundsService fundsService)
        {
            _fundsService = fundsService;
        }

        //adding students to the list
        [RelayCommand]
        public async void GetFundsList()
        {
            Funds.Clear();
            var fundsList = await _fundsService.GetFundsList();
            if (fundsList?.Count > 0)
            {
                foreach (var fund in fundsList)
                {
                    Funds.Add(fund);
                }
            }
        }

        [RelayCommand]
        public async void AddUpdateFunds()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateFunds));
        }

        [RelayCommand]
        public async void DisplayAction(FundsModel fundsModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("Funds", fundsModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateFunds), navParam);
            }
            else if (response == "Delete")
            {
                var delResponse = await _fundsService.DeleteFunds(fundsModel);
                if (delResponse > 0)
                {
                    GetFundsList();
                }
            }
        }

    }
}

