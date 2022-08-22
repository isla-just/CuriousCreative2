using System;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using TheCuriousCreative2.Models;
using TheCuriousCreative2.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Diagnostics;

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
        [RelayCommand]
        public async void StaffSearchItems()
        {
            var projectList = await _staffService.GetStaffList();
            var searchedName = projectList.Where(value => value.StaffName.ToLowerInvariant().Contains('s')).ToList();
            var searchedID = projectList.Where(value => value.StaffID.ToString().Contains('0')).ToList();


            Staffs.Clear();
            foreach (var staff in searchedName)
            {
                Staffs.Add(staff);
            }
            foreach (var staff in searchedID)
            {
                Staffs.Add(staff);
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
                    Salary = StaffDetail.Salary,
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

