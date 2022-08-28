using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class Login : ContentPage
{
    public Login(LoginViewModel viewModel)
    {
        InitializeComponent();
        this.BindingContext = viewModel;
    }




    ////Navigate to Dashboard
    //private async void Navigation_Clicked(object sender, EventArgs e)
    //{
    //    await Shell.Current.GoToAsync("Dashboard");

    //}
}
