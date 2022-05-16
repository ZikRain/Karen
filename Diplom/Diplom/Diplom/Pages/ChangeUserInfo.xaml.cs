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
    public partial class ChangeUserInfo : ContentPage
    {
        public ChangeUserInfo(User user)
        {
            InitializeComponent();
            Load(user);
        }

        

        private void Load(User user)
        {
            name.Text = user.name;
            surname.Text = user.surname;
            patronymic.Text = user.patronymic;
            telephone.Text = user.telephone;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            string newName = name.Text;
            string newSurname = surname.Text;
            string newPatronymic = patronymic.Text;
            string newTelephone = telephone.Text;

            try
            {
                await WebApiWrapper.UserUpdateProfile(newName, newSurname, newPatronymic, newTelephone);
                MessagingCenter.Send(this, "UpdateUser");
                await Navigation.PopModalAsync();

            }catch (Exception ex)
            {
                await DisplayAlert("Ошибка", ex.Message, "OK");
            }
        }
    }
}