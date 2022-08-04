namespace TheCuriousCreative2;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new AppShell();
        Routing.RegisterRoute("Login", typeof(Login));
        Routing.RegisterRoute("Dashboard", typeof(Dashboard));
        Routing.RegisterRoute("ClientManagement", typeof(ClientManagement));
        Routing.RegisterRoute("Funds", typeof(Funds));
        Routing.RegisterRoute("ProjectManagement", typeof(ProjectManagement));
        Routing.RegisterRoute("StaffManagement", typeof(StaffManagement));
    }
}

