using RichMvvm.Extentions;
using RichMvvm.Mvvm;
using RichMvvm.Views;
using TestKovalska.ViewModels;

namespace TestTaskKovalska;

public partial class App
{
    public App()
    {
        InitializeComponent();
    }
    
    protected override Window CreateWindow(IActivationState activationState)
    {
        if (MainPage.IsNull())
        {
            MainPage = new BaseNavigationPage(ViewFactory.CreatePage<MainViewModel>());
        }

        return base.CreateWindow(activationState);
    }
}