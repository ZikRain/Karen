using Diplom.API;
using Diplom.API.Entities;
using Diplom.Utility;
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
    public partial class UserInfo : ContentPage
    {
        public User User { get; set; } = new User();
        public ObservableCollection<Order> Orders { get; set; } = new ObservableCollection<Order>();
        public UserInfo()
        {
            InitializeComponent();
            Load();
            Subscribe();
        }

        private void Subscribe()
        {
            MessagingCenter.Subscribe<ChangeUserInfo>(this,
                "UpdateUser",
                (sender) =>
                {
                    Load();
                });
            MessagingCenter.Subscribe<Cart>(this,
               "UpdateOrderList",
               (sender) =>
               {
                   Load();
               });
        }

        private async Task Load()
        {
            try
            {
                if (await WebApiWrapper.UserIsValidTicket())
                {
                    User = await WebApiWrapper.UserGet();

                }
                else
                {
                    User = null;
                    PropertyManager.DeleteProperties();
                }

                if (User == null)
                {
                    info.IsVisible = false;
                    noninfo.IsVisible = true;
                }
                else
                {
                    info.IsVisible = true;
                    noninfo.IsVisible = false;
                    Title.Text = !string.IsNullOrWhiteSpace(User.name) ? "Здравствуйте, " + User.name + "!" : "Здравствуйте!";
                    fio.Text = (!string.IsNullOrWhiteSpace(User.name) && !string.IsNullOrWhiteSpace(User.surname) && !string.IsNullOrWhiteSpace(User.patronymic)) ? User.name + " " + User.surname + " " + User.patronymic : "Ваши данные не заполнены";
                    telephone.Text = !string.IsNullOrWhiteSpace(User.telephone) ? User.telephone : "Ваши данные не заполнены";
                    Orders = new ObservableCollection<Order>(await WebApiWrapper.OrderList());
                    collection.ItemsSource = Orders;
                }
            }catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void SignIn_Clicked(object sender, EventArgs e)
        {
            string log = login_In.Text;
            string pas = password.Text;

            if (string.IsNullOrWhiteSpace(log) || string.IsNullOrWhiteSpace(pas))
            {
                await DisplayAlert("Ошибка", "Для входа все поля должны быть заполнены", "OK");
                return;
            }

            try
            {
                PropertyManager.SetTicket(await WebApiWrapper.UserAuth(log, pas));
                Load();

            }catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void SignOut_Clicked(object sender, EventArgs e)
        {
            try
            {
                PropertyManager.DeleteProperties();
                await WebApiWrapper.UserSignOut();
                Load();
            }
            catch(Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void collection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var order = e.CurrentSelection.FirstOrDefault() as Order;
            if (order == null) return;
            ((CollectionView)sender).SelectedItem = null;
            await Navigation.PushAsync(new OrderInfo(order));
        }

        private async void Register_Clicked(object sender, EventArgs e)
        {
            string log = login_In.Text;
            string pas = password.Text;

            if (string.IsNullOrWhiteSpace(log) || string.IsNullOrWhiteSpace(pas))
            {
                await DisplayAlert("Ошибка", "Для регистрации все поля должны быть заполнены", "OK");
                return;
            }

            try
            {
                await WebApiWrapper.UserRegister(log, pas);
                Load();
                await DisplayAlert("Реистрация", "Регистрация прошла успешно", "ОК");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new ChangeUserInfo(User));
        }
    }
}