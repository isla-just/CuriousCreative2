namespace TheCuriousCreative;

public partial class Login : ContentPage
{
	public Login()
	{
		InitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("..");

    }
}
