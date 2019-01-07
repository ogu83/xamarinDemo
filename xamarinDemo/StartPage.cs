using Xamarin.Forms;

namespace xamarinDemo
{
    public class StartPage : ContentPage
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
}
