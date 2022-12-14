using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Input;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;

namespace TheCuriousCreative2.ViewModels
{

    //using our model and services together to perform the action of adding or updating
    [QueryProperty(nameof(ProjectDetail), "ProjectDetail")]

    public partial class AddUpdateProjectViewModel : ObservableObject
    {
        //test
        public ObservableCollection<ProjectModel> Projects { get; set; } = new ObservableCollection<ProjectModel>();

        public ObservableCollection<ProjectModel> ActiveProj { get; set; } = new ObservableCollection<ProjectModel>();

        [ObservableProperty]
        private ProjectModel _projectDetail = new ProjectModel();

        private readonly IProjectService _projectService;
        public AddUpdateProjectViewModel(IProjectService projectService)
        {
            listOfClients = new List<ClientModel>();
            GetListOfClients();
            _projectService = projectService;
        }

        [ObservableProperty]
        List<ClientModel> listOfClients;

        [ObservableProperty]
        ClientModel selectedClient;

        [ObservableProperty]
        private string _projectName;

        [ObservableProperty]
        private string _projectImage;

        [ObservableProperty]
        private string _client;

        [ObservableProperty]
        private bool _status;

        [ObservableProperty]
        private string _designTeam;

        [ObservableProperty]
        private int _deposit;

        [ObservableProperty]
        private string _depositPaid;

        [ObservableProperty]
        private int _pricePerMonth;

        [ObservableProperty]
        private int _maxEmployeeHours;

        [ObservableProperty]
        private int _maxClientHours;

        [ObservableProperty]
        private bool _priority;

        [ObservableProperty]
        string search;


        //adding projects to the list
        [RelayCommand]
        public async void GetProjectList()
        {
            Projects.Clear();
            var projectList = await _projectService.GetProjectList();
            if (projectList?.Count > 0)
            {
                foreach (var project in projectList)
                {
                    Projects.Add(project);
                }
            }
        }

        //search functionlity to search for project name and ID
        [ICommand]
        public async void GetProjectListSearch()
        {
            var projectList = await _projectService.GetProjectList();
            var filteredItems = projectList.Where(value => value.ProjectName.Contains(Search)).ToList();
            var filteredID = projectList.Where(value => value.ProjectID.ToString().Contains(Search)).ToList();

            Projects.Clear();
            foreach (var projectName in filteredItems)
            {
                Projects.Add(projectName);
            }


            Projects.Clear();
            foreach (var projectID in filteredItems)
            {
                Projects.Add(projectID);
            }
        }

        //search functionlity to search and filter according to project's client
        [ICommand]
        public async void GetProjectClientFilter()
        {
            var projectList = await _projectService.GetProjectList();
            var projectClient = projectList.Where(value => value.Client.Contains(Search)).ToList();


            Projects.Clear();
            foreach (var Client in projectClient)
            {
                Projects.Add(Client);
            }

        }

        //search functionlity to search and filter according to project's design team
        [ICommand]
        public async void GetProjectTeamFilter()
        {
            var projectList = await _projectService.GetProjectList();
            var designTeam = projectList.Where(value => value.DesignTeam.Contains(Search)).ToList();


            Projects.Clear();
            foreach (var DesignTeam in designTeam)
            {
                Projects.Add(DesignTeam);
            }

        }

        //add display action to assign active state
        [ObservableProperty]
        ProjectModel activeProject = new ProjectModel();

        //use this to set visibility - this is a toggle
        [ObservableProperty]
        bool isEditing = false;

        [ObservableProperty]
        bool isTeams = false;

        public async void GetListOfClients()
        {
            ListOfClients = await App.ClientService.GetClientList();
        }


        [RelayCommand]
        public async void UpdateProject()
        {
            int response = -1;
            if (ActiveProject.ProjectID > 0)
            {
                response = await _projectService.UpdateProject(ActiveProject);
            }
            else
            {
                Debug.WriteLine("Id not found");
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Project Info has been updated", "Successfully saved", "OK");
                GetProjectList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!!!!", "Something went wrong while editing record", "OK");
            }

        }

        [RelayCommand]
        public async void AddProject()
        {
            int response = -1;
            if (ProjectDetail.ProjectID > 0)
            {
                Debug.WriteLine("this project already exists");
            }
            else
            {
                response = await _projectService.AddProject(new Models.ProjectModel
                {
                    ProjectName = ProjectDetail.ProjectName,
                    ProjectImage = ProjectDetail.ProjectImage,
                    //ProjectImage = ProjectDetail.ProjectImage == "Bunny Friends" ? "illustration_bunny_friend.jpg" : ProjectDetail.ProjectImage == "Bunnies" ? "illustration_bunny.jpg" : ProjectDetail.ProjectImage == "Mouse House" ? "mouse_house.jpg" : ProjectDetail.ProjectImage == "Kitty Kitty" ? "kitty_kitty.jpg" : ProjectDetail.ProjectImage == "Miranda's House" ? "mirandas_house.jpg" : ProjectDetail.ProjectImage == "The House" ? "illustration_house.jpg" : ProjectDetail.ProjectImage == "Flower Town" ? "flower_town.jpg" : ProjectDetail.ProjectImage == "Yeti" ? "illustration_yeti.jpg" : ProjectDetail.ProjectImage == "Build A Home" ? "illustration_house_white.jpg" : "kitty_kitty.jpg",
                    Client = ProjectDetail.Client,
                    Status = ProjectDetail.Status,
                    DesignTeam = ProjectDetail.DesignTeam,
                    Deposit = ProjectDetail.Deposit,
                    DepositPaid = ProjectDetail.DepositPaid,
                    PricePerMonth = ProjectDetail.PricePerMonth,
                    MaxClientHours = ProjectDetail.MaxClientHours,
                    MaxEmployeeHours = ProjectDetail.MaxEmployeeHours
                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Project has been added", "Project info successfully saved", "OK");
                GetProjectList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }

        }

        [RelayCommand]
        public async void DisplayAction(ProjectModel projectModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {

                ActiveProject = projectModel;
                Debug.WriteLine(ActiveProject);
                IsEditing = true;
                IsTeams = false;

            }
            else if (response == "Delete")
            {
                var delResponse = await _projectService.DeleteProject(projectModel);
                if (delResponse > 0)
                {
                    GetProjectList();
                }
            }
        }

        //See Teams
        [RelayCommand]
        public async void BackToTeams()
        {
            IsTeams = true;
            IsEditing = false;
        }

        //Back To Add new project
        [RelayCommand]
        public async void BackToAdd()
        {
            IsEditing = false;
            IsTeams = false;
        }
    }
}
