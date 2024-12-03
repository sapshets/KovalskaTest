using System.Collections.ObjectModel;
using Kotlin.Jvm.Internal;
using RichMvvm.Extentions;
using RichMvvm.Models;
using TestKovalska.Models;
using TestKovalska.Services.Abstract;
using TestTaskKovalska;
using TestTaskKovalska.Controls;
using TestTaskKovalska.Extentions;
using Result = Android.App.Result;

namespace TestKovalska.ViewModels;

public class MainViewModel : BaseViewModel
{
    private IRestService _rest;

    public Command RefreshCommand => new Command(Refresh);
    public MobileData MobileData { get; set; }
    public ObservableCollection<TreeViewNode> Items => MobileData.IsNull() ? new ObservableCollection<TreeViewNode>() : MobileData.TechnicalPlaces.ToTreeViewNodes().ToObservableCollection();
    public MainViewModel(IRestService rest)
    {
        _rest = rest;
        Refresh();
    }

    public override async void OnNavigatedTo(NavigationParameters parameters)
    {
        App.CurrentViewModel = this;
        base.OnNavigatedTo(parameters);
    }

    private async void Refresh()
    {
        try
        {
            IsLoading = true;
            var request = new MobileSyncRequestModel
            {
                Jsonrpc = "2.0",
                Id = "F586042C-8B26-4C8E-8B76-E8AA2E17421D",
                Method = "GetMonitoringPlan",
                Params = new Params
                {
                    PersonnelNumber = "1350019",
                    FactoryCode = "01050",
                    Date = "2024-10-16"
                }
            };
            MobileData = await _rest.GetMobileData(request);
            RaisePropertyChanged(nameof(Items));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        finally
        {
            IsLoading = false;
        }
        
    }
}