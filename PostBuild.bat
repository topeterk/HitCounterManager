@echo off

REM MIT License

REM Copyright(c) 2016-2022 Peter Kirmeier and Ezequiel Medina

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

echo PostBuild.bat START ===========

set DIR_SRC=%1
set DIR_OUTPUT=%2
set DIR_DEST=%3

rmdir /S /Q %DIR_DEST% 2>nul
mkdir %DIR_DEST%

echo Creating portable package:
echo ^ ^ Copy files from %DIR_OUTPUT%
FOR %%G IN (HitCounterManager.exe HitCounterManager.exe.config SoulMemory.dll LiveSplit.HollowKnight.dll LiveSplit.Celeste.dll LiveSplit.Core.dll Irony.dll LiveSplit.ScriptableAutoSplit.dll LiveSplit.Cuphead.dll) DO copy %DIR_OUTPUT%\%%G %DIR_DEST%
echo ^ ^ Copy files from %DIR_SRC%
FOR %%G IN (Sources\HitCounterManagerInit.xml Sources\HitCounter.html Sources\HitCounter.template) DO copy %DIR_SRC%\%%G %DIR_DEST%
echo ^ ^ Copy files from %DIR_SRC%\ASLScripts
mkdir %DIR_DEST%\ASLScripts
copy %DIR_SRC%\ASLScripts %DIR_DEST%\ASLScripts
echo ^ ^ Copy files from %DIR_SRC%\Designs
mkdir %DIR_DEST%\Designs
copy %DIR_SRC%\Designs %DIR_DEST%\Designs

echo PostBuild.bat END ===========
