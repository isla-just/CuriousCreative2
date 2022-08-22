using TheCuriousCreative2.Services;
using TheCuriousCreative2.ViewModels;
using Microsoft.Maui.Controls.Hosting;
using Microsoft.Maui.Hosting;
//using SkiaSharp.Views.Maui.Controls.Hosting;


namespace TheCuriousCreative2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            //.UseSkiaSharp(true)
            .UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
                fonts.AddFont("Satoshi-Regular.otf", "SatoshiRegular");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Bold.otf", "PoppinsBold");
                fonts.AddFont("Poppins-Italic.otf", "PoppinsItalic");
                fonts.AddFont("Poppins-Medium.otf", "PoppinsMedium");
                fonts.AddFont("Poppins-Regular.otf", "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.otf", "PoppinsSemiBold");
                fonts.AddFont("Poppins-Light.otf", "PoppinsLight");

                fonts.AddFont("Satoshi-Bold.otf", "SatoshiBold");
                fonts.AddFont("Satoshi-Light.otf", "SatoshiLight");
                fonts.AddFont("Satoshi-Medium.otf", "SatoshiMedium");
           
            });
        //services
        builder.Services.AddSingleton<IClientService, ClientService>();
        builder.Services.AddSingleton<IFundsService, FundsService>();
        builder.Services.AddSingleton<IProjectService, ProjectService>();
        builder.Services.AddSingleton<IStaffService, StaffService>();

        //views
        builder.Services.AddSingleton<ClientManagement>();
        builder.Services.AddSingleton<Funds>();
        builder.Services.AddSingleton<ProjectManagement>();
        builder.Services.AddSingleton<StaffManagement>();
        builder.Services.AddSingleton<Login>();

        //view models
        builder.Services.AddSingleton<AddUpdateClientViewModel>();
        builder.Services.AddSingleton<FundsListViewModel>();
        builder.Services.AddSingleton<AddUpdateProjectViewModel>();
        builder.Services.AddSingleton<AddUpdateStaffViewModel>();
        builder.Services.AddSingleton<LoginViewModel>();

        return builder.Build();
	}
}

