// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using ReactiveUI;
using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class UpdatePageViewModel : ViewModelWindowBase
    {
        public static SettingsRoot Settings => App.CurrentApp.Settings;

        public UpdatePageViewModel()
        {
            DownloadReleaseLog = ReactiveCommand.Create(() =>
            {
                if (GitHubUpdate.QueryAllReleases())
                {
                    CallPropertyChanged(nameof(LatestVersionName));
                    CallPropertyChanged(nameof(FullChangeLog));
                }
            });
            WebOpenLatestRelease = ReactiveCommand.Create(() => {
                GitHubUpdate.WebOpenLatestRelease();
                OwnerWindow?.Close();
            });
        }

#pragma warning disable IDE0079
#pragma warning disable CA1822
        public string LatestVersionName => GitHubUpdate.LatestVersionName;
        public string FullChangeLog => GitHubUpdate.Changelog;
#pragma warning restore CA1822
#pragma warning restore IDE0079

        public bool CheckUpdatesOnStartup
        {
            get => Settings.CheckUpdatesOnStartup;
            set => SetAndNotifyWhenChanged(ref Settings.CheckUpdatesOnStartup, value);
        }

        public ICommand DownloadReleaseLog { get; }
        public ICommand WebOpenLatestRelease { get; }
    }
}
