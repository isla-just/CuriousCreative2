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

        [ObservableProperty]
        private ProjectModel _projectDetail = new ProjectModel();

        private readonly IProjectService _projectService;
        public AddUpdateProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [ObservableProperty]
        private string _projectName;

        //[ObservableProperty]
        //private string _image;

        [ObservableProperty]
        private string _client;

        [ObservableProperty]
        private string _status;

        [ObservableProperty]
        private string _designTeam;

        [ObservableProperty]
        private int _deposit;

        [ObservableProperty]
        private string _depositPaid;

        [ObservableProperty]
        private int _pricePerMonth;

        [ObservableProperty]
        private bool _priority;

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
        [RelayCommand]
        public async void ProjectSearchItems()
        {
            var projectList = await _projectService.GetProjectList();
            var searchedName = projectList.Where(value => value.ProjectName.ToLowerInvariant().Contains('b')).ToList();
            var searchedID = projectList.Where(value => value.ProjectID.ToString().Contains('0')).ToList();


            Projects.Clear();
            foreach (var project in searchedName)
            {
                Projects.Add(project);
            }
            foreach (var project in searchedID)
            {
                Projects.Add(project);
            }
        }

        //add display action to assign active state
        [ObservableProperty]
        ProjectModel activeProject = new ProjectModel();

        //use this to set visibility - this is a toggle
        [ObservableProperty]
        bool isEditing = false;


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
                await Shell.Current.DisplayAlert("Project Info Saved", "Record Saved", "OK");
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
                Debug.WriteLine("this Project already exists");
            }
            else
            {
                response = await _projectService.AddProject(new Models.ProjectModel
                {
                    ProjectName = ProjectDetail.ProjectName,
                    Client = ProjectDetail.Client,
                    Status = ProjectDetail.Status,
                    DesignTeam = ProjectDetail.DesignTeam,
                    Deposit = ProjectDetail.Deposit,
                    DepositPaid = ProjectDetail.DepositPaid,
                    PricePerMonth = ProjectDetail.PricePerMonth,
                    Priority = ProjectDetail.Priority
                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Project Info Saved", "Record Saved", "OK");
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
    }
}

