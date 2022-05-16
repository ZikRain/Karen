using Diplom.API;
using Diplom.API.Entities;
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
    public partial class ProductInfo : ContentPage
    {
        Product Product { get; set; } = new Product();
        public ProductInfo(Product product)
        {
            InitializeComponent();
            Load(product);
        }

        private async void Load(Product product)
        {
            Product = product;
            image.Source = Product.Image;
            name.Text = Product.Name;
            price.Text = Product.Price + "₽";
            quantity.Text = Product.Quantity.ToString();
            collection.ItemsSource = product.options;
            
        }

        private void button_add_Clicked(object sender, EventArgs e)
        {
            Product.Quantity = 1;
            Load(Product);
            MessagingCenter.Send(this, "UpdateProduct", Product);
        }

        private void button_minus_Clicked(object sender, EventArgs e)
        {
            if(Product.Quantity > 1)
            {
                Product.Quantity -= 1;
            }
            else
            {
                Product.Quantity = 0;
            }
            Load(Product);
            MessagingCenter.Send(this, "UpdateProduct", Product);
        }

        private void quantity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                Label label = (Label)sender;
                int Q = Convert.ToInt32(label.Text);
                if (Q > 0)
                {
                    (label.Parent.Parent.FindByName("group_buttons") as StackLayout).IsVisible = true;
                    (label.Parent.Parent.FindByName("button_add") as Button).IsVisible = false;
                }
                else
                {
                    (label.Parent.Parent.FindByName("group_buttons") as StackLayout).IsVisible = false;
                    (label.Parent.Parent.FindByName("button_add") as Button).IsVisible = true;
                }
            }
        }

        private void button_plus_Clicked(object sender, EventArgs e)
        {
            if (Product.Quantity > Product.Count)
            {
                Product.Quantity += 1;
            }
            else
            {
                DisplayAlert("Ошибка", "На складе нет столько товара", "ОК");
            }
            Load(Product);
            MessagingCenter.Send(this, "UpdateProduct", Product);
        }

        private void CheckBox_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsChecked")
            {
                MessagingCenter.Send(this, "UpdateProduct", Product);
            }
        }
    }
}