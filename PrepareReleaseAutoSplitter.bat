@echo off

REM MIT License

REM Copyright(c) 2022 Ezequiel Medina

REM Permission Is hereby granted, free Of charge, to any person obtaining a copy
REM of this software And associated documentation files (the "Software"), to deal
REM in the Software without restriction, including without limitation the rights
REM to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
REM copies of the Software, And to permit persons to whom the Software Is
REM furnished to do so, subject to the following conditions:

REM The above copyright notice And this permission notice shall be included In all
REM copies Or substantial portions of the Software.

REM THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
REM IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
REM FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
REM AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
REM LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
REM OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
REM SOFTWARE.

echo PrepareReleaseAutoSplitter.bat START ===========

REM To run this script you need to have 7zip installed at the default path
REM or you have to update the path:

set PATH=%PATH%;C:\Program Files\7-Zip
set PR_FINAL=FinalFiles
if not exist %PR_FINAL% mkdir %PR_FINAL%

echo Packing Portable AutoSplitterCore Release:
set PR_BASE=bin\ReleaseWin\AutoSplitterCore
set PR_TARGET=%PR_FINAL%\AutoSplitterCorePortable
set PR_OUTPUT=%PR_FINAL%\AutoSplitterCore_Portable_v1.x.0.0.zip
rmdir /S /Q %PR_TARGET% 2>nul
mkdir %PR_TARGET%
del %PR_OUTPUT% 2>nul
FOR %%G IN (AutoSplitterCore.dll HitCounterManager.dll Irony.dll LiveSplit.Celeste.dll LiveSplit.Core.dll LiveSplit.Cuphead.dll LiveSplit.HollowKnight.dll LiveSplit.ScriptableAutoSplit.dll SoulMemory.dll Newtonsoft.Json.dll) DO copy %PR_BASE%\%%G %PR_TARGET%

echo Copying ASLScripts:
mkdir %PR_TARGET%\ASLScripts
copy ASLScripts %PR_TARGET%\ASLScripts

7z a %PR_OUTPUT% .\%PR_TARGET%\*

echo Copying AutoSplitterCore Installer Release:
copy bin\AutoSplitterCoreInstaller\AutoSplitterCore_Installer_v1.x.0.0.msi %PR_FINAL%

echo PrepareReleaseAutoSplitter.bat END ===========