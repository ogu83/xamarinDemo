using System;

using Xamarin.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using UIKit;
using System.Net.Http;

namespace xamarinDemo
{
    public class CustomerResult 
    {
        public List<Customer> customers { get; set; }
        public object meta { get; set; }
    }

    public class Customer
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string customer_url { get; set; }
    }

    public class ListViewItem 
    {
        public int Index{ get; set; }
        public string Name { get; set; }
    }

    public class CustomersPage : ContentPage
    {
        public CustomersPage()
        {
            GetFromWeb();
        }

        public async void GetFromWeb()
        {
            var client = new HttpClient();
            var jsonStr = await client.GetStringAsync("https://api.predic8.de/shop/customers/");
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<CustomerResult>(jsonStr);
            var customers = response.customers;
        }
    }

    public class ListViewPage: ContentPage
    {        
        public List<ListViewItem> Items { get; set; }

        public ListViewPage()
        {
            Items = new List<ListViewItem>();

            var header = new Label
            {
                Text = "ListView"
            };

            var button = new Button
            {
                Text = "+"
            };

            var listView = new ListView()
            {
                ItemsSource = Items,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid() { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                    var iLabel = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    iLabel.SetBinding(Label.TextProperty, "Index");

                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Name");

                    grid.Children.Add(iLabel, 0, 0);
                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            listView.ItemSelected += (sender, e) => 
            {
                var elem = e.SelectedItem as ListViewItem;
                if (elem != null) 
                {
                    var msg = $"willst du {elem.Name} löschen";
                    UIAlertView alert = new UIAlertView();
                    alert.Title = "Löschen?";
                    alert.AddButton("OK");
                    alert.AddButton("Absagen");
                    alert.Message = msg;
                    alert.AlertViewStyle = UIAlertViewStyle.Default;
                    alert.Clicked += (sender1, e1) =>
                    {
                        if (e1.ButtonIndex == 0)
                        {                            
                            Items.Remove(elem);
                            listView.ItemsSource = null;
                            listView.ItemsSource = Items;
                        }
                    };
                    alert.Show();
                }

                listView.SelectedItem = null;
            };

            button.Clicked += (sender, e) =>
            {
                UIAlertView alert = new UIAlertView();
                alert.Title = "Nahme?";
                alert.AddButton("OK"); 
                alert.AddButton("Absagen");
                alert.Message = "Geben sie eine nahme";
                alert.AlertViewStyle = UIAlertViewStyle.PlainTextInput;
                alert.Clicked += (sender1, e1) => 
                {
                    if (e1.ButtonIndex == 0) 
                    {
                        var input = alert.GetTextField(0).Text;
                        Items.Add(new ListViewItem() 
                        {
                            Index = Items.Count, 
                            Name = input 
                        });
                        listView.ItemsSource = null;
                        listView.ItemsSource = Items;
                    }
               };
                alert.Show();
            };

            var headerStack = new StackLayout
            {
                Children =
                {
                    header,
                    button
                },
                Orientation = StackOrientation.Horizontal
            };



            Content = new StackLayout
            {
                Children =
                {
                    headerStack,
                    listView
                }
            };
        }
    }

    public class MasterPageItem
    {
        public string Title { get; set; }    
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
    }

    public class StartPage: ContentPage
    {
        public StartPage()
        {
            var header = new Label
            {
                Text = "Start Seite"
            };
            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    header,
                }
            };
        }
    }

    public class MenuPage : ContentPage
    {
        public ListView ListView { get { return listView; }}
        ListView listView;

        public MenuPage()
        {
            var masterPageItems = new List<MasterPageItem>();
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "Startseite",
                IconSource ="",
                TargetType = typeof(StartPage)
            });
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "ListView",
                IconSource = "",
                TargetType = typeof(ListViewPage)
            });
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "Kunden",
                IconSource = "",
                TargetType = typeof(CustomersPage)
            });
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "Sound",
                IconSource = "",
                TargetType = null
            });
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "Admin",
                IconSource = "",
                TargetType = null
            });
            masterPageItems.Add(new MasterPageItem()
            {
                Title = "Logout",
                IconSource = "",
                TargetType = null
            });

            listView = new ListView()
            {
                ItemsSource = masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid() { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title");

                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            Title = "Menu";
            Content = new StackLayout
            {
                Children = 
                { 
                    listView 
                }
            };
        }
    }

    public class Page1: MasterDetailPage
    {
        MenuPage masterPage;
        ContentPage detailPage;

        public Page1()
        {
            masterPage = new MenuPage();
            detailPage = new StartPage();

            Master = masterPage;
            Detail = detailPage;

            masterPage.ListView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var elem = e.SelectedItem as MasterPageItem;

                if (elem != null)
                {
                    if (elem.Title == "Logout")
                    {
                        Application.Current.MainPage = new NavigationPage(new LoginPage());
                    }
                    else
                    {
                        if (elem.TargetType != null)
                            Detail = new NavigationPage((Page)Activator.CreateInstance(elem.TargetType));
                        
                        masterPage.ListView.SelectedItem = null;
                        IsPresented = false;
                    }
                }
            };
        }
    }

    public class LoginPage : ContentPage
    {
        public LoginPage()
        {
            var header = new Label
            {
                Text = "Login"             
            };

            var userName = new Entry
            {                
                Placeholder = "Benutzername"
            };

            var password = new Entry
            {                
                Placeholder = "Kennwort",
                IsPassword = true
            };

            var login = new Button
            {
                Text = "Login",
            };

            login.Clicked += (sender, e) => 
            {
                if (userName.Text.ToLower() == "admin" && password.Text.ToLower() == "1234")
                {
                    Application.Current.MainPage = new NavigationPage(new Page1());
                }
                else
                {
                    DisplayAlert("Fehler","Zugriff verweigert","Ok");
                }
            };

            Content = new StackLayout
            {
                VerticalOptions = LayoutOptions.Start,
                Children =
                {
                    header,
                    userName,
                    password,
                    login
                }
            };
        }
    }
    
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            var content = new ContentPage
            {
                Title = "xamarinDemo",
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.Center,
                    Children = {
                        new Label {
                            HorizontalTextAlignment = TextAlignment.Center,
                            Text = "Welcome to Xamarin Forms!"
                        }
                    }
                }
            };

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
