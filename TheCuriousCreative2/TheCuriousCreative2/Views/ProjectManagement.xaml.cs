using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;

public partial class ProjectManagement : ContentPage
{
    private AddUpdateProjectViewModel _viewMode;
    public ProjectManagement(AddUpdateProjectViewModel viewModel)
    {
        InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    //onchange when project table is changed
    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetProjectListCommand.Execute(null);
    }
}
