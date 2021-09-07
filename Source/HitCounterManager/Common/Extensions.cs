//MIT License

//Copyright (c) 2021-2021 Peter Kirmeier

//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all
//copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
//SOFTWARE.

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HitCounterManager.Common
{
    /// <summary>
    /// Markup extension to load embedded resources directly into XAML.
    /// see https://docs.microsoft.com/de-de/xamarin/xamarin-forms/user-interface/images?tabs=windows
    /// see https://docs.microsoft.com/de-de/xamarin/xamarin-forms/xaml/markup-extensions/creating
    /// </summary>
    [ContentProperty(nameof(Resource))]
    public class ImageFromResource : IMarkupExtension
    {
        public string Resource { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Resource == null) return null;
            return ImageSource.FromResource(Resource, typeof(ImageFromResource).GetTypeInfo().Assembly);
        }
    }

    public class NegateBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => !(bool)value;
    }

    /// <summary>
    /// Replace INotifyPropertyChanged with this class to be able to use the simplified CallPropertyChanged
    /// </summary>
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Triggers an PropertyChanged event of INotifyPropertyChanged
        /// Note: As noted here (https://twitter.com/ogormanphilip/status/1240740053652922368), it is safe to call this on non-UI threads
        /// </summary>
        /// <param name="sender">The sender who was the origin of this change</param>
        /// <param name="propertyName">Name of the changing property</param>
        public void CallPropertyChanged(object sender, string propertyName) => PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// When a different value is set for the property, it gets updated and an INotifyPropertyChanged event is fired 
        /// </summary>
        /// <typeparam name="T">Type that can be checked for equality</typeparam>
        /// <param name="sender">Origin of the change, will be given to the INotifyPropertyChanged</param>
        /// <param name="property">The property that shall be changed</param>
        /// <param name="newValue">The new value for the property</param>
        /// <param name="propertyName">The name of the property, recommended to use: nameof(xxx)</param>
        /// <returns>true = data has changed, false = data has not changed</returns>
        public bool SetAndNotifyWhenChanged<T>(object sender, ref T property, T newValue, string propertyName) where T : IEquatable<T>
        {
            if (!property.Equals(newValue))
            {
                property = newValue;
                CallPropertyChanged(sender, propertyName);
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// When a different value is set for the property, it gets updated and an INotifyPropertyChanged event is fired.
        /// The value must be a natural number to allow update of the property
        /// </summary>
        /// <param name="sender">Origin of the change, will be given to the INotifyPropertyChanged</param>
        /// <param name="property">The property that shall be changed</param>
        /// <param name="newValueString">The new value for the property as string</param>
        /// <param name="propertyName">The name of the property, recommended to use: nameof(xxx)</param>
        /// <returns>true = data has changed, false = data has not changed</returns>
        public bool SetAndNotifyWhenNaturalNumberChanged(object sender, ref int property, string newValueString, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(newValueString)) return false;

            int i;
            if (!Extensions.TryParseMinMaxNumber(newValueString, out i, 0, int.MaxValue))
            {
                CallPropertyChanged(this, propertyName); // This will reset to last valid value
                return false;
            }

            return SetAndNotifyWhenChanged(sender, ref property, i, propertyName);
        }  

        /// <summary>
        /// When a different value is set for the property, it gets updated and an INotifyPropertyChanged event is fired.
        /// The value must be a natural number to allow update of the property
        /// </summary>
        /// <param name="sender">Origin of the change, will be given to the INotifyPropertyChanged</param>
        /// <param name="property">The property that shall be changed</param>
        /// <param name="newValue">The new value for the property</param>
        /// <param name="propertyName">The name of the property, recommended to use: nameof(xxx)</param>
        /// <returns>true = data has changed, false = data has not changed</returns>
        public bool SetAndNotifyWhenNaturalNumberChanged(object sender, ref int property, int newValue, string propertyName)
        {
            if (newValue < 0)
            {
                // This will reset to last valid value
                CallPropertyChanged(this, propertyName);
                return false;
            }

            return SetAndNotifyWhenChanged(sender, ref property, newValue, propertyName);
        }
    }

    public class NaturalNumbersEntryValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += OnEntryTextChanged;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= OnEntryTextChanged;
            base.OnDetachingFrom(entry);
        }

        private static void OnEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.NewTextValue)) return;

            int i;
            if (!Extensions.TryParseMinMaxNumber(e.NewTextValue, out i)) ((Entry)sender).Text = e.OldTextValue;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Workaround that Picker in some cases acts like using a OneTime binding.
        /// This function replaces the ItemSource that will reload the values.
        /// see: https://github.com/xamarin/Xamarin.Forms/issues/4077
        /// </summary>
        /// <param name="picker">Picker control that shall update its values.</param>
        public static void ForceUpdate(this Picker picker)
        {
            System.Collections.IList prevSource = picker.ItemsSource;
            object prevSelectedItem = picker.SelectedItem;
            picker.ItemsSource = null;
            picker.ItemsSource = prevSource;
            picker.SelectedItem = prevSelectedItem;
        }

        /// <summary>
        /// Tries to convert a string into an integer that is in a given range
        /// </summary>
        /// <param name="output">Integer that is set when successfull</param>
        /// <param name="input">String to convert</param>
        /// <param name="minValue">Min value of the range</param>
        /// <param name="maxValue">Max value of the range</param>
        /// <returns>true = successfull, false = cannot be converted or not in range</returns>
        public static bool TryParseMinMaxNumber(string input, out int output, int minValue = int.MinValue, int maxValue = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                output = 0;
                return false;
            }

            if (int.TryParse(input, out output))
            {
                if ((0 <= output.CompareTo(minValue)) && (output.CompareTo(maxValue) <= 0))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
