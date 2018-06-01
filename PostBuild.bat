@echo off

echo PostBuild.bat START ===========

set DIR_SRC=%1
set DIR_OUTPUT=%2
set DIR_DEST=%3

rmdir /S /Q %DIR_DEST% 2>nul
mkdir %DIR_DEST%

echo Creating portable package:
echo ^ ^ Copy files from %DIR_OUTPUT%
FOR %%G IN (HitCounterManager.exe HitCounterManager.exe.config) DO copy %DIR_OUTPUT%\%%G %DIR_DEST%
echo ^ ^ Copy files from %DIR_SRC%
FOR %%G IN (HitCounterManagerInit.xml HitCounter.html HitCounter.template) DO copy %DIR_SRC%\%%G %DIR_DEST%
echo ^ ^ Copy files from %DIR_SRC%\Designs
mkdir %DIR_DEST%\Designs
copy %DIR_SRC%\Designs %DIR_DEST%\Designs

echo PostBuild.bat END ===========
