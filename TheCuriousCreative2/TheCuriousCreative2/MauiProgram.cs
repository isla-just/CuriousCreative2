using TheCuriousCreative2.Services;
using TheCuriousCreative2.ViewModels;


namespace TheCuriousCreative2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
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

        //views
        builder.Services.AddSingleton<ClientManagement>();

        //view models
        //builder.Services.AddSingleton<ClientListPageViewModel>();
        builder.Services.AddSingleton<AddUpdateClientViewModel>();

        return builder.Build();
	}
}

