using TheCuriousCreative2.ViewModels;

namespace TheCuriousCreative2.Views;

    public partial class AddUpdateProject : ContentPage
    {
        public AddUpdateProject(AddUpdateProjectViewModel viewModel)
        {
            InitializeComponent();
            this.BindingContext = viewModel;
        }
    }