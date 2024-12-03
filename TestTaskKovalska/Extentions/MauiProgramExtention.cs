using RichMvvm.Extentions;
using TestKovalska.Services.Abstract;
using TestKovalska.Services.Impleventation;
using TestKovalska.ViewModels;
using TestTaskKovalska.Views;

namespace TestTaskKovalska.Extentions;

public static class MauiProgramExtention
{
    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<IRestService, RestService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
    {
        builder.RegisterForNavigation<MainPage, MainViewModel>();
        builder.RegisterForNavigation<DetailsPage, DetailsViewModel>();
        return builder;
    }
}