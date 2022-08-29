using TheCuriousCreative2.Services;

namespace TheCuriousCreative2;

public partial class App : Application
{

    public static IStaffService StaffService { get; private set; }
    public static IProjectService ProjectService { get; private set; }
    public static IClientService ClientService { get; private set; }
    public static IFundsService FundsService { get; private set; }



    public App(IStaffService staffService, IProjectService projectService, IClientService clientService, IFundsService fundsService)
    {
        InitializeComponent();

        MainPage = new AppShell();

        StaffService = staffService;
        ProjectService = projectService;
        ClientService = clientService;
        FundsService = fundsService;

    }
}

