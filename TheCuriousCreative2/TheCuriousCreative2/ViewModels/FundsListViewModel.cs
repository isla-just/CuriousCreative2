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
    //[QueryProperty(nameof(FundsDetail), "FundsDetail")]

    public partial class FundsListViewModel : ObservableObject
    {

        private readonly IProjectService _projectService;
        private readonly IStaffService _staffService;
        private readonly IFundsService _fundsService;

        public FundsListViewModel(IFundsService fundsService, IProjectService projectService, IStaffService staffService)
        {
            _fundsService = fundsService;
            _projectService = projectService;
            _staffService = staffService;

            TotalExpenses = Salaries + Expenses;

            GetFundsList();
        }

        //using our model and services together to perform the action of deleting, showing and editing

        public ObservableCollection<FundsModel> Funds { get; set; } = new ObservableCollection<FundsModel>();

        [ObservableProperty]
        private FundsModel _fundsDetail = new FundsModel();

        //[ObservableProperty]
        //List<Funds> listOfFunds;

        [ObservableProperty]
        private int _fundsTotal;

        [ObservableProperty]
        private int _salaries;

        [ObservableProperty]
        private int _clientIncome;

        [ObservableProperty]
        private int _expenses;

        //calculate

        [ObservableProperty]
        int totalExpenses;

        //for printing list dropdown

        //[ObservableProperty]
        //List<ProjectModel> listOfProjects;

        //[ObservableProperty]
        //ProjectModel selectedProject;

        [ObservableProperty]
        ProjectModel printingCosts;


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

        [ObservableProperty]
        FundsModel _fundsUpdate = new FundsModel();

        [RelayCommand]
        public async void AddPrinting()
        {

            int response = -1;

            if (FundsUpdate.FundsId > 0)
            {
                Debug.WriteLine("double entry");
            }
            else
            {
                response = await _fundsService.UpdateFunds(FundsDetail);
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


        [RelayCommand]
        public async void AddFunds()
        {

            var projectList = await _projectService.GetProjectList();


            //to do: if deposit is paid add set amount to total
            foreach(var project in projectList)
            {
                Debug.WriteLine(ClientIncome + project.PricePerMonth);
                ClientIncome = ClientIncome + project.PricePerMonth;
            }

            var salaryList = await _staffService.GetStaffList();

            foreach (var staff in salaryList)
            {

                //multiphy your hourly rate by the amount of hours worked:
                //to do - if user is admin then add fixed salary not hourly rate


                Debug.WriteLine(Salaries + staff.Salary*staff.HoursWorked);
                Salaries = Salaries + staff.Salary * staff.HoursWorked;
            }

            //to do add printing expenses to this calculation
            FundsTotal = FundsTotal + ClientIncome - Salaries;

            Preferences.Set("MonthlyFunds", FundsTotal);
            Preferences.Set("FundsSalary", Salaries);
            Preferences.Set("FundsClients", ClientIncome);

            //this isnt working

            int response = -1;

            response = await _fundsService.AddFunds(new Models.FundsModel
            {
                FundsTotal = FundsTotal,
                Salaries = Salaries,
                ClientIncome = ClientIncome,
                Expenses = 0
            });

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

