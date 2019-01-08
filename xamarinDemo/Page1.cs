using System;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class Page1 : MasterDetailPage
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
}
