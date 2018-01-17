﻿using System;
using Xamarin.Forms;
using System.Collections;
using System.Linq;
using System.Reflection;
using CustomLayouts.ViewModels;

namespace CustomLayouts
{
    public class PagerIndicatorTabs : ScrollView , BaseIndicator
    {
        int _selectedIndex;

        public Grid GridContainer { get; set; } = new Grid()
        {
            //HorizontalOptions = LayoutOptions.CenterAndExpand;
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Color.Gray,
        };

        public PagerIndicatorTabs()
        {
            GridContainer.WidthRequest = 500;
            GridContainer.RowDefinitions.Add(new RowDefinition() { Height = 35 });
            this.Content = GridContainer;
            this.ScrollToAsync(100, 0, true);
            this.Orientation = ScrollOrientation.Horizontal;


            /*
            var assembly = typeof(PagerIndicatorTabs).GetTypeInfo().Assembly;
            foreach (var res in assembly.GetManifestResourceNames())
                System.Diagnostics.Debug.WriteLine("found resource: " + res);
            */

        }

        void CreateTabs()
        {

            if (GridContainer.Children != null && GridContainer.Children.Count > 0) GridContainer.Children.Clear();

            foreach (var item in ItemsSource)
            {

                var index = GridContainer.Children.Count;
                var tab = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                    Padding = new Thickness(7),
                };

                if (item is HomeViewModel homeViewModel)
                {
                    tab.Children.Add(new Label { Text = homeViewModel.Title + (index + 1), FontSize = 11 });
                }

                /*
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        tab.Children.Add(new Image { Source = "pin.png", HeightRequest = 20 });
                        tab.Children.Add(new Label { Text = "Tab " + (index + 1), FontSize = 11 });
                        break;

                    case Device.Android:
                        tab.Children.Add(new Image { Source = "pin.png", HeightRequest = 25 });
                        break;
                }
                */

                var tgr = new TapGestureRecognizer();
                tgr.Command = new Command(() =>
                {
                    SelectedItem = ItemsSource[index];
                });
                tab.GestureRecognizers.Add(tgr);
                GridContainer.Children.Add(tab, index, 0);
            }
        }

        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IList),
                typeof(PagerIndicatorTabs),
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    ((PagerIndicatorTabs)bindable).ItemsSourceChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((PagerIndicatorTabs)bindable).ItemsSourceChanged();
                }
        );

        public IList ItemsSource
        {
            get
            {
                return (IList)GetValue(ItemsSourceProperty);
            }
            set
            {
                SetValue(ItemsSourceProperty, value);
            }
        }

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(PagerIndicatorTabs),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((PagerIndicatorTabs)bindable).SelectedItemChanged();
                }
        );

        public object SelectedItem
        {
            get
            {
                return GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        void ItemsSourceChanging()
        {
            if (ItemsSource != null)
                _selectedIndex = ItemsSource.IndexOf(SelectedItem);
        }

        void ItemsSourceChanged()
        {
            if (ItemsSource == null) return;

            this.GridContainer.ColumnDefinitions.Clear();
            foreach (var item in ItemsSource)
            {
                this.GridContainer.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }

            CreateTabs();
        }

        void SelectedItemChanged()
        {

            var selectedIndex = ItemsSource.IndexOf(SelectedItem);
            var pagerIndicators = GridContainer.Children.Cast<StackLayout>().ToList();

            foreach (var pi in pagerIndicators)
            {
                UnselectTab(pi);
            }

            if (selectedIndex > -1)
            {
                SelectTab(pagerIndicators[selectedIndex]);
            }
        }

        static void UnselectTab(StackLayout tab)
        {
            tab.Opacity = 0.5;
        }

        static void SelectTab(StackLayout tab)
        {
            tab.Opacity = 1.0;
        }
    }
}

