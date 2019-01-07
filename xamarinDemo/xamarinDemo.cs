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

    public class Meta 
    {
        public int count { get; set; }
        public int limit { get; set; }
        public int page { get; set; }
        public string next_url { get; set; }
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

    public class MasterPageItem
    {
        public string Title { get; set; }    
        public string IconSource { get; set; }
        public Type TargetType { get; set; }
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
