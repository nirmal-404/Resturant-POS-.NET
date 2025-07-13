using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Models;

namespace RestaurantPosMaui.Controls;

public partial class SaveMenuItemFormControl : ContentView
{
    public SaveMenuItemFormControl()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ItemProperty =
        BindableProperty.Create(nameof(Item), typeof(MenuItemModel), typeof(SaveMenuItemFormControl), new MenuItemModel(), propertyChanged: OnItemChanged );

    public MenuItemModel Item 
    { 
        get => (MenuItemModel)GetValue(ItemProperty) ; 
        set => SetValue(ItemProperty, value); 
    }

    private static void OnItemChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue is MenuItemModel menuItemModel)
        {
            if(bindable is SaveMenuItemFormControl thisControl)
            {
                if (menuItemModel.Id > 0)
                {
                    thisControl.itemIcon.Source = menuItemModel.Icon;
                    thisControl.itemIcon.HeightRequest = thisControl.itemIcon.WidthRequest = 100;
                }
                else
                {
                    thisControl.itemIcon.Source = "image_add_regular_36.png";
                    thisControl.itemIcon.HeightRequest = thisControl.itemIcon.WidthRequest = 36;
                }

            }
        }
    }

    [RelayCommand]
    private void ToggleCategorySelection(MenuCategoryModel category) => 
        category.IsSelected = !category.IsSelected;


    public event Action? OnCancel;

    [RelayCommand]
    private void Cancel() => OnCancel?.Invoke();
}