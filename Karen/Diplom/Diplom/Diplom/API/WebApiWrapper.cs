using Diplom.API.Entities;
using Diplom.Utility;
using Newtonsoft.Json;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Diplom.API
{
    internal class WebApiWrapper
    {
        static readonly string _Server = "https://testapi.icbcode.ru";
        static string _Ticket()
        {
            return PropertyManager.GetTicket();
        }
        
        public static async Task<List<Category>> ProductList()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            HttpResponseMessage response = await client.GetAsync($"{_Server}/Product/List");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<List<Category>>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<List<Option>> ProductGetOptions(long id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            HttpResponseMessage response = await client.GetAsync($"{_Server}/Product/GetOptions?product_id={id}");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<List<Option>>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<User> UserGet()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            HttpResponseMessage response = await client.GetAsync($"{_Server}/User/Get");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<string> UserRegister(string login, string password)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("login",login),
                new KeyValuePair<string,string>("password",password)

            });

            HttpResponseMessage response = await client.PostAsync($"{_Server}/User/Register", formContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> UserAuth(string login, string password)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("login",login),
                new KeyValuePair<string,string>("password",password)

            });

            HttpResponseMessage response = await client.PostAsync($"{_Server}/User/Auth", formContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<string> UserSignOut()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("",""),

            });

            HttpResponseMessage response = await client.PostAsync($"{_Server}/User/SignOut", formContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<bool> UserIsValidTicket()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("",""),

            });

            HttpResponseMessage response = await client.PostAsync($"{_Server}/User/IsValidTicket", formContent);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<bool>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<string> UserUpdateProfile(string name,string surname,string patronymic,string telephone)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());
            var formContent = new FormUrlEncodedContent(new[]
                {
                new KeyValuePair<string,string>("name",name),
                new KeyValuePair<string,string>("surname",surname),
                new KeyValuePair<string,string>("patronymic",patronymic),
                new KeyValuePair<string,string>("telephone",telephone)

            });

            HttpResponseMessage response = await client.PostAsync($"{_Server}/User/UpdateProfile", formContent);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
        public static async Task<List<Order>> OrderList()
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            HttpResponseMessage response = await client.GetAsync($"{_Server}/Order/List");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<List<Order>>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<Order> OrderGet(long id)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            HttpResponseMessage response = await client.GetAsync($"{_Server}/Order/Get?order_id={id}");

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
        }
        public static async Task<Order> OrderCreate(List<OrderItem> orderitems)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _Ticket());

            Dictionary<string, string> keys = new Dictionary<string, string>();
            foreach (var item in orderitems)
            {
                keys.Add($"orderItems[{item.productID}]", item.quantity.ToString());
            }

            List<long> option = new List<long>();

            orderitems.ForEach(x => x.options.ForEach(y => option.Add(y.optionID)));

            var optionsArr = option.ToArray();

            for (int i = 0; i < optionsArr.Length; i++)
            {
                keys.Add($"options[{i}]", optionsArr[i].ToString());
            }

            var formContent = new FormUrlEncodedContent(keys);

            HttpResponseMessage response = await client.PostAsync($"{_Server}/Order/Create", formContent);

            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
        }
    }
}
