namespace TheCuriousCreative2;

public partial class FlyoutFooter : ContentView
{
	public FlyoutFooter()
	{
		InitializeComponent();
	}

    private async void Back_Clicked(object sender, EventArgs e)
    {

        await Shell.Current.GoToAsync("Login");

    }
}
