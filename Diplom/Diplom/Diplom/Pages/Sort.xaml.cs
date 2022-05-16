using Diplom.AdditionalEntities;
using Diplom.API.AdditionalModel;
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
    public partial class Sort : ContentPage
    {
        public ObservableCollection<CategoryData> Category { get; set; } = new ObservableCollection<CategoryData>();
        public Sort(ObservableCollection<CategoryData> Category)
        {
            InitializeComponent();
            Load(Category);
            
        }

        private void Load(ObservableCollection<CategoryData> category)
        {
            Category = category;
            collection.ItemsSource = Category;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button button = sender as Button;

            CategoryData category = button.CommandParameter as CategoryData;

            MessagingCenter.Send(this,"Sort",category);

            await Navigation.PopModalAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}