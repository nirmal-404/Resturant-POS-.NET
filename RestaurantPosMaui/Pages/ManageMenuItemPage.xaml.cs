using RestaurantPosMaui.ViewModels;

namespace RestaurantPosMaui.Pages;

public partial class ManageMenuItemPage : ContentPage
{

    private readonly ManageMenuItemsViewModel _manageMenuItemsViewModel;
	public ManageMenuItemPage(ManageMenuItemsViewModel manageMenuItemsViewModel)
	{
        InitializeComponent();
        _manageMenuItemsViewModel = manageMenuItemsViewModel;
        BindingContext = _manageMenuItemsViewModel;
        InitializeAsync();
	}

    private async void InitializeAsync() =>
        await _manageMenuItemsViewModel.InitializeAsync();

    private async void CategoriesListControl_OnCategorySelected(Models.MenuCategoryModel categoryModel)
    {
        await _manageMenuItemsViewModel.SelectCategoryCommand.ExecuteAsync(categoryModel.Id);
    }

    private async void MenuItemsListControl_OnSelectItem(Data.MenuItem menuItem) =>
        await _manageMenuItemsViewModel.EditMenuItemCommand.ExecuteAsync(menuItem);

    private void SaveMenuItemFormControl_OnCancel()
    {
        _manageMenuItemsViewModel.CancelCommand.Execute(null);
    }
}