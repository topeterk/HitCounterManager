// SPDX-FileCopyrightText: © 2021-2026 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System.Windows.Input;
using HitCounterManager.Common;

namespace HitCounterManager.ViewModels
{
    public class UpdatePageViewModel : ViewModelWindowBase
    {
        public static SettingsRoot Settings => App.CurrentApp.Settings;

        public UpdatePageViewModel()
        {
            DownloadReleaseLog = RelayCommand.Create(() =>
            {
                if (GitHubUpdate.QueryAllReleases())
                {
                    RaisePropertyChanged(nameof(LatestVersionName));
                    RaisePropertyChanged(nameof(FullChangeLog));
                }
            });
            WebOpenLatestRelease = RelayCommand.Create(() => {
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
