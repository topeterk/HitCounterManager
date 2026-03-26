// SPDX-FileCopyrightText: © 2021-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using HitCounterManager.ViewModels;

namespace HitCounterManager.Common
{
    /// <summary>
    /// Loads an image (Bitmap) from local resources (avares://)
    /// </summary>
    public class LocalResourceBitmap(string path) : Bitmap(AssetLoader.Open(new Uri($"avares://{Assembly.GetExecutingAssembly().GetName().Name ?? string.Empty}{path}")))
    {
    }

    /// <summary>
    /// Markup extension to load embedded string resources (EmbeddedResource) directly into XAML.
    /// </summary>
    public class StringFromManifest : MarkupExtension
    {
        static readonly Dictionary<string, string> LoadedStringSources = [];

        public string? Resource { get; set; }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Resource == null) return null!;
            if (LoadedStringSources.TryGetValue(Resource, out var existingResource)) return existingResource;

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

        private static string ResolveColorFromString(string s)
        {
            int index = s.TrimEnd().LastIndexOf(' ');
            string keep = s[..(index + 1)];
            string value = s[(index + 1)..];
            if (Color.TryParse(keep, out var color_value))
                return keep + color_value.ToString();
            else if (App.CurrentApp.Resources.TryGetValue(value, out var resourceColor))
            {
                if (resourceColor is SolidColorBrush brush)
                    return keep + brush.Color.ToString();
                else if (resourceColor is Color color)
                    return keep + color.ToString();
            }
            return s;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => BoxShadows;
    }

    /// <summary>
    /// Wrapper for a INotifyPropertyChanged with some additional helper methods
    /// </summary>
    public class NotifyPropertyChangedImpl : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Triggers an PropertyChanged event of INotifyPropertyChanged
        /// </summary>
        /// <param name="propertyName">Name of the changing property</param>
        public void RaisePropertyChanged([CallerMemberName] string? propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

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
                RaisePropertyChanged(propertyName);
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
                RaisePropertyChanged(propertyName); // This will reset the UI to last valid value
            return false;
        }
    }

    public static class Extensions
    {
        /// <summary>
        /// Tries to open an URI with the system's registered default browser.
        /// </summary>
        /// <param name="uri">URI that shall be opened</param>
        public static void OpenWithBrowser(Uri uri)
        {
            ILauncher? launcher = App.CurrentApp.TopLevel?.Launcher;
            if (launcher is not null)
            {
                Dispatcher.UIThread.Post(async () => await launcher.LaunchUriAsync(uri), DispatcherPriority.ApplicationIdle);
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


    // Based on template from https://docs.avaloniaui.net/docs/input-interaction/commanding (and MiniCommand from Avalonia samples)
    public sealed class RelayCommand<T> : ICommand
    {
        private readonly Action<T>? _execute;
        private readonly Func<T, Task>? _executeAsync;
        private readonly Func<bool>? _canExecute;

#pragma warning disable CS0067
        public event EventHandler? CanExecuteChanged; // Does not fully support CanExecuteChanged!
#pragma warning restore CS0067

        public RelayCommand(Action<T> execute, Func<bool>? canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public RelayCommand(Func<T, Task> execute, Func<bool>? canExecute = null)
        {
            _executeAsync = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke() ?? true;

        public async void Execute(object? parameter)
        {
            if (CanExecute(null))
            {
                if (_execute != null)
                    _execute((T)parameter!);
                else
                    await _executeAsync!((T)parameter!);
            }
        }
    }

    // Factory for RelayCommand<T>
    public sealed class RelayCommand
    {
        public static RelayCommand<object> Create(Action execute, Func<bool>? canExecute = null) => new(_ => execute(), canExecute);
        public static RelayCommand<object> CreateFromTask(Func<Task> execute, Func<bool>? canExecute = null) => new(_ => execute(), canExecute);

        public static RelayCommand<T> Create<T>(Action<T> execute, Func<bool>? canExecute = null) => new(execute, canExecute);
        public static RelayCommand<T> CreateFromTask<T>(Func<T, Task> execute, Func<bool>? canExecute = null) => new(execute, canExecute);
    }
}
