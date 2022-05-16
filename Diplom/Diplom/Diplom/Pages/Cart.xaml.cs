using Diplom.API;
using Diplom.API.Entities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Diplom.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cart : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();
        public Cart()
        {
            NavigationPage.SetBackButtonTitle(this, "");
            InitializeComponent();
            Load();
            Subscribe();
          
        }
        private void Subscribe()
        {
            MessagingCenter.Subscribe<ProductInfo, Product>(this,
                "UpdateProduct",
                (sender, args) =>
                {
                    if (args.Quantity > 0)
                    {
                        if (Products.FirstOrDefault(x => x.ID == args.ID) == null)
                        {
                            Products.Add(args);
                        }
                        else
                        {
                            Products.FirstOrDefault(x => x.ID == args.ID).Quantity = args.Quantity;
                            Products.FirstOrDefault(x => x.ID == args.ID).options = args.options;
                        }
                    }
                    else
                    {
                        Products.Remove(Products.FirstOrDefault(x => x.ID == args.ID));
                    }
                    
                    Load();
                });
            MessagingCenter.Subscribe<Catalog, Product>(this,
                "UpdateProduct",
                (sender, args) =>
                {
                    if (args.Quantity > 0)
                    {
                        if (Products.FirstOrDefault(x => x.ID == args.ID) == null)
                        {
                            Products.Add(args);
                        }
                        else
                        {
                            Products.FirstOrDefault(x => x.ID == args.ID).Quantity = args.Quantity;
                            Products.FirstOrDefault(x => x.ID == args.ID).options = args.options;
                        }
                    }
                    else
                    {
                        Products.Remove(Products.FirstOrDefault(x => x.ID == args.ID));
                    }

                    Load();
                });

        }
        private void Load()
        {
            if(Products.Count == 0)
            {
                empty_cart.IsVisible = true;
                button_addOrder.IsVisible = false;
            }
            else
            {
                empty_cart.IsVisible = false;
                button_addOrder.IsVisible = true;
            }
            collection.ItemsSource = Products;
        }

        private void button_delete_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;
            Product product = button.CommandParameter as Product;
            Products.Remove(Products.FirstOrDefault(x => x.ID == product.ID));
            Load();
            MessagingCenter.Send(this, "DeleteFromCart", product);
        }

        private async void button_addOrder_Clicked_1(object sender, EventArgs e)
        {
            if (Products.Count > 0)
            {
                List<OrderItem> orderItems = new List<OrderItem>();

                foreach (Product product in Products)
                {
                    List<OrderItemOption> options = new List<OrderItemOption>();
                    
                    foreach(var i in product.options.Where(x => x.enable))
                    {
                        var option = new OrderItemOption() {
                            optionID = i.id,
                            name = i.name,
                            price = i.price
                        };
                        options.Add(option);
                    }

                    OrderItem item = new OrderItem()
                    {
                        productID = product.ID,
                        name = product.Name,
                        price = product.Price,
                        quantity = product.Quantity,
                        options = options
                        
                    };

                    orderItems.Add(item);
                }

                try
                {
                    Order order = await WebApiWrapper.OrderCreate(orderItems);
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Ошибка", ex.Message, "ОК");
                    ((TabbedPage)Application.Current.MainPage).CurrentPage = ((TabbedPage)Application.Current.MainPage).Children[2];
                    return;
                }

                Products.Clear();
                MessagingCenter.Send(this, "UpdateOrderList");
                MessagingCenter.Send(this, "ClearCart");
                Load();
            }
        }
    }
}