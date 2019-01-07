﻿using System.Collections.Generic;
using Xamarin.Forms;

namespace xamarinDemo
{
    public class MenuPage : ContentPage
    {
        public ListView ListView { get { return listView; } }
        ListView listView;

        public MenuPage()
        {
            var masterPageItems = new List<MasterPageItem>();
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
}