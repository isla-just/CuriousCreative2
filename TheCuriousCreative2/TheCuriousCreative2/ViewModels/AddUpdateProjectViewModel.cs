using CommunityToolkit.Mvvm.ComponentModel;
//using Microsoft.Toolkit.Mvvm.ComponentModel;
//using Microsoft.Toolkit.Mvvm.Input;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace TheCuriousCreative2.ViewModels
{
    [QueryProperty(nameof(ProjectDetail), "ProjectDetail")]
    public partial class AddUpdateProjectViewModel : ObservableObject
    {
        [ObservableProperty]
        private ProjectModel _projectDetail = new ProjectModel();

        private readonly IProjectService _projectService;
        public AddUpdateProjectViewModel(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [RelayCommand]
        public async void AddUpdateProject()
        {
            int response = -1;
            if (ProjectDetail.ProjectID > 0)
            {
                response = await _projectService.EditProject(ProjectDetail);
            }
            else
            {
                response = await _projectService.AddProject(new Models.ProjectModel
                {
                    ProjectName = ProjectDetail.ProjectName,
                    Image = ProjectDetail.Image,
                    Status = ProjectDetail.Status,
                    Client = ProjectDetail.Client,
                    Deposit = ProjectDetail.Deposit,
                    DepositPaid = ProjectDetail.DepositPaid,
                    PricePerMonth = ProjectDetail.PricePerMonth,
                    TeamID = ProjectDetail.TeamID,
                    Priority = ProjectDetail.Priority,
                });
            }



            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Project Info Saved", "Record Saved", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }
        }

    }
}

