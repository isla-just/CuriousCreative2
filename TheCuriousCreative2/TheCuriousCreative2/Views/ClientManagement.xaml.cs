using TheCuriousCreative2.ViewModels;
using TheCuriousCreative2.Controls;

namespace TheCuriousCreative2;

public partial class ClientManagement : ContentPage
{
    private AddUpdateClientViewModel _viewMode;
    public ClientManagement(AddUpdateClientViewModel viewModel)
	{
		InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    //private async void TapGestureRecognizer_Tapped_NavigateToPoupPage1(object sender, EventArgs e)
    //{
    //    await App.Current.MainPage.Navigation.PushModalAsync(new BasePopupPage());
    //}

    //onchange when student table is changed
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetClientListCommand.Execute(null);
    }

}
