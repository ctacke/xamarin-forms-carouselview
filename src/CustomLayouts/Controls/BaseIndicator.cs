﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomLayouts
{
    /// <summary>
    /// all indicator need to inherit the interface of View
    /// <scc cref="View"/>
    /// </summary>
    public interface BaseIndicator : IViewController, IVisualElementController, IElementController 
    {
        int SelectedIndex { get; }

        void CreateTabs();

        IList ItemsSource { get; set; }

        object SelectedItem { get; set; }

        void ItemsSourceChanging();

        void ItemsSourceChanged();

        void SelectedItemChanged();
    }

    /*
    public class Indicator
    {
        public static BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(BaseIndicator.ItemsSource),
                typeof(IList),
                typeof(BaseIndicator),
                null,
                BindingMode.OneWay,
                propertyChanging: (bindable, oldValue, newValue) =>
                {
                    ((BaseIndicator)bindable).ItemsSourceChanging();
                },
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((BaseIndicator)bindable).ItemsSourceChanged();
                }
        );

        public static BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(BaseIndicator.ItemsSource),
                typeof(object),
                typeof(BaseIndicator),
                null,
                BindingMode.TwoWay,
                propertyChanged: (bindable, oldValue, newValue) =>
                {
                    ((BaseIndicator)bindable).SelectedItemChanged();
                }
        );
    }
    */
}
