using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class Login : ContentPage
{
    private LoginViewModel _viewModel;
    public Login(LoginViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        this.BindingContext = viewModel;
    }




    protected override void OnAppearing()
    {
        //run getsubjects when page loads
        base.OnAppearing();
        _viewModel.LoginVerificationCommand.Execute(null);
    }



    // Navigate to Dashboard
    //private async void Navigation_Clicked(object sender, EventArgs e)
    //{

    //    await Shell.Current.GoToAsync("Dashboard");

    //}
}
