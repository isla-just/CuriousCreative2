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

            GetCounters();

        }

        //fig this at a later stage
        [ObservableProperty]
        int projectCounter = 2;

        [ObservableProperty]
        int clientCounter = 6;

        [RelayCommand]
        public async void GetCounters()
        {
            var projectList = await _projectService.GetProjectList();


            projectCounter = projectList.Count();

            Debug.WriteLine(projectCounter);

            var clientList = await _clientService.GetClientList();

            foreach (var client in clientList)
            {
                clientCounter++;
            }

        }
    }
}

