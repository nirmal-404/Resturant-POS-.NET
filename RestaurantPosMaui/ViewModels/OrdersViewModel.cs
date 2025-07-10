using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RestaurantPosMaui.Data;
using RestaurantPosMaui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantPosMaui.ViewModels;
public partial class OrdersViewModel : ObservableObject
{
    private readonly DatabaseService _databaseService;
    public OrdersViewModel(DatabaseService databaseService)
    { 
        _databaseService = databaseService;
    }

    public ObservableCollection<OrderModel> Orders { get; set; } = [];

    // Returns true if the order creation was successfull, false otherwise
    public async Task<bool> PlaceOrderAsync(CartModel[] cartItems, bool isPaidOnline)
    {
        var orderItems = cartItems
            .Select(c => new OrderItem
            {
                Icon = c.Icon,
                ItemId = c.ItemId,
                Name = c.Name,
                Price = c.Price,
                Quantity = c.Quantity
            }).ToArray();

        var orderModel = new OrderModel
        {
            OrderDate = DateTime.Now,
            PaymentMode = isPaidOnline ? "Online" : "Cash",
            TotalAmountPaid = cartItems.Sum(c => c.Amount),
            TotalItemsCount = cartItems.Length,
            Items = orderItems
        };

        var errorMessage = await _databaseService.PlaceOrderAsync(orderModel);
        if (!string.IsNullOrEmpty(errorMessage))
        {
            // Order creation failed
            await Shell.Current.DisplayAlert("Error", errorMessage, "Ok");
            return false;
        }
        // Order creation was successfull
        Orders.Add(orderModel);
        await Toast.Make("Order placed successfully").Show();
        return true;
    }


    private bool _isInitialized;

    [ObservableProperty]
    private bool _isLoading;

    public async Task InitilizeAsync()
    { 
        if(_isInitialized) return;
        _isInitialized = true;
        IsLoading = true;

        var dbOrders = await _databaseService.GetOrdersAsync();
        var orders = dbOrders.Select(o => new OrderModel
        {
            Id = o.Id,
            OrderDate = DateTime.Now,
            PaymentMode = o.PaymentMode,
            TotalAmountPaid = o.TotalAmountPaid,
            TotalItemsCount = o.TotalItemsCount
        });

        foreach (var order in orders)
        { 
            Orders.Add(order);
        }
        IsLoading = false;
    }

    [ObservableProperty]
    private OrderItem[] _orderItems = [];

    [RelayCommand]
    private async Task SelectOrderAsync(OrderModel? order)
    { 
        var preSelectedOrder = Orders.FirstOrDefault(o => o.IsSelected);
        if (preSelectedOrder != null)
        {
            preSelectedOrder.IsSelected = false;
            if (preSelectedOrder.Id == order?.Id)
            {
                OrderItems = [];
                return;
            }
        }
        if(order == null || order.Id <= 0)
        {
            OrderItems = [];
            return;
        }
        IsLoading = true;
        order.IsSelected = true;
        OrderItems = await _databaseService.GetOrderItemsAsync(order.Id);
        IsLoading = false;
    }
}

