using Diplom.AdditionalEntities;
using Diplom.API;
using Diplom.API.AdditionalModel;
using Diplom.API.Entities;
using Diplom.API.Enum;
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
    public partial class Catalog : ContentPage
    {
        public ObservableCollection<CategoryData> Category { get; set; } = new ObservableCollection<CategoryData>();
        public Catalog()
        {
            InitializeComponent();
            Load();
            Subscribe();
        }

        

        private void Subscribe()
        {
            MessagingCenter.Subscribe<Cart,Product>(this,
                "DeleteFromCart",
                (sender, args) =>
                {
                    Category.FirstOrDefault(x => x.id == args.categoryID).FirstOrDefault(x => x.ID == args.ID).Quantity = 0;
                    Category.FirstOrDefault(x => x.id == args.categoryID).FirstOrDefault(x => x.ID == args.ID).options.Where(x => x.enable).ToList().ForEach(x=>x.enable=false);
                });
            MessagingCenter.Subscribe<ProductInfo, Product>(this,
                "UpdateProduct",
                (sender, args) =>
                {
                    Category.FirstOrDefault(x => x.id == args.categoryID).FirstOrDefault(x => x.ID == args.ID).Quantity = args.Quantity;
                    Category.FirstOrDefault(x => x.id == args.categoryID).FirstOrDefault(x => x.ID == args.ID).options = args.options;
                
                });
            MessagingCenter.Subscribe<Cart>(this,
                "ClearCart",
                (sender) =>
                {
                    Category.ToList().ForEach(x => x.ToList().ForEach(y => y.Quantity = 0));
                    Category.ToList().ForEach(x=>x.ToList().ForEach(y=>y.options.ForEach(z=>z.enable = false)));
                });
            MessagingCenter.Subscribe<Sort,CategoryData>(this,
                "Sort",
                (sender,arg) =>
                {
                    collection.ScrollTo(arg.First());
                });


        }

        private async Task Load()
        {
            try
            {
                var list = await WebApiWrapper.ProductList();

                foreach (var item in list)
                {
                    var a = new CategoryData(item.name, item.id, new ObservableCollection<Product>(item.products));
                    Category.Add(a);
                }

                
                foreach (var category in Category)
                {
                    foreach(var item in category)
                    {
                        item.options = await WebApiWrapper.ProductGetOptions(item.ID);

                    }
                }

                collection.ItemsSource = Category;

            }catch (Exception ex)
            {
                await DisplayAlert("Ошибка",ex.Message,"ОК");
            }
            
        }

        private async void collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = e.CurrentSelection.FirstOrDefault() as Product;
            if (product == null) return;
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new ProductInfo(product));
        }

        private void button_minus_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            Product product = button.CommandParameter as Product;

            Product productInList = Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID);

            if (productInList.Quantity > 1)
            {
                Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID).Quantity--;
            }
            else
            {
                Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID).Quantity=0;
            }

            MessagingCenter.Send(this, "UpdateProduct", Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID));
        }
        private async void button_plus_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            Product product = button.CommandParameter as Product;

            Product productInList = Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID);

            if(productInList.Count > productInList.Quantity)
            {
                Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID).Quantity++;
                MessagingCenter.Send(this, "UpdateProduct", Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID));
            }
            else
            {
                await DisplayAlert("Ошибка","На складе кончился товар","ОК");
            }
            
        }
        
        private void quantity_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Text")
            {
                Label label = (Label)sender;

                int Q = Convert.ToInt32(label.Text);

                if(Q> 0)
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
        private async void button_add_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            Product product = button.CommandParameter as Product;

            Product productInList = Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID);

            if (productInList.Count > 0)
            {
                Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID).Quantity = 1;

                MessagingCenter.Send(this, "UpdateProduct", Category.FirstOrDefault(x => x.id == product.categoryID).FirstOrDefault(x => x.ID == product.ID));
            }
            else
            {
                await DisplayAlert("Ошибка", "На складе нет этого товар", "ОК");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new Sort(Category));
        }
    }
}