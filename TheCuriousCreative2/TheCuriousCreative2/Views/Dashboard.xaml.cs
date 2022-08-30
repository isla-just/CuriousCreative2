using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class Dashboard : ContentPage
{
    private DashboardViewModel _viewMode;
    public Dashboard(DashboardViewModel viewModel)
	{
		InitializeComponent();

        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    //onchange when student table is changed
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetCountersCommand.Execute(null);
    }

    private async void Navigation_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ProjectManagement");
    }

    private async void FundsNav_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("Funds");
    }
}
