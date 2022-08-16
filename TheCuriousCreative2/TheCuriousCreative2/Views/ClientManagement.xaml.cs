using TheCuriousCreative2.ViewModels;
using TheCuriousCreative2.Controls;

namespace TheCuriousCreative2;

public partial class ClientManagement : ContentPage
{
	public ClientManagement(AddUpdateClientViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }

    private async void TapGestureRecognizer_Tapped_NavigateToPoupPage1(object sender, EventArgs e)
    {
        await App.Current.MainPage.Navigation.PushModalAsync(new BasePopupPage());
    }

    //private async void TapGestureRecognizer_Tapped_NavigateToNormalPage(object sender, EventArgs e)
    //{
    //    await App.Current.MainPage.Navigation.PopModalAsync();
    //    await App.Current.MainPage.Navigation.PushAsync(new NewPage1());
    //}
}
