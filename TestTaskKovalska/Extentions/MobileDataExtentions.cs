using System.Collections.ObjectModel;
using RichMvvm.Extentions;
using RichMvvm.Models;
using TestKovalska.Models;
using TestKovalska.ViewModels;
using TestTaskKovalska.Controls;

namespace TestTaskKovalska.Extentions;

public static class MobileDataExtentions
{
    public static List<TreeViewNode> ToTreeViewNodes(this List<TechnicalPlace> mobileData)
    {
        var result = new List<TreeViewNode>();
        if (mobileData.IsEmpty())
        {
            return result;
        }

        foreach (var item in mobileData)
        {
            var node = new TreeViewNode(item.Name);
            if (item.TechnicalPlaces.IsNotEmpty())
            {
                var subNode = new TreeViewNode("TechnicalPlaces");
                foreach (var subItem in item.TechnicalPlaces.ToTreeViewNodes())
                {
                    subNode.Children.Add(subItem);
                }
                node.Children.Add(subNode);
            }

            if (item.Equipments.IsNotEmpty())
            {
                var subNode = new TreeViewNode("Equipments");
                foreach (var equip in item.Equipments)
                {
                    subNode.Children.Add(new TreeViewNode(equip.Name)
                    {
                        AdvancedAction = new Action(async () =>
                            await App.CurrentViewModel.ShowViewModelAsync<DetailsViewModel>(new NavigationParameters()
                                { { "Equipment", equip } }))
                    });
                    subNode.AdvancedAction = new Action(async () =>
                        await App.CurrentViewModel.ShowViewModelAsync<DetailsViewModel>(new NavigationParameters()
                        { { "Equipment", equip } }));
                }
                node.Children.Add(subNode);
            }
            result.Add(node);
        }
        
        return result;
    }
}