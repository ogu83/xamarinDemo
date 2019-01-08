using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class MenuPage : ContentPage
    {
        public static ObservableCollection<MasterPageItem> masterPageItems = new ObservableCollection<MasterPageItem>();

        public ListView ListView { get { return listView; } }
        ListView listView;

        public MenuPage()
        {          
            if (masterPageItems.Count == 0)
            {                           
                masterPageItems.Add(new MasterPageItem()
                {
                    Title = "Startseite",
                    IconSource = "",
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
                    TargetType = typeof(SoundsPage)
                });
                masterPageItems.Add(new MasterPageItem()
                {
                    Title = "Admin",
                    IconSource = "",
                    TargetType = typeof(AdminEntryPage)
                });
                masterPageItems.Add(new MasterPageItem()
                {
                    Title = "Logout",
                    IconSource = "",
                    TargetType = null
                });
            }

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

            var image = new Image
            {
                Source = ImageSource.FromStream(()=> { 
                    var assembly = typeof(App).GetTypeInfo().Assembly;
                    var imgRes = ResourceLoader.GetEmbeddedResourceStream(assembly, "790012.png");
                    return imgRes; 
                })
            };


            Title = "Menu";
            /*
            Icon = (FileImageSource)ImageSource.FromStream(() =>
            {
                var assembly = typeof(App).GetTypeInfo().Assembly;
                var imgRes = ResourceLoader.GetEmbeddedResourceStream(assembly, "790012.png");
                return imgRes; 
            });
            */

            Content = new StackLayout
            {
                Children =
                {
                    image,
                    listView
                }
            };
        }
    }
}
