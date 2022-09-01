using System;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheCuriousCreative2.Services;
using TheCuriousCreative2.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace TheCuriousCreative2.ViewModels
{
	public partial class DashboardViewModel : ObservableObject
    {
        private readonly IProjectService _projectService;
        private readonly IStaffService _staffService;
        private readonly IClientService _clientService;
        private readonly IFundsService _fundsService;


        public DashboardViewModel(IFundsService fundsService, IProjectService projectService, IClientService clientService, IStaffService staffService)
    {
        _fundsService = fundsService;
        _projectService = projectService;
        _staffService = staffService;
        _clientService = clientService;

            GetAdmin();
            GetCounters();
            GetFundsList();
            GetTeamsList();
            GetProjects();

        }




        //adding clients to the list
        [RelayCommand]
        public async void GetFundsList()
        {
            FundData.Clear();
            var fundList = await _fundsService.GetFundsList();
            if (fundList?.Count > 0)
            {
                foreach (var fund in fundList)
                {

                    FundData.Add(new FundChartModel { Amount = fund.FundsTotal, Month = fund.Date.TrimEnd()});

                }

            }
        }


        public ObservableCollection<FundChartModel> FundData { get; set; } = new ObservableCollection<FundChartModel>();
        //public List<FundChartModel> FundData { get; set; }

                public ObservableCollection<ProjectChart> ProjectData { get; set; } = new ObservableCollection<ProjectChart>();

        //fig this at a later stage
        [ObservableProperty]
        int projectCounter = 0;

        [ObservableProperty]
        int clientCounter = 0;

        [ObservableProperty]
        string userName;

        [RelayCommand]
        public async void GetCounters()
        {
            var projectList = await _projectService.GetProjectList();


            ProjectCounter = projectList.Count();


            var clientList = await _clientService.GetClientList();

            ClientCounter = clientList.Count();

        }

        [RelayCommand]
        public async void GetProjects()
        {
            var projectList = await _projectService.GetProjectList();


            foreach (var project in projectList)
            {
                ProjectData.Add(new ProjectChart { Amount = project.PricePerMonth, Project = project.ProjectName });

            }

        }

        //add display action to assign active state
        [ObservableProperty]
        StaffModel currentAdmin = new StaffModel();

        [RelayCommand]
        public async void GetAdmin()
        {
            var adminList = await _staffService.GetStaffList();

            foreach(var admin in adminList)
            {

                var checking = Preferences.Get("StaffName", UserName);

                if (admin.StaffName == checking)
                {
                    CurrentAdmin = admin;
                }
                else
                {
                    Debug.WriteLine("user not found");
                }
            }


        }

        //dream team
        [ObservableProperty]
        int teamCounter1 = 0;

        //baby creatives
        [ObservableProperty]
        int teamCounter2 = 0;

        //Colour Connoisseurs
        [ObservableProperty]
        int teamCounter3 = 0;

        //Sketch Worx
        [ObservableProperty]
        int teamCounter4 = 0;

        //Stay Sketchy
        [ObservableProperty]
        int teamCounter5 = 0;

        //Cre8
        [ObservableProperty]
        int teamCounter6 = 0;

        //Magic Makers
        [ObservableProperty]
        int teamCounter7 = 0;

        //Magic Makers
        [ObservableProperty]
        int teamCounter8 = 0;


        //Admin peeps
        [ObservableProperty]
        int teamCounter9 = 0;


        [RelayCommand]
        public async void GetTeamsList()
        {
            var teamList = await _staffService.GetStaffList();

            foreach (var team in teamList)
            {

                if (team.DesignTeam == "The Dream Team")
                {
                    TeamCounter1 = TeamCounter1 + 1;
                }
                else if (team.DesignTeam == "Baby Creatives")
                {
                    TeamCounter2 = TeamCounter2 + 1;
                }
                else if (team.DesignTeam == "Colour Connoisseurs")
                {
                    TeamCounter3++;
                }
                else if (team.DesignTeam == "Sketch Worx")
                {
                    TeamCounter4++;
                }
                else if (team.DesignTeam == "Stay Sketchy")
                {
                    
                        TeamCounter5++;
                }
                else if (team.DesignTeam == "Cre8")
                {
                    TeamCounter6++;
                }
                else if (team.DesignTeam == "Magic Makers")
                {
                    TeamCounter7++;
                }
                //else if (team.Role == "Admin")
                //{
                //    TeamCounter9++;
                //}
                else
                {
                    TeamCounter8++;
                }

             

            }

        }

        [RelayCommand]
        public async void NavigateNext() {
            await Shell.Current.GoToAsync("/Projects");
        }

        [ObservableProperty]
        bool feat1 = true;

        [ObservableProperty]
        bool feat2 = false;

        [ObservableProperty]
        bool feat3 = false;

        [ObservableProperty]
        bool feat4 = false;


        //simple slider
        [RelayCommand]
        public async void Featured1()
        {
            Feat1 = false;
            Feat2 = true;


        }
        [RelayCommand]
        public async void Featured2()
        {
            Feat2 = false;
            Feat3 = true;

 
        }
        [RelayCommand]
        public async void Featured3()
        {
            Feat3 = false;
            Feat4 = true;


        }
        [RelayCommand]
        public async void Featured4()
        {
            Feat4 = false;
            Feat1 = true;


        }

    }
}

