using RestaurantPosMaui.ViewModels;
using MenuItem = RestaurantPosMaui.Data.MenuItem;
namespace RestaurantPosMaui.Pages;

public partial class MainPage : ContentPage
{
    private HomeViewModel _homeViewModel;

    public MainPage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = homeViewModel;
        Initialize();
    }

    private async void Initialize()
    { 
        await _homeViewModel.InitializeAsync();
    }

    private async void CategoriesListControl_OnCategorySelected(Models.MenuCategoryModel category) 
    { 
        await _homeViewModel.SelectCategoryCommand.ExecuteAsync(category.Id);
    }

    private async void MenuItemsListControl_OnSelectItem(MenuItem menuItem)
    {
        _homeViewModel.AddToCartCommand.Execute(menuItem);
    }

}