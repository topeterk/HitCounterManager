// SPDX-FileCopyrightText: © 2023-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

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

        internal readonly MainPage InnerPage;

        public MainWindow()
        {
            AvaloniaXamlLoader.Load(this);
            InitializeComponent();

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

            InnerPage = this.FindControl<MainPage>("InnerPage")!;

            Closing += ClosingHandler;
            PositionChanged += PositionChangedHandler;
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
