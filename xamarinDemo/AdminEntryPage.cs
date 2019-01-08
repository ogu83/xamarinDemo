using Xamarin.Forms;

namespace xamarinDemo
{
    public class AdminEntryPage : ContentPage
    {
        public AdminEntryPage()
        {
            var header = new Label
            {
                Text = "Admin"
            };

            var password = new Entry
            {
                Placeholder = "Kennwort",
                IsPassword = true
            };

            var button = new Button
            {
                Text = "OK"
            };

            button.Clicked += (sender, e) =>
            {
                if (password.Text.ToLower() == "1234")
                {
                    Application.Current.MainPage = new NavigationPage(new AdminPage());
                }
                else
                {
                    DisplayAlert("Fehler", "Zugriff verweigert", "Ok");
                }
            };

            Content = new StackLayout
            {
                Children =
                {
                    header,
                    password,
                    button
                }
            };
        }
    }
}
