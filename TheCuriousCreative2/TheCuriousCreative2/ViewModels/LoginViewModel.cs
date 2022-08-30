using System;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TheCuriousCreative2.ViewModels
{
    public partial class LoginViewModel : ObservableObject
    {
        [ObservableProperty]
        string userName;

        [ObservableProperty]
        string password;

        [ObservableProperty]
        string errorDisplay;

        [ObservableProperty]
        bool stayLoggedOn = false;

        [RelayCommand]
        public async void GoToDashboard()
        {
            bool isLoggedOn = Preferences.Get("StayLoggedOn", false);
            if (isLoggedOn)
            {
                
                await Shell.Current.GoToAsync("/Dashboard");
            }
            else
            {
               
            }
        }


        [RelayCommand]
        public async Task LoginVerification()
        {

            bool search = await App.StaffService.AdminStaffLoginAuth(UserName, password);

            if (search)
            {
                Debug.WriteLine("User Was Found");
                ErrorDisplay = "should navigate";
                Preferences.Set("StayLoggedOn", stayLoggedOn);
                await Shell.Current.GoToAsync("/Dashboard");
            }
            else
            {
                Debug.WriteLine("User Not Found");
                ErrorDisplay = "Invalid username or password";
            }
        }

    }
}
// User
// Sally Williams
// SallyCreates