using Xamarin.Forms;

namespace xamarinDemo
{
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
                    DisplayAlert("Fehler", "Zugriff verweigert", "Ok");
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
}
