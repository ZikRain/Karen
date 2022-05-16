using Diplom.API;
using Diplom.API.Entities;
using Diplom.API.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diplom.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderInfo : ContentPage
    {
        public Order Order { get; set; }
        public OrderInfo(Order order)
        {
            InitializeComponent();
            Load(order);
        }

        private async void Load(Order order)
        {
            try
            {
                Order = await WebApiWrapper.OrderGet(order.id);
                id.Text = Order.id.ToString();
                date.Text = Order.date.ToString();
                status.Text = ConvertEnum.OrderStateToString(Order.state);
                amount.Text = Order.amount.ToString();
                count.Text = Order.CountItems.ToString();

                collection.ItemsSource = Order.items;
            }catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}