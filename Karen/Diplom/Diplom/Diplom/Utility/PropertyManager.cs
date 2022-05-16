using Diplom.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Diplom.Utility
{
    internal class PropertyManager
    {
        public static string GetTicket()
        {
            Application.Current.Properties.TryGetValue("ticket", out object ticket);
            if (ticket == null)
            {
                return "";
            }
            else
            {
                return ticket.ToString();
            }
        }
        public static async void SetTicket(string ticket)
        {
            Application.Current.Properties.Remove("ticket");
            Application.Current.Properties.Add("ticket", ticket);
            await Application.Current.SavePropertiesAsync();
        }
        public static async void DeleteProperties()
        {
            Application.Current.Properties.Clear();
            await Application.Current.SavePropertiesAsync();
            
        }
    }
}
