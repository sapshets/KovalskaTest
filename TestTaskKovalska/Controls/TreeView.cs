using System.Collections;
using System.Collections.Specialized;
using RichMvvm.Extentions;

namespace TestTaskKovalska.Controls;

public partial class TreeView : ContentView
{
    private StackLayout _root = new StackLayout { Spacing = 0 };

    public TreeView()
    {
        Content = _root;
    }

    protected virtual void OnItemsSourceSetting(IEnumerable oldValue, IEnumerable newValue)
    {
        if (oldValue is INotifyCollectionChanged oldItemsSource)
        {
            oldItemsSource.CollectionChanged -= OnItemsSourceChanged;
        }

        if (newValue is INotifyCollectionChanged newItemsSource)
        {
            newItemsSource.CollectionChanged += OnItemsSourceChanged;
        }
    }

    protected virtual void OnItemsSourceSet()
    {
        Render();
    }

    private protected virtual void OnItemsSourceChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                {
                    foreach (var item in e.NewItems)
                    {
                        _root.Children.Insert(e.NewStartingIndex, new TreeViewNodeView(item as IHasChildrenTreeViewNode, ItemTemplate));
                    }
                }
                break;
            case NotifyCollectionChangedAction.Remove:
                {
                    foreach (var item in e.OldItems)
                    {
                        _root.Children.Remove(_root.Children.FirstOrDefault(x => (x as View).BindingContext == item));
                    }
                }
                break;
            default:
                Render();
                break;
        }
    }

    protected virtual void OnItemTemplateChanged()
    {
        // TODO: Some optimizations
        // Eventually
        Render();
    }

    void Render()
    {
        _root.Children.Clear();

        if (ItemsSource == null)
        {
            return;
        }

        foreach (var item in ItemsSource)
        {
            if (item is IHasChildrenTreeViewNode node)
            {
                _root.Children.Add(new TreeViewNodeView(node, ItemTemplate));
            }
        }
    }
}

public class TreeViewNodeView : ContentView
{
    protected Label extendButton;
    protected StackLayout slChildrens;
    protected IHasChildrenTreeViewNode Node { get; }
    protected DataTemplate ItemTemplate { get; }
    public TreeViewNodeView(IHasChildrenTreeViewNode node, DataTemplate itemTemplate)
    {
        var sl = new StackLayout { Spacing = 0 };
        BindingContext = Node = node;
        ItemTemplate = itemTemplate;
        Content = sl;

        slChildrens = new StackLayout { IsVisible = node.IsExtended, Margin = new Thickness(10, 0, 0, 0), Spacing = 0 };

        extendButton = new Label()
        {
            Text = "+",
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Colors.Transparent,
            Opacity = node.IsLeaf ? 0 : 1
        };

        extendButton.Triggers.Add(new DataTrigger(typeof(Label))
        {
            Binding = new Binding(nameof(Node.IsLeaf)),
            Value = true,
            Setters = { new Setter { Property = ImageButton.OpacityProperty, Value = 0 } }
        });

        extendButton.Triggers.Add(new DataTrigger(typeof(Label))
        {
            Binding = new Binding(nameof(Node.IsLeaf)),
            Value = false,
            Setters = { new Setter { Property = ImageButton.OpacityProperty, Value = 1 } }
        });

        extendButton.Triggers.Add(new DataTrigger(typeof(Label))
        {
            Binding = new Binding(nameof(Node.IsExtended)),
            Value = true,
            EnterActions =
            {
                new GenericTriggerAction<Label>((sender) =>
                {
                    sender.Text = "-";
                })
            },
            ExitActions =
            {
                new GenericTriggerAction<Label>((sender) =>
                {
                    sender.Text = "-";
                })
            }
        });

        
        var click = new Action(() =>
        {
            if (!node.CanExtend)
            {
                if (node.AdvancedAction.IsNotNull())
                {
                    node.AdvancedAction.Invoke();
                }

                return;
            }
            node.IsExtended = !node.IsExtended;
            slChildrens.IsVisible = node.IsExtended;

            if (node.IsExtended)
            {
                extendButton.Text = "-";

                if (node is ILazyLoadTreeViewNode lazyNode && lazyNode.GetChildren != null && !lazyNode.Children.Any())
                {
                    var lazyChildren = lazyNode.GetChildren(lazyNode);
                    foreach (var child in lazyChildren)
                    {
                        lazyNode.Children.Add(child);
                    }

                    if (!lazyNode.Children.Any())
                    {
                        extendButton.Opacity = 0;
                        lazyNode.IsLeaf = true;
                    }
                }
            }
            else
            {
                extendButton.Text = "+";
            }
        });

        var content = ItemTemplate.CreateContent() as View;

        extendButton.GestureRecognizers.Add(new TapGestureRecognizer(){Command = new Command(click)});
        content.GestureRecognizers.Add(new TapGestureRecognizer(){Command = new Command(click)});
        sl.Children.Add(new StackLayout
        {
            Orientation = StackOrientation.Horizontal,
            Children =
            {
                extendButton,
                content
            }
        });

        foreach (var child in node.Children)
        {
            slChildrens.Children.Add(new TreeViewNodeView(child, ItemTemplate));
        }

        sl.Children.Add(slChildrens);

        if (Node.Children is INotifyCollectionChanged ovservableCollection)
        {
            ovservableCollection.CollectionChanged += Children_CollectionChanged;
        }
    }

    private void Children_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action == NotifyCollectionChangedAction.Add)
        {
            foreach (var item in e.NewItems)
            {
                slChildrens.Children.Insert(e.NewStartingIndex, new TreeViewNodeView(item as IHasChildrenTreeViewNode, ItemTemplate));
            }
        }

        else if (e.Action == NotifyCollectionChangedAction.Remove)
        {
            foreach (var item in e.OldItems)
            {
                slChildrens.Children.Remove(slChildrens.Children.FirstOrDefault(x => (x as View).BindingContext == item));
            }
        }
    }
}