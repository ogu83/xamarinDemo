using System.Net.Http;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class CustomersPage : ContentPage
    {
        const string baseUrl = "https://api.predic8.de";

        public CustomersPage()
        {
            GetFromWeb();
        }

        public async void GetFromWeb()
        {
            var client = new HttpClient();
            var jsonStr = await client.GetStringAsync($"{baseUrl}/shop/customers/");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerResult>(jsonStr);
            var customers = response.customers;
        }
    }
}
