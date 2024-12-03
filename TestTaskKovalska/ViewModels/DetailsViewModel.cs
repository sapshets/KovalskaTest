using RichMvvm.Models;
using TestKovalska.Models;

namespace TestKovalska.ViewModels;

public class DetailsViewModel : BaseViewModel
{
    public Equipment Equipment { get; set; }

    public override void OnNavigatedTo(NavigationParameters parameters)
    {
        Equipment = parameters.GetValue<Equipment>("Equipment");
        RaisePropertyChanged(nameof(Equipment));
        base.OnNavigatedTo(parameters);
    }
}