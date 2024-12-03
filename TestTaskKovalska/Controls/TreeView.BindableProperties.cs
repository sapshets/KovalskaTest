using System.Collections;

namespace TestTaskKovalska.Controls;

public partial class TreeView
{
    public static readonly BindableProperty ItemsSourceProperty = BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(TreeView), null, propertyChanging: (b, o, n) => (b as TreeView).OnItemsSourceSetting(o as IEnumerable, n as IEnumerable), propertyChanged: (b, o, v) => (b as TreeView).OnItemsSourceSet());
    public IEnumerable ItemsSource { get => (IEnumerable)GetValue(ItemsSourceProperty); set => SetValue(ItemsSourceProperty, value); }

    public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(TreeView), new DataTemplate(typeof(DefaultTreeViewNodeView)), propertyChanged: (b, o, n) => (b as TreeView).OnItemTemplateChanged());
    public DataTemplate ItemTemplate { get => (DataTemplate)GetValue(ItemTemplateProperty); set => SetValue(ItemTemplateProperty, value); }
 }