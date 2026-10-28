// SPDX-FileCopyrightText: © 2023-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using HitCounterManager.Common;
using HitCounterManager.ViewModels;
using System.ComponentModel;

namespace HitCounterManager.Views
{
    public partial class MainWindow : WindowBase<MainPageViewModel>
    {
        private static SettingsRoot Settings => App.CurrentApp.Settings;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            base.InitializeComponent();

            double offset = 0D;

            #region AutoSplitter

            if (AutoSplitterCoreModule.AutoSplitterCoreLoaded &&
                App.CurrentApp.Resources.TryGetValue("MainButtonWidth", out var mainButtonWidthObj)
                && mainButtonWidthObj is double mainButtonWidth)
            {
                offset = mainButtonWidth;
            }

            #endregion

            Width = Settings.MainWidth + offset;
            Height = Settings.MainHeight;

            Closing += ClosingHandler;
            PositionChanged += PositionChangedHandler;
        }

        protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
        {
            base.OnPropertyChanged(change);

            if (change.Property == WindowStateProperty && (change.NewValue is WindowState newState))
            {
                if (Settings.TrayIconEnable && Settings.TrayIconMinimize && (newState == WindowState.Minimized))
                {
                    Hide();
                }
            }
        }

        private void PositionChangedHandler(object? sender, PixelPointEventArgs e)
        {
            Settings.MainPosX = Position.X;
            Settings.MainPosY = Position.Y;
        }

        private void ClosingHandler(object? sender, CancelEventArgs e)
        {
            Settings.MainWidth = (int)Width;
            Settings.MainHeight = (int)Height;
        }
    }
}
