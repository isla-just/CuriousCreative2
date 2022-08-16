using TheCuriousCreative2.Views;

namespace TheCuriousCreative2;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddUpdateProject), typeof(AddUpdateProject));
    }
}

