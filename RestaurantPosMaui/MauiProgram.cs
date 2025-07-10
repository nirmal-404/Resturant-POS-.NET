using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Pages;
using RestaurantPosMaui.ViewModels;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace RestaurantPosMaui
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var culture = new CultureInfo("en-LK");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<DatabaseService>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<OrdersViewModel>()
                .AddSingleton<OrdersPage>();

            return builder.Build();
        }
    }
}
