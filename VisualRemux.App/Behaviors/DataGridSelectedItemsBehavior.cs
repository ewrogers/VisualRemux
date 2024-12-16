using System.Collections;
using System.Collections.Specialized;
using Avalonia;
using Avalonia.Controls;

namespace VisualRemux.App.Behaviors;

public class DataGridSelectedItemsBehavior : AvaloniaObject
{
    public static readonly AttachedProperty<IList?> SelectedItemsProperty
        = AvaloniaProperty.RegisterAttached<DataGridSelectedItemsBehavior, DataGrid, IList?>("SelectedItems");

    public static IList? GetSelectedItems(DataGrid dataGrid) => dataGrid.GetValue(SelectedItemsProperty);

    public static void SetSelectedItems(DataGrid dataGrid, IList? value) =>
        dataGrid.SetValue(SelectedItemsProperty, value);

    static DataGridSelectedItemsBehavior()
    {
        SelectedItemsProperty.Changed.AddClassHandler<DataGrid>(HandleSelectedItemsChanged);
    }

    private static void HandleSelectedItemsChanged(DataGrid dataGrid, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.OldValue is not null)
        {
            dataGrid.SelectionChanged -= OnDataGridSelectionChanged;
        }

        if (e.NewValue is not null)
        {
            dataGrid.SelectionChanged += OnDataGridSelectionChanged;
        }
    }

    private static void OnDataGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is not DataGrid dataGrid)
        {
            return;
        }

        var selectedItems = GetSelectedItems(dataGrid);
        if (selectedItems is not INotifyCollectionChanged)
        {
            return;
        }

        var gridItems = dataGrid.SelectedItems;
        if (gridItems is null)
        {
            return;
        }

        selectedItems.Clear();
        foreach (var item in gridItems)
        {
            selectedItems.Add(item);
        }
    }
}