//MIT License

//Copyright (c) 2021-2024 Peter Kirmeier

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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using ReactiveUI;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Controls.Templates;
using Avalonia.Controls;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Common
{
    /// <summary>
    /// Loads an image (Bitmap) from local resources (avares://)
    /// </summary>
    public class LocalResourceBitmap : Bitmap
    {
        public LocalResourceBitmap(string path) : base(AssetLoader.Open(new Uri($"avares://{Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty}{path}"))) { }
    }

    /// <summary>
    /// Should be replaced with DynamicResource.
    /// Workaround: Avalonia DataTriggerBehavior does not always work when page is loaded.
    ///             This class seems to work as kind of a proxy.
    ///             During first pass the DataTriggerBehavior is created and this object is instantiated,
    ///             however, the trigger will somehow not be fired.
    ///             During second pass the markup extension (ProvideValue) gets executed,
    ///             but this time the trigger was/gets fired as well.
    /// See: https://github.com/wieslawsoltes/AvaloniaBehaviors/issues/56
    /// See: https://stackoverflow.com/questions/68979876/how-to-simulate-datatrigger-with-avalonia
    /// </summary>
    public class LazyResource : MarkupExtension
    {
        private string Key { get; init; }

        public LazyResource(string key) => Key = key;

        public override object ProvideValue(IServiceProvider serviceProvider) => App.CurrentApp.Resources[Key]!;
    }

    /// <summary>
    /// Markup extension to load embedded string resources (EmbeddedResource) directly into XAML.
    /// </summary>
    public class StringFromManifest : MarkupExtension
    {
        static Dictionary<string, string> LoadedStringSources = new Dictionary<string, string>();

        public string? Resource { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Resource == null) return null!;
            if (LoadedStringSources.ContainsKey(Resource)) return LoadedStringSources[Resource];

            string result = new StreamReader(AssetLoader.Open(new Uri($"resm:{Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty}{Resource}"))).ReadToEnd();
            LoadedStringSources.Add(Resource, result);
            return result;
        }
    }

    /// <summary>
    /// Avalonia workaround to support custom colors for BoxShadows
    /// </summary>
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

    /// <summary>
    /// Inverts/Negates a bool value. This works for Statics as well, for usual Bindings it is recommended to use bang operator "!".
    /// (see: https://docs.avaloniaui.net/docs/data-binding/converting-binding-values)
    /// </summary>
    public class NegateBoolConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool?)value ?? null;
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => !(bool?)value;

        /// <summary>
        /// Avalonia needs this.
        /// The converter is resolved as a markup extension and therefore requires this method:
        /// See: https://github.com/AvaloniaUI/Avalonia/issues/2554 or https://github.com/AvaloniaUI/Avalonia/issues/2835
        /// </summary>
        /// <returns>Instance to the converted (itself)</returns>
        public IValueConverter ProvideValue() => this;
    }

    /// <summary>
    /// Wrapper for a ReactiveObject with some additional helper methods
    /// </summary>
    public class NotifyPropertyChangedImpl : ReactiveObject
    {
        /// <summary>
        /// Triggers an PropertyChanged event of INotifyPropertyChanged
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

    public static class Extensions
    {
        /// <summary>
        /// Tries to open an URI with the system's registered default browser.
        /// (See: https://github.com/dotnet/runtime/issues/21798)
        /// </summary>
        /// <param name="uri">URI that shall be opened</param>
        public static void OpenWithBrowser(Uri uri)
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

    public class ViewLocator : IDataTemplate
    {
        public Control Build(object? data)
        {
            var name = data?.GetType().FullName!.Replace("ViewModel", "View");
            var type = name is null ? null : Type.GetType(name);

            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }

            return new TextBlock { Text = "Not Found: " + name };
        }

        public bool Match(object? data)
        {
            return data is ViewModelBase;
        }
    }
}
