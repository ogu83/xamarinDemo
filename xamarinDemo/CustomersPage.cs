using System.Collections.ObjectModel;
using System.Net.Http;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class CustomersPage : ContentPage
    {
        const string baseUrl = "https://api.predic8.de";

        public ObservableCollection<Customer> Customers { get; set; }

        Grid gridView;

        public CustomersPage()
        {
            Customers = new ObservableCollection<Customer>();

            var header = new Label
            {
                Text = "Kunden"
            };

            gridView = new Grid();
            gridView.ColumnDefinitions.Add(new ColumnDefinition());
            gridView.ColumnDefinitions.Add(new ColumnDefinition());

            Content = new StackLayout
            {
                Children =
                {
                    header, 
                    new ScrollView 
                    { 
                        Content = gridView 
                    }
                }
            };

            GetFromWeb();
        }

        public async void GetFromWeb(string url = "/shop/customers/")
        {
            var client = new HttpClient();
            var jsonStr = await client.GetStringAsync($"{baseUrl}{url}");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerResult>(jsonStr);

            var meta = response.meta;

            var customers = response.customers;
            var cIndex = Customers.Count; //GET PREVIOUS INDEX
            customers.ForEach((customer) =>{ Customers.Add(customer); }); //FILL UP DATA

            var rowCount = customers.Count / 2; //ADD ROWS FOR INCOMING DATA
            for (int r = 0; r < rowCount; r++)
                gridView.RowDefinitions.Add((new RowDefinition()));
                                          
            for (int rowIndex = cIndex/2; rowIndex < Customers.Count/2; rowIndex++)  
            {  
                for (int columnIndex = 0; columnIndex < 2; columnIndex++)  
                {  
                    if (cIndex >= Customers.Count)  
                        break;  
                    
                    var c = Customers[cIndex];
                    cIndex++;
                    var btn = new Button  
                    {  
                        BindingContext = c,
                        Text = $"{c.firstname} {c.lastname}",
                        Margin = 2,
                        BackgroundColor = Color.Silver
                    };  

                    btn.Clicked += (sender, e) => 
                    {
                        var s = sender as Button;
                        if (s != null)
                        {
                            var cb = s.BindingContext as Customer;
                            if (s.Text == cb.customer_url)
                                s.Text = $"{cb.firstname} {cb.lastname}";
                            else
                                s.Text = cb.customer_url;
                        }
                    };

                    gridView.Children.Add(btn, columnIndex, rowIndex);  
                }  
            }

            if (!string.IsNullOrEmpty(meta.next_url) && Customers.Count<meta.count)
                GetFromWeb(meta.next_url);
        }
    }
}