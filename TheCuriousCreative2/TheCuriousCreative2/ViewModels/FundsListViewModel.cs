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

        private readonly IProjectService _projectService;
        private readonly IStaffService _staffService;
        private readonly IFundsService _fundsService;

        public FundsListViewModel(IFundsService fundsService, IProjectService projectService, IStaffService staffService)
        {
            _fundsService = fundsService;
            _projectService = projectService;
            _staffService = staffService;

            TotalExpenses = Salaries + Expenses;

            //GetFundsList();
            StoreFunds();
            CheckDays();
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
        private int _expenses = 0;

        //calculate

        [ObservableProperty]
        int totalExpenses;

        [ObservableProperty]
        int daysLeft = 0;

        [RelayCommand]
        public async void CheckDays()
        {
            //getting the current date and the last day of month
            DateTime now = DateTime.Now;
            var lastDayOfMonth = DateTime.DaysInMonth(now.Year, now.Month);

            int nowFormatted = (int)now.Day;

            DaysLeft = lastDayOfMonth - nowFormatted;

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
        public async void StoreFunds()
        {

            var projectList = await _projectService.GetProjectList();


            //to do: if deposit is paid add set amount to total
            foreach (var project in projectList)
            {
                //if active then add to funds
                if (project.Status == "active")
                {
                    Debug.WriteLine(ClientIncome + project.PricePerMonth);
                    ClientIncome = ClientIncome + project.PricePerMonth;
                }
            
            }

            var salaryList = await _staffService.GetStaffList();

            foreach (var staff in salaryList)
            {

                //multiphy your hourly rate by the amount of hours worked:

                if(staff.Role == "Admin")
                {
                    Salaries = Salaries +17000;
                }
                else
                {
                    Debug.WriteLine(Salaries + staff.Salary * staff.HoursWorked);
                    Salaries = Salaries + staff.Salary * staff.HoursWorked;
                }

            }

            //to do add printing expenses to this calculation
            FundsTotal = FundsTotal + ClientIncome - Salaries;

        }



        [RelayCommand]
        public async void AddFunds()
        {

            DateTime now = DateTime.Now;
            string nowFormatted = now.ToString();

            int response = -1;
            int projectResponse = -1;
            int hoursResponse = -1;

            response = await _fundsService.AddFunds(new Models.FundsModel
            {
                Date = nowFormatted,
                FundsTotal = FundsTotal,
                Salaries = Salaries,
                ClientIncome = ClientIncome,
                Expenses = 0
            });

            //to dp - update active projects to inactive
            //to do - zero hours worked

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Account closed", "Record Saved", "OK");
                FundsTotal = 0;
                Salaries = 0;
                ClientIncome = 0;
                Expenses = 0;
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }


        }

    }
}
