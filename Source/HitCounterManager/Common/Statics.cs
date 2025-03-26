//MIT License

//Copyright (c) 2021-2025 Peter Kirmeier

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
        public static string ApplicationName { get => ApplicationNameRaw.Name ?? string.Empty; }

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
