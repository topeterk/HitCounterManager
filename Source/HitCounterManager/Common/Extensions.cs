//MIT License

//Copyright (c) 2021-2022 Peter Kirmeier

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ReactiveUI;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Input;
using Avalonia.Input.Platform;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Platform;
using Avalonia.Xaml.Interactions.Events;
using Avalonia.Xaml.Interactivity;
using Bitmap = Avalonia.Media.Imaging.Bitmap;
using Color = Avalonia.Media.Color;

namespace HitCounterManager.Common
{
    public static class Device
    {
        // TODO: Temporary placeholder Device.OpenUri
        // Workaround to open a browser with a given url:
        // https://github.com/dotnet/runtime/issues/21798
        public static void OpenUri(Uri uri)
        {
            string url = uri.OriginalString;
            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url.Replace("&", "^&")}") { CreateNoWindow = true });
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                    Process.Start("xdg-open", url);
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                    Process.Start("open", url);
                else
                    Process.Start(url);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }

    /// <summary>
    /// Markup extension to load embedded image resources (AvaloniaResource) directly into XAML.
    /// TODO: Avalonia see alternative implementation: https://docs.avaloniaui.net/docs/controls/image
    /// </summary>
    public class ImageFromResource : MarkupExtension
    {
        static Dictionary<string, Bitmap> LoadedImageSources = new Dictionary<string, Bitmap>();

        public string? Resource { get; set; }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            if (Resource == null) return null;
            if (LoadedImageSources.ContainsKey(Resource)) return LoadedImageSources[Resource];

            IAssetLoader AssetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            string assemblyName = Assembly.GetEntryAssembly()!.GetName().Name!;
            Bitmap result = new Bitmap(AssetLoader.Open(new Uri($"avares://{assemblyName}{Resource}")));
            LoadedImageSources.Add(Resource, result);
            return result;
        }
    }

    /// <summary>
    /// Markup extension to load embedded string resources (EmbeddedResource) directly into XAML.
    /// </summary>
    public class StringFromManifest : MarkupExtension
    {
        // On GTK: Do not remove this image cache (e.g. with loading on demand) as images may not load properly
        static Dictionary<string, string> LoadedStringSources = new Dictionary<string, string>();

        public string? Resource { get; set; }

        public override object? ProvideValue(IServiceProvider serviceProvider)
        {
            if (Resource == null) return null;
            if (LoadedStringSources.ContainsKey(Resource)) return LoadedStringSources[Resource];

            IAssetLoader AssetLoader = AvaloniaLocator.Current.GetService<IAssetLoader>()!;
            string assemblyName = Assembly.GetEntryAssembly()!.GetName().Name!;
            string result = new StreamReader(AssetLoader.Open(new Uri($"resm:{assemblyName}{Resource}"))).ReadToEnd();
            LoadedStringSources.Add(Resource, result);
            return result;
        }
    }

    // TODO: Avalonia workaround to support custom colors for BoxShadows
    public class BoxShadowsBuilder : MarkupExtension
    {
        private readonly BoxShadows BoxShadows;
        public BoxShadowsBuilder(string s)
        {
            string[] sp = s.Split(',', StringSplitOptions.RemoveEmptyEntries);
            BoxShadow BoxShadowFirst = BoxShadow.Parse(ResolveColorFromString(sp[0]));
            if (1 == sp.Length)
            {
                BoxShadows = new BoxShadows(BoxShadowFirst);
            }
            else if (1 < sp.Length)
            {
                BoxShadow[] BoxShadowsRest = new BoxShadow[sp.Length - 1];
                for (int i = 0; i < sp.Length-1; i++)
                {
                    BoxShadowsRest[i] = BoxShadow.Parse(ResolveColorFromString(sp[i+1]));
                }
                BoxShadows = new BoxShadows(BoxShadowFirst, BoxShadowsRest);
            }
            else BoxShadows = new BoxShadows();
        }

        private string ResolveColorFromString(string s)
        {
            int index = s.TrimEnd().LastIndexOf(" ");
            string keep = s.Substring(0, index + 1);
            string value = s.Substring(index + 1);
            Color color_value;
            if (Color.TryParse(keep, out color_value))
                return keep + color_value.ToString();
            else if (App.CurrentApp.Resources.ContainsKey(value))
            {
                if (App.CurrentApp.Resources[value] is SolidColorBrush brush)
                    return keep + brush.Color.ToString();
                else if (App.CurrentApp.Resources[value] is Color color)
                    return keep + color.ToString();
            }
            return s;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => BoxShadows;
    }

    // TODO Avalonia https://docs.avaloniaui.net/docs/data-binding/converting-binding-values (Convert bool)
    public class NegateBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool?)value ?? null;
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool?)value;
        public IValueConverter ProvideValue() => this; // TODO Avalonia What is this? Why does it require provide method? https://github.com/AvaloniaUI/Avalonia/issues/2835
    }

    /// <summary>
    /// Wrapper for a ReactiveObject with some additional helper methods
    /// </summary>
    public class NotifyPropertyChangedImpl : ReactiveObject
    {
        /// <summary>
        /// Triggers an PropertyChanged event of INotifyPropertyChanged
        /// Note: As noted here (https://twitter.com/ogormanphilip/status/1240740053652922368), it is safe to call this on non-UI threads
        /// TODO Avalonia: Check if this is still safe from outside UI thread?
        /// </summary>
        /// <param name="propertyName">Name of the changing property</param>
        public void CallPropertyChanged([CallerMemberName] string? propertyName = null) => this.RaisePropertyChanged(propertyName);

        /// <summary>
        /// When a different value is set for the property, it gets updated and an INotifyPropertyChanged event is fired 
        /// </summary>
        /// <typeparam name="T">Type that can be checked for equality</typeparam>
        /// <param name="property">The property that shall be changed</param>
        /// <param name="newValue">The new value for the property</param>
        /// <param name="propertyName">The name of the property, recommended to use: nameof(xxx)</param>
        /// <returns>true = data has changed, false = data has not changed</returns>
        public bool SetAndNotifyWhenChanged<T>(ref T property, T newValue, [CallerMemberName] string? propertyName = null) where T : IEquatable<T>
        {
            if (!property.Equals(newValue))
            {
                property = newValue;
                CallPropertyChanged(propertyName);
                return true;
            }
            return false;
        }

        /// <summary>
        /// When a different value is set for the property, it gets updated and an INotifyPropertyChanged event is fired.
        /// The value must be a natural number to allow update of the property
        /// </summary>
        /// <param name="property">The property that shall be changed</param>
        /// <param name="newValue">The new value for the property</param>
        /// <param name="propertyName">The name of the property, recommended to use: nameof(xxx)</param>
        /// <returns>true = data has changed, false = data has not changed</returns>
        public bool SetAndNotifyWhenNaturalNumberChanged(ref int property, int newValue, [CallerMemberName] string? propertyName = null)
        {
            if (0 <= newValue)
                return SetAndNotifyWhenChanged(ref property, newValue, propertyName);
            else
                CallPropertyChanged(propertyName); // This will reset the UI to last valid value
            return false;
        }
    }

    // TODO: Avalonia User Numeric input field?
    public class NaturalNumbersEntryValidationBehavior : Behavior<TextBox>
    {
        public static readonly StyledProperty<RoutingStrategies> RoutingStrategiesProperty =
            AvaloniaProperty.Register<TextInputEventBehavior, RoutingStrategies>(nameof(RoutingStrategies), RoutingStrategies.Tunnel | RoutingStrategies.Bubble);

        public RoutingStrategies RoutingStrategies
        {
            get => GetValue(RoutingStrategiesProperty);
            set => SetValue(RoutingStrategiesProperty, value);
        }

        protected override void OnAttachedToVisualTree()
        {
            AssociatedObject?.AddHandler(TextBox.PastingFromClipboardEvent, TextPaste, RoutingStrategies);
            AssociatedObject?.AddHandler(InputElement.TextInputEvent, TextInput, RoutingStrategies);
        }
        protected override void OnDetachedFromVisualTree()
        {
            AssociatedObject?.RemoveHandler(TextBox.PastingFromClipboardEvent, TextPaste);
            AssociatedObject?.RemoveHandler(InputElement.TextInputEvent, TextInput);
        }

        private void TextPaste(object? sender, RoutedEventArgs e) => ValidateText(e, ((IClipboard)AvaloniaLocator.Current.GetRequiredService(typeof(IClipboard))).GetTextAsync().Result);
        private void TextInput(object? sender, TextInputEventArgs e) => ValidateText(e, e.Text);

        private void ValidateText(RoutedEventArgs e, string? Text)
        {
            int i;
            if (!Extensions.TryParseMinMaxNumber(Text, out i)) e.Handled = true;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Tries to convert a string into an integer that is in a given range
        /// </summary>
        /// <param name="output">Integer that is set when successfull</param>
        /// <param name="input">String to convert</param>
        /// <param name="minValue">Min value of the range</param>
        /// <param name="maxValue">Max value of the range</param>
        /// <returns>true = successfull, false = cannot be converted or not in range</returns>
        public static bool TryParseMinMaxNumber(string? input, out int output, int minValue = int.MinValue, int maxValue = int.MaxValue)
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
