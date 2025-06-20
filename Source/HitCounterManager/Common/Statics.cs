// SPDX-FileCopyrightText: © 2021-2025 Peter Kirmeier
// SPDX-License-Identifier: MIT

using System;
using System.Reflection;

namespace HitCounterManager.Common
{
    /// <summary>
    /// Miscellaneous statics
    /// </summary>
    public static class Statics
    {
        /// <summary>
        /// AssemblyName of the running application (e.g. exe file)
        /// On Android it may not be set, so fallback to the running platform class library (e.g. PCL/DLL)
        /// </summary>
        public static AssemblyName ApplicationNameRaw { get => Assembly.GetEntryAssembly()?.GetName() ?? Assembly.GetExecutingAssembly().GetName(); }

        /// <summary>
        /// Name of the running application (e.g. exe file)
        /// On Android it may not be set, so fallback to the running platform class library (e.g. PCL/DLL)
        /// </summary>
        public static string ApplicationName { get => ApplicationNameRaw.Name?.Replace(".PCL","") ?? string.Empty; }

        /// <summary>
        /// Version of the running platform class library (e.g. PCL/DLL)
        /// </summary>
        public static Version? ApplicationVersion { get => ApplicationNameRaw.Version; }

        /// <summary>
        /// Version string of the running platform class library (e.g. PCL/DLL)
        /// </summary>
        public static string ApplicationVersionString { get => ApplicationVersion?.ToString() ?? string.Empty; }

        /// <summary>
        /// Titel string of the application including its version
        /// </summary>
        public static string ApplicationTitle { get => ApplicationName + " - v" + ApplicationVersionString + " ALPHAVERSION"; }

        /// <summary>
        /// Application is capable of global hotkeys
        /// </summary>
        public static bool GlobalHotKeySupport => OperatingSystem.IsWindows();

        /// <summary>
        /// Path to the directory where user files should be stored.
        /// Null defaults to relative paths.
        /// </summary>
        public static string? ApplicationStorageDir { get; set; }
    }
}
