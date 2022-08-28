using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;
using Microsoft.Toolkit.Mvvm.Input;

namespace TheCuriousCreative2.ViewModels
{

    //using our model and services together to perform the action of adding or updating
    [QueryProperty(nameof(StaffDetail), "StaffDetail")]

    public partial class AddUpdateStaffViewModel : ObservableObject
    {
        //test
        public ObservableCollection<StaffModel> Staffs { get; set; } = new ObservableCollection<StaffModel>();

        [ObservableProperty]
        private StaffModel _staffDetail = new StaffModel();

        private readonly IStaffService _staffService;
        public AddUpdateStaffViewModel(IStaffService staffService)
        {
            _staffService = staffService;
        }


        [ObservableProperty]
        private string _staffName;

        //[ObservableProperty]
        //private string _image;


        [ObservableProperty]
        private string _staffPassword;

        [ObservableProperty]
        private string _role;

        [ObservableProperty]
        private string _nickName;

        [ObservableProperty]
        private string _designTeam;

        [ObservableProperty]
        private int _salary;

        [ObservableProperty]
        private int _hoursWorked;

        [ObservableProperty]
        private int _maxHours;

        [ObservableProperty]
        private string _birthday;

        [ObservableProperty]
        private string _currentProject;

        [ObservableProperty]
        string search;

        //adding Staff to the list
        [RelayCommand]
        public async void GetStaffList()
        {
            Staffs.Clear();
            var staffList = await _staffService.GetStaffList();
            if (staffList?.Count > 0)
            {
                foreach (var staff in staffList)
                {
                    Staffs.Add(staff);
                }
            }
        }


        //search functionlity to search for staff member name and ID
        [ICommand]
        public async void GetStaffListSearch()
        {
            var subjectList = await _staffService.GetStaffList();
            var filteredItems = subjectList.Where(value => value.StaffName.ToLowerInvariant().Contains(Search)).ToList();
            var filteredID = subjectList.Where(value => value.StaffID.ToString().Contains(Search)).ToList();

            Staffs.Clear();
            foreach (var staffName in filteredItems)
            {
                Staffs.Add(staffName);
            }


            Staffs.Clear();
            foreach (var staffID in filteredItems)
            {
                Staffs.Add(staffID);
            }
        }


        //search functionlity to search and filter according to staff member's role
        [ICommand]
        public async void GetStaffRoleFilter()
        {
            var staffList = await _staffService.GetStaffList();
            var staffRole = staffList.Where(value => value.Role.ToLowerInvariant().Contains(Search)).ToList();


            Staffs.Clear();
            foreach (var Role in staffRole)
            {
                Staffs.Add(Role);
            }

        }

        //search functionlity to search and filter according to staff member's design team
        [ICommand]
        public async void GetStaffTeamFilter()
        {
            var staffList = await _staffService.GetStaffList();
            var staffTeam = staffList.Where(value => value.DesignTeam.ToLowerInvariant().Contains(Search)).ToList();


            Staffs.Clear();
            foreach (var DesignTeam in staffTeam)
            {
                Staffs.Add(DesignTeam);
            }

        }


        //search functionlity to search and filter according to staff member's project
        [ICommand]
        public async void GetStaffProjectFilter()
        {
            var staffList = await _staffService.GetStaffList();
            var staffProject = staffList.Where(value => value.CurrentProject.ToLowerInvariant().Contains(Search)).ToList();


            Staffs.Clear();
            foreach (var CurrentProject in staffProject)
            {
                Staffs.Add(CurrentProject);
            }

        }



        //add display action to assign active state
        [ObservableProperty]
        StaffModel activeStaff = new StaffModel();

        //use this to set visibility - this is a toggle
        [ObservableProperty]
        bool isEditing = false;


        [RelayCommand]
        public async void UpdateStaff()
        {
            int response = -1;
            if (ActiveStaff.StaffID > 0)
            {
                response = await _staffService.UpdateStaff(ActiveStaff);
            }
            else
            {
                Debug.WriteLine("Id not found");
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Staff Info Saved", "Record Saved", "OK");
                GetStaffList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!!!!", "Something went wrong while editing record", "OK");
            }

        }

        [RelayCommand]
        public async void AddStaff()
        {
            int response = -1;
            if (StaffDetail.StaffID > 0)
            {
                Debug.WriteLine("this Staff already exists");
            }
            else
            {
                response = await _staffService.AddStaff(new Models.StaffModel
                {
                    StaffName = StaffDetail.StaffName,
                    StaffPassword = StaffDetail.StaffPassword,
                    Role = StaffDetail.Role,
                    Nickname = StaffDetail.Nickname,
                    DesignTeam = StaffDetail.DesignTeam,
                    Salary = StaffDetail.Role == "Admin" ? (15000) : StaffDetail.Role == "Lead Designer" ? (StaffDetail.MaxHours * 600) : (StaffDetail.MaxHours * 400),
                    HoursWorked = StaffDetail.HoursWorked,
                    MaxHours = StaffDetail.MaxHours,
                    Birthday = StaffDetail.Birthday,
                    CurrentProject = StaffDetail.CurrentProject

                });
            }

            if (response > 0)
            {
                await Shell.Current.DisplayAlert("Staff Info Saved", "Record Saved", "OK");
                GetStaffList();
            }
            else
            {
                await Shell.Current.DisplayAlert("Heads Up!", "Something went wrong while adding record", "OK");
            }

        }

        [RelayCommand]
        public async void DisplayAction(StaffModel staffModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option", "OK", null, "Edit", "Delete");
            if (response == "Edit")
            {

                ActiveStaff = staffModel;
                Debug.WriteLine(ActiveStaff);
                IsEditing = true;

            }
            else if (response == "Delete")
            {
                var delResponse = await _staffService.DeleteStaff(staffModel);
                if (delResponse > 0)
                {
                    GetStaffList();
                }
            }
        }
    }
}

