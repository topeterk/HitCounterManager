﻿//MIT License

//Copyright (c) 2024-2025 Peter Kirmeier

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
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using static HitCounterManager.IAutoSplitterCoreInterface;

namespace HitCounterManager
{
    public interface IAutoSplitterCoreInterface
    {
        #region Populated by HitCounterManager

        /// <summary>
        /// Gets or sets the active game index.
        /// Value is the game number of the AutoSplitter's selected game.
        /// </summary>
        int ActiveGameIndex { get; set; }

        /// <summary>
        /// Gets or sets the ASC practice mode activation (no automatic splitting).
        /// </summary>
        bool PracticeMode { get; set; }

        /// <summary>
        /// Gets or sets the currently selected profile name.
        /// </summary>
        string ProfileName { get; set; }

        /// <summary>
        /// Gets all profile names.
        /// </summary>
        /// <returns></returns>
        List<string> ProfileNames { get; }

        /// <summary>
        /// Create a profile with the given new name and select it.
        /// </summary>
        /// <param name="NewName">Name of the new profile</param>
        void ProfileNew(string NewName);

        /// <summary>
        /// Start over a new run.
        /// Increased the attempt counter, select first split and reset all values.
        /// </summary>
        void ProfileReset();

        /// <summary>
        /// Amount of available splits in the current run.
        /// </summary>
        int SplitCount { get; }

        /// <summary>
        /// Gets all split names of the currently selected profile.
        /// </summary>
        List<string> SplitsNames { get; }

        /// <summary>
        /// Index of currently active split.
        /// </summary>
        int ActiveSplit { get; }

        /// <summary>
        /// Creates a split at the end of the splits with the given new name.
        /// </summary>
        /// <param name="NewName">Name of the new split</param>
        void SplitAppendNew(string NewName);

        /// <summary>
        /// Modifies the currently selected split by given <paramref name="Amount"/>.
        /// </summary>
        /// <param name="Amount">Amount of splits that will be moved forwards/backwards</param>
        void ProfileSplitGo(int Amount);

        /// <summary>
        /// Inreases or decreases the hit counts of the currently selected split by <paramref name="Amount"/>.
        /// </summary>
        /// <param name="Amount">Positive values will increase and negative will decrease hit count respectively</param>
        /// <param name="IsWayHit">true = count towards way hits, false = count towards (boss) hits</param>
        public void HitSumUp(int Amount, bool IsWayHit);

        /// <summary>
        /// Indicates if timer is currently running.
        /// </summary>
        bool TimerRunning { get; }

        /// <summary>
        /// Starts/Stops the timer.
        /// </summary>
        void StartStopTimer(bool Start);

        /// <summary>
        /// Triggers an update of the timer values.
        /// When in game time is available the time will be updated based on that value.
        /// Otherwise the elapsed time since last update will be stored.
        /// </summary>
        void UpdateDuration();

        #endregion

        #region Populated by AutoSplitterCore

        /// <summary>
        /// List of available games for the AutoSplitter game selection.
        /// The index for each game is also the games' index for ActiveGameIndex.
        /// The collection should be filled once the registration method is called.
        /// </summary>
        ObservableCollection<string> GameList { get; }

        delegate bool GetCurrentInGameTimeDelegate(out long totalTimeMs);

        /// <summary>
        /// Method returning the in game time if available.
        /// If not available false will be returned.
        /// If available true will be returned and the totalTimeMs is set to the value of the in game time.
        /// The method should be filled once the registration method is called.
        /// </summary>
        GetCurrentInGameTimeDelegate GetCurrentInGameTimeMethod { get; set; }

        /// <summary>
        /// Method that gets called when the user wants to access the settings dialog.
        /// A bool will be given indicating if dark mode is enabled.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action<bool /* DarkMode */> OpenSettingsMethod { get; set; }

        /// <summary>
        /// Method that gets called when the user wants to save settings.
        /// This may happen either manually at any time or right before shutdown of application.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action SaveSettingsMethod { get; set; }

        /// <summary>
        /// Method that gets called when the user changes the ActiveGameIndex.
        /// An int will be given with the new ActiveGameIndex.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action<int /* ActiveGameIndex */> SetActiveGameIndexMethod { get; set; }

        /// <summary>
        /// Method that gets called when the user changes the PracticeMode.
        /// A bool will be given with the new PracticeMode setting.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action<bool /* PracticeMode */> SetPracticeModeMethod { get; set; }

        /// <summary>
        /// Method that gets called when the user starts over a new run.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action SplitterResetMethod { get; set; }

        /// <summary>
        /// Method that gets called when another profile got selected.
        /// A string will be given with the new selected profile name.
        /// The method should be filled once the registration method is called.
        /// </summary>
        Action<string /* ProfileName */> ProfileSelectedMethod { get; set; }

        #endregion
    }

    public static class AutoSplitterCoreModule
    {
        private static object AutoSplitterInstance;
        private static MethodInfo RegisterHitCounterManagerInterface;

        /// <summary>
        /// Indicates that application has successfully loaded AutoSplitterCore plugin.
        /// </summary>
        public static bool AutoSplitterCoreLoaded { get; private set; } = false;

        public static void AutoSplitterCoreTryLoadModule()
        {
            string DllPath = Path.GetFullPath("AutoSplitterCore.dll");
            if (File.Exists(DllPath))
            {
                try
                {
                    Assembly AutoSplitterAssembly = Assembly.LoadFrom(DllPath);
                    if (AutoSplitterAssembly.GetType("AutoSplitterCore.AutoSplitterMainModule") is Type AutoSplitterMainModuleType)
                    {
                        AutoSplitterInstance = Activator.CreateInstance(AutoSplitterMainModuleType);
                        RegisterHitCounterManagerInterface = AutoSplitterMainModuleType.GetMethod(nameof(RegisterHitCounterManagerInterface));
                        AutoSplitterCoreLoaded = true;
                    }
                }
                catch { }
            }
        }

        public static void AutoSplitterRegisterInterface(AutoSplitterCoreInterface interfaceASC)
        {
            RegisterHitCounterManagerInterface?.Invoke(AutoSplitterInstance, [interfaceASC]);
        }
    }

    public class AutoSplitterCoreInterface : IAutoSplitterCoreInterface
    {
        private readonly Form1 form1;
        private readonly ProfilesControl profCtrl;

        public AutoSplitterCoreInterface(Form1 form)
        {
            form1 = form;
            profCtrl = form1.profCtrl;
            GameList.CollectionChanged += (object sender, NotifyCollectionChangedEventArgs e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add: foreach (string game in e.NewItems) form1.comboBoxGame.Items.Add(game); break;
                    case NotifyCollectionChangedAction.Remove: foreach (string game in e.NewItems) form1.comboBoxGame.Items.Remove(game); break;
                    case NotifyCollectionChangedAction.Reset: form1.comboBoxGame.Items.Clear(); break;
                    default: break;
                }
            };
        }

        public bool GetCurrentInGameTime(out long totalTimeMs)
        {
            if (GetCurrentInGameTimeMethod is null)
            {
                totalTimeMs = 0;
                return false;
            }
            return GetCurrentInGameTimeMethod(out totalTimeMs);
        }

        public void OpenSettings() => OpenSettingsMethod?.Invoke(Program.DarkMode);

        public void SaveSettings() => SaveSettingsMethod?.Invoke();

        public void SetActiveGameIndex(int ActiveGameIndex) => SetActiveGameIndexMethod?.Invoke(ActiveGameIndex);

        public void SetPracticeMode(bool PracticeMode) => SetPracticeModeMethod?.Invoke(PracticeMode);

        public void SplitterReset() => SplitterResetMethod?.Invoke();

        public void ProfileSelected(string ProfileName) => ProfileSelectedMethod?.Invoke(ProfileName);

        #region IAutoSplitterCoreInterface

        public int ActiveGameIndex
        {
            get => form1.comboBoxGame.SelectedIndex;
            set => form1.comboBoxGame.SelectedIndex = value;
        }

        public bool PracticeMode
        {
            get => form1.PracticeModeCheck.Checked;
            set => form1.PracticeModeCheck.Checked = value;
        }

        public string ProfileName
        {
            get => profCtrl.SelectedProfileInfo.ProfileName;
            set => profCtrl.SelectedProfileInfo.ProfileName = value;
        }

        public List<string> ProfileNames => [.. profCtrl.GetProfileList()];

        public void ProfileNew(string NewName) => profCtrl.ProfileNew(NewName);

        public void ProfileReset() => profCtrl.ProfileReset();

        public int SplitCount => profCtrl.SelectedProfileInfo.SplitCount;

        public List<string> SplitsNames => profCtrl.SelectedProfileInfo.SplitNames;

        public int ActiveSplit => profCtrl.SelectedProfileInfo.ActiveSplit;

        public void SplitAppendNew(string NewName) => profCtrl.SelectedProfileInfo.AddSplit(NewName, 0, 0, 0, 0, 0, 0);

        public void ProfileSplitGo(int Amount) => profCtrl.ProfileSplitGo(Amount);

        public void HitSumUp(int Amount, bool IsWayHit)
        {
            if (IsWayHit) profCtrl.SelectedProfileInfo.WayHit(Amount);
            else profCtrl.SelectedProfileInfo.Hit(Amount);
        }

        public bool TimerRunning => profCtrl.TimerRunning;

        public void StartStopTimer(bool Start) => form1.StartStopTimer(Start);

        public void UpdateDuration() => profCtrl.UpdateDuration();

        public ObservableCollection<string> GameList { get; private set; } = [];

        public GetCurrentInGameTimeDelegate GetCurrentInGameTimeMethod { get; set; }

        public Action<bool /* DarkMode */> OpenSettingsMethod { get; set; }

        public Action SaveSettingsMethod { get; set; }

        public Action<int /* ActiveGameIndex */> SetActiveGameIndexMethod { get; set; }

        public Action<bool /* PracticeMode */> SetPracticeModeMethod { get; set; }

        public Action SplitterResetMethod { get; set; }

        public Action<string /* ProfileName */> ProfileSelectedMethod { get; set; }

        #endregion
    }
}
