using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheCuriousCreative2.Services;
using TheCuriousCreative2.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TheCuriousCreative2.ViewModels
{
    //using our model and services together to perform the action of adding or updating
    [QueryProperty(nameof(FundsDetail), "FundsDetail")]

    public partial class FundsListViewModel : ObservableObject
    {
        //using our model and services together to perform the action of deleting, showing and editing

        public ObservableCollection<FundsModel> Funds { get; set; } = new ObservableCollection<FundsModel>();

        [ObservableProperty]
        private FundsModel _fundsDetail = new FundsModel();

        private readonly IFundsService _fundsService;
        public FundsListViewModel(IFundsService fundsService)
        {
            _fundsService = fundsService;
            GetFundsList();


            //int expense1 = Int32.Parse(Expenses);
            //int expense2 = Int32.Parse(Salaries);

            //totalExpenses = expense1 + expense2;

            totalExpenses = 20;
        }

        //[ObservableProperty]
        //List<Funds> listOfFunds;

        [ObservableProperty]
        private string _fundsTotal;

        [ObservableProperty]
        private string _salaries;

        [ObservableProperty]
        private string _clientIncome;

        [ObservableProperty]
        private string _expenses;


        //calculate

        [ObservableProperty]
        int totalExpenses = 10;

        //for printing list dropdown

        //[ObservableProperty]
        //List<ProjectModel> listOfProjects;

        //[ObservableProperty]
        //ProjectModel selectedProject;

        [ObservableProperty]
        ProjectModel printingCosts;

        [RelayCommand]
        public async void AddPrinting()
        {
            int response = -1;

            if (FundsDetail.FundsId > 0)
            {
                Debug.WriteLine("double entry");
            }
            else
            {
                response = await _fundsService.UpdateFunds(new Models.FundsModel
                {
                    FundsTotal = FundsDetail.FundsTotal,
                    Salaries = FundsDetail.Salaries,
                    ClientIncome = FundsDetail.ClientIncome,
                    Expenses = FundsDetail.Expenses,
                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Client Info Saved", "Record Saved", "OK");
                GetFundsList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }
        }



        //adding clients to the list
        [RelayCommand]
        public async void GetFundsList()
        {
            Funds.Clear();
            var fundList = await _fundsService.GetFundsList();
            if (fundList?.Count > 0)
            {
                foreach (var fund in fundList)
                {
                    Funds.Add(fund);
                }
            }
        }


        [RelayCommand]
        public async void AddFunds()
        {
            int response = -1;

            if (FundsDetail.FundsId > 0)
            {
                Debug.WriteLine("double entry");
            }
            else
            {
                response = await _fundsService.AddFunds(new Models.FundsModel
                {
                    FundsTotal = FundsDetail.FundsTotal,
                    Salaries = FundsDetail.Salaries,
                    ClientIncome = FundsDetail.ClientIncome,
                    Expenses = FundsDetail.Expenses,
                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Client Info Saved", "Record Saved", "OK");
                GetFundsList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }


        }



    }
}

