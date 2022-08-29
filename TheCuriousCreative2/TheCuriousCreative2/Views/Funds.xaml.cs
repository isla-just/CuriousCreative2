using TheCuriousCreative2.ViewModels;
using TheCuriousCreative2.Controls;

namespace TheCuriousCreative2;

public partial class Funds : ContentPage
{
    private FundsListViewModel _viewMode;
    public Funds(FundsListViewModel viewModel)
    {
        InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }
}