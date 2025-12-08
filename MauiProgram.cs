using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using FreshMvvm.Maui.Extensions;
using RGPopup.Maui.Extensions;
using Maui.TouchEffect.Hosting;
using UraniumUI;
namespace CS.ERP_MOB
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiTouchEffect()
                .UseUraniumUI()
                .UseUraniumUIMaterial()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("FontAwesome5Solid.otf", "FontAwesomeSolid");
                    fonts.AddFont("FontAwesome5Regular.ttf", "FontAwesomeRegular");
                    fonts.AddFont("FontAwesome5Brands.ttf", "FontAwesomeBrands");
                    //fonts.AddFont("Font Awesome 7 Free-Solid-900.otf", "FontAwesomeSolid");

                })
                .UseMauiRGPopup(config =>
                {
                    config.BackPressHandler = null;
                    config.FixKeyboardOverlap = true;
                });

            builder.Services.Add(ServiceDescriptor.Transient<MainPage, MainPage>());
            builder.Services.Add(ServiceDescriptor.Transient<MainPageModel, MainPageModel>());

#if DEBUG
            builder.Logging.AddDebug();
#endif

            //return builder.Build();
            MauiApp mauiApp = builder.Build();

            mauiApp.UseFreshMvvm();
            return mauiApp;
        }
    }
}
