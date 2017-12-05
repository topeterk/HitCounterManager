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
FOR %%G IN (img_check.png img_cross.png HitCounterManagerSave.xml HitCounter.html HitCounter.template HitCounterNoDeath.html HitCounterNoDeathBlack.html HitCounterNoDeathPink.html HitCounterNumeric.html HitCounterNumericBlack.html HitCounterNumericPink.html HitCounterYesNo.html HitCounterYesNoBlack.html HitCounterYesNoPink.html) DO copy %DIR_SRC%\%%G %DIR_DEST%

echo PostBuild.bat END ===========
