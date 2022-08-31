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


            GetFundsList();
            StoreFunds();
            CheckDays();
            GetProj();

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

        //this needs to be stored in preferences

        [ObservableProperty]
        private int _expenses;

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
        public ObservableCollection<FundChartModel> IncomeData { get; set; } = new ObservableCollection<FundChartModel>();
        public ObservableCollection<FundChartModel> SalaryData { get; set; } = new ObservableCollection<FundChartModel>();
        public ObservableCollection<FundChartModel> AdditionalData { get; set; } = new ObservableCollection<FundChartModel>();
        public ObservableCollection<FundChartModel> TotalData { get; set; } = new ObservableCollection<FundChartModel>();


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
                    IncomeData.Add(new FundChartModel { Amount = fund.ClientIncome, Month = fund.Date.TrimEnd() });
                    SalaryData.Add(new FundChartModel { Amount = fund.Salaries, Month = fund.Date.TrimEnd() });
                    AdditionalData.Add(new FundChartModel { Amount = fund.Expenses, Month = fund.Date.TrimEnd() });
                    TotalData.Add(new FundChartModel { Amount = fund.FundsTotal, Month = fund.Date.TrimEnd() });
                }
            }
        }

        [RelayCommand]
        public async void StoreFunds()
        {

            var projectList = await _projectService.GetProjectList();

            foreach (var project in projectList)
            {

                //if active then add to funds
                if (project.Status == true)
                {
                    //if deposit is paid 
                    if (project.DepositPaid == true)
                    {
                        ClientIncome = ClientIncome + project.Deposit;
                    }

                    Debug.WriteLine(ClientIncome + project.PricePerMonth);
                    ClientIncome = ClientIncome + project.PricePerMonth;
                }
            
            }

            var salaryList = await _staffService.GetStaffList();

            foreach (var staff in salaryList)
            {

                //multiphy your hourly rate by the amount of hours worked:

                 //jeandre already did the math on her side
                    Salaries = Salaries + staff.Salary;

            }

            //getting from preferences and setting the variable to stored value
            Expenses = Preferences.Get("Expenses", Expenses);

            TotalExpenses = Salaries + Expenses;

            FundsTotal = FundsTotal + ClientIncome - Salaries - Expenses;

        }

        //test
        public ObservableCollection<ProjectModel> ActiveProjects { get; set; } = new ObservableCollection<ProjectModel>();


        //close off month
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
                Expenses = Expenses
            });
       
            ////to do - zero hours worked for new month
            //  int search = await App.StaffService.ZeroHours(UserName);

            //if (search)
            //{
            //    Debug.WriteLine("User Was Found");
            //    ErrorDisplay = "should navigate";
            //    Preferences.Set("StayLoggedOn", stayLoggedOn);
            //    await Shell.Current.GoToAsync("/Dashboard");
            //}
            //else
            //{
            //    Debug.WriteLine("User Not Found");
            //    ErrorDisplay = "Invalid username or password";

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Account closed", "Record Saved", "OK");
                FundsTotal = 0;
                Salaries = 0;
                ClientIncome = 0;
                Expenses = 0;
                TotalExpenses = 0;
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }


        }


        [ObservableProperty]
        public int printingExpenses = 0;


        //test
        public ObservableCollection<ProjectModel> Projects { get; set; } = new ObservableCollection<ProjectModel>();

       


        [RelayCommand]
        public async void GetProj() {

            var projList = await _projectService.GetProjectList();
            if (projList?.Count > 0)
            {
                foreach (var proj in projList)
                {
                    Projects.Add(proj);

                    //appending active projects to an object
                    if (proj.Status == true)
                    {
                        ActiveProjects.Add(proj);
                    }
                }
            }
        }


        //test
        public ObservableCollection<StaffModel> HourLog { get; set; } = new ObservableCollection<StaffModel>();


        //[RelayCommand]
        //public async void GetHours()
        //{

        //    var hourList = await _staffService.GetStaffList();
        //    if (hourList?.Count > 0)
        //    {
        //        foreach (var hour in hourList)
        //        {
        //            //appending active projects to an object
        //            if (hour.HoursWorked != 0)
        //            {
        //                HourLog.Add(proj);
        //            }
        //        }
        //    }
        //}


        [RelayCommand]
        public async void AddPrinting()
        {
            Expenses = Expenses + PrintingExpenses;
            Preferences.Set("Expenses", Expenses);

            await Shell.Current.DisplayAlert("This project has been added to the printing que", "Project name", "OK");
        }


    }
}
