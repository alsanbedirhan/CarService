using CommunityToolkit.Maui.Views;

namespace CarService.Views;

public partial class SearchPopUp : Popup
{
    public SearchPopUp()
    {
        InitializeComponent();
    }
    public SearchPopUp(List<clsSearch> items, SelectionMode selectionMode, List<decimal>? selected = null) : this()
    {
        ItemsCollectionView.SelectionMode = selectionMode;
        ItemsCollectionView.ItemsSource = items;
        if (selected != null)
        {
            items.Where(x => selected.Contains(x.Key)).ToList().ForEach(ItemsCollectionView.SelectedItems.Add);
        }
        //ItemsCollectionView.SelectionChanged += ItemsCollectionView_SelectionChanged;
    }

    //private void ItemsCollectionView_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    //{
    //    ItemsCollectionView.SelectionChanged -= ItemsCollectionView_SelectionChanged;
    //    Close(e.CurrentSelection.FirstOrDefault());
    //}

    private void Ok_Clicked(object sender, EventArgs e)
    {
        List<clsSearch> items = new List<clsSearch>();
        switch (ItemsCollectionView.SelectionMode)
        {
            case SelectionMode.Single:
                if (ItemsCollectionView.SelectedItem is clsSearch search && search != null)
                {
                    items.Add(search);
                }
                break;
            case SelectionMode.Multiple:
                foreach (clsSearch item in ItemsCollectionView.SelectedItems)
                {
                    items.Add(item);
                }
                break;
        }
        Close(items);
    }

    private void Clear_Clicked(object sender, EventArgs e)
    {
        ItemsCollectionView.SelectedItems.Clear();
    }
}
public class clsSearch
{
    public decimal Key { get; set; }
    public string DisplayValue { get; set; }
}