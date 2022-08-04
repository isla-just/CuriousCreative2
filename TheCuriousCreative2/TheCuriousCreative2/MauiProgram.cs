﻿namespace TheCuriousCreative2;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Poppins-Bold.otf", "PoppinsBold");
                fonts.AddFont("Poppins-Italic.otf", "PoppinsItalic");
                fonts.AddFont("Poppins-Medium.otf", "PoppinsMedium");
                fonts.AddFont("Poppins-Regular.otf", "PoppinsRegular");
                fonts.AddFont("Poppins-SemiBold.otf", "PoppinsSemiBold");

                fonts.AddFont("Satoshi-Bold.otf", "SatoshiBold");
                fonts.AddFont("Satoshi-Light.otf", "SatoshiLight");
                fonts.AddFont("Satoshi-Medium.otf", "SatoshiMedium");
                fonts.AddFont("Satoshi-Regular.otf", "SatoshiRegular");
            });

		return builder.Build();
	}
}
