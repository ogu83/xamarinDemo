using System.Collections.ObjectModel;
using UIKit;
using Xamarin.Forms;
using xamarinDemo.ViewModels;

namespace xamarinDemo
{
    public class ListViewPage : ContentPage
    {
        public ObservableCollection<ListViewItem> Items { get; set; }

        public ListViewPage()
        {
            Items = new ObservableCollection<ListViewItem>();

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
                    var alert = new UIAlertView();
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
                            foreach (var item in Items)
                            {
                                item.Index = Items.IndexOf(item) + 1;
                            }
                            //listView.ItemsSource = null;
                            //listView.ItemsSource = Items;
                        }
                    };
                    alert.Show();
                }

                listView.SelectedItem = null;
            };

            button.Clicked += (sender, e) =>
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
                        Items.Add(new ListViewItem()
                        {
                            Index = Items.Count+1,
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
}
