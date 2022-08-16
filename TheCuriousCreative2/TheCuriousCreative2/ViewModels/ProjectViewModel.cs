using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;

namespace TheCuriousCreative2.ViewModels
{
    public partial class ProjectViewModel : ObservableObject
    {
        public ObservableCollection<ProjectModel> Projects { get; set; } = new ObservableCollection<ProjectModel>();

        private readonly IProjectService _projectService;
        public ProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }



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


        [RelayCommand]
        public async void AddUpdateProject()
        {
            await AppShell.Current.GoToAsync(nameof(AddUpdateProject));
        }


        [RelayCommand]
        public async void DisplayAction(ProjectModel projectModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("ProjectDetail", projectModel);
                await AppShell.Current.GoToAsync(nameof(AddUpdateProject), navParam);
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