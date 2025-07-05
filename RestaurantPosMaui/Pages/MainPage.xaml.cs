using RestaurantPosMaui.ViewModels;

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
}
