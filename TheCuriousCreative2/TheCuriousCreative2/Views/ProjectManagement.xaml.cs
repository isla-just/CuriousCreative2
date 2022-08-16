using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2;


public partial class ProjectManagement : ContentPage
{
    private ProjectViewModel _viewMode;
    public ProjectManagement(ProjectViewModel viewModel)
    {
        InitializeComponent();
        _viewMode = viewModel;
        this.BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _viewMode.GetProjectListCommand.Execute(null);
    }
}