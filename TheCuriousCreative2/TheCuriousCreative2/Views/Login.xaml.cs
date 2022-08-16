namespace TheCuriousCreative2;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void Navigation_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("Dashboard");

    }
}
