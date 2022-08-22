using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class StaffManagement : ContentPage
{
    private AddUpdateStaffViewModel _viewMode;
    public StaffManagement(AddUpdateStaffViewModel viewModel)
    {
        InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    //onchange when staff table is changed
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetStaffListCommand.Execute(null);
    }
}
