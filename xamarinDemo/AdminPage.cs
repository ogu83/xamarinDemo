using UIKit;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class AdminPage : ContentPage
    {
        public AdminPage()
        {
            var header = new Label
            {
                Text = "Admin"
            };

            var subHeader = new Label
            {
                Text = "Menu"
            };

            var listView = new ListView()
            {
                ItemsSource = MenuPage.masterPageItems,
                ItemTemplate = new DataTemplate(() =>
                {
                    var grid = new Grid() { Padding = new Thickness(5, 10) };
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(30) });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Star });

                    var label = new Label { VerticalOptions = LayoutOptions.FillAndExpand };
                    label.SetBinding(Label.TextProperty, "Title", BindingMode.TwoWay);

                    grid.Children.Add(label, 1, 0);

                    return new ViewCell { View = grid };
                }),
                SeparatorVisibility = SeparatorVisibility.None
            };

            listView.ItemSelected += (sender, e) =>
            {
                var item = e.SelectedItem as MasterPageItem;
                if (item != null)
                {                    
                    var alert = new UIAlertView();
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
                            item.Title = input;
                            listView.ItemsSource = null;
                            listView.ItemsSource = MenuPage.masterPageItems;
                        }
                    };
                    alert.Show();
                }
            };

            Button btn = new Button
            {
                Text = "Ok"
            };

            btn.Clicked += (sender, e) => 
            {
                Application.Current.MainPage = new NavigationPage(new Page1());    
            };

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    subHeader,
                    listView,
                    btn
                }
            };
        }

        protected override bool OnBackButtonPressed()
        {
            Application.Current.MainPage = new NavigationPage(new Page1());
            return true;
            //return base.OnBackButtonPressed();
        }
    }
}
