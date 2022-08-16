using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class ClientManagement : ContentPage
{
	public ClientManagement(AddUpdateClientViewModel viewModel)
	{
		InitializeComponent();
        this.BindingContext = viewModel;
    }
}
