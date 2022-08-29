namespace TheCuriousCreative2;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute("Dashboard", typeof(Dashboard));
        Routing.RegisterRoute("Login", typeof(Login));
        Routing.RegisterRoute("ClientManagement", typeof(ClientManagement));
        Routing.RegisterRoute("Funds", typeof(Funds));
        Routing.RegisterRoute("ProjectManagement", typeof(ProjectManagement));
        Routing.RegisterRoute("StaffManagement", typeof(StaffManagement));
    }
}

