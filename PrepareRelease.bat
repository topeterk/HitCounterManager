@echo off

REM MIT License

REM Copyright(c) 2018-2018 Peter Kirmeier

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

echo PrepareRelease.bat START ===========

REM To run this script you need to have 7zip installed at the default path.

set PATH=%PATH%;C:\Program Files\7-Zip
set DIR_PACKAGE=PortablePackage
set DIR_FINAL=FinalFiles

rmdir /S /Q %DIR_FINAL% 2>nul
mkdir %DIR_FINAL%

echo Packing Windows Portable Release:
mkdir %DIR_FINAL%\ReleaseWinPortable\
call PostBuild.bat .\ bin\ReleaseWin\ %DIR_FINAL%\ReleaseWinPortable\
7z a %DIR_FINAL%\HitCounterManager_Win_Portable_v1.x.y.z.zip .\%DIR_FINAL%\ReleaseWinPortable\*

echo Packing Mono Portable Release:
mkdir %DIR_FINAL%\ReleaseMonoPortable\
call PostBuild.bat .\ bin\ReleaseMono\ %DIR_FINAL%\ReleaseMonoPortable\
7z a %DIR_FINAL%\HitCounterManager_Mono_Portable_v1.x.y.z.zip .\%DIR_FINAL%\ReleaseMonoPortable\*

echo Packing Windows Setup Release:
copy /B Setup\Setup\Express\SingleImage\DiskImages\DISK1\setup.exe %DIR_FINAL%\HitCounterManager_Win_Setup_v1.x.y.z.exe

echo PrepareRelease.bat END ===========
