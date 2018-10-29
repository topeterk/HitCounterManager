#!/bin/bash
#
# MIT License
#
# Copyright(c) 2018-2018 Peter Kirmeier
#
# Permission Is hereby granted, free Of charge, to any person obtaining a copy
# of this software And associated documentation files (the "Software"), to deal
# in the Software without restriction, including without limitation the rights
# to use, copy, modify, merge, publish, distribute, sublicense, And/Or sell
# copies of the Software, And to permit persons to whom the Software Is
# furnished to do so, subject to the following conditions:
#
# The above copyright notice And this permission notice shall be included In all
# copies Or substantial portions of the Software.
#
# THE SOFTWARE Is PROVIDED "AS IS", WITHOUT WARRANTY Of ANY KIND, EXPRESS Or
# IMPLIED, INCLUDING BUT Not LIMITED To THE WARRANTIES Of MERCHANTABILITY,
# FITNESS FOR A PARTICULAR PURPOSE And NONINFRINGEMENT. IN NO EVENT SHALL THE
# AUTHORS Or COPYRIGHT HOLDERS BE LIABLE For ANY CLAIM, DAMAGES Or OTHER
# LIABILITY, WHETHER In AN ACTION Of CONTRACT, TORT Or OTHERWISE, ARISING FROM,
# OUT OF Or IN CONNECTION WITH THE SOFTWARE Or THE USE Or OTHER DEALINGS IN THE
# SOFTWARE.

echo PostBuild.sh START ===========

DIR_SRC=$1
DIR_OUTPUT=$2
DIR_DEST=$3

rm -rf $DIR_DEST 2>/dev/null
mkdir $DIR_DEST
echo Creating portable package:
echo "  Copy files from Output"
cp $DIR_OUTPUT/{HitCounterManager.exe,HitCounterManager.exe.config} $DIR_DEST
echo "  Copy files from Sources"
cp $DIR_SRC/Sources/{HitCounterManagerInit.xml,HitCounter.html,HitCounter.template,HitCounterManagerMono.sh} $DIR_DEST
echo "  Copy files from Designs"
mkdir $DIR_DEST/Designs
cp -r $DIR_SRC/Designs $DIR_DEST
echo "  Applying file permissions"
chmod 644 $DIR_DEST/*
chmod 666 $DIR_DEST/HitCounter.html
chmod 755 $DIR_DEST/HitCounterManagerMono.sh $DIR_DEST/HitCounterManager.exe
chmod 755 $DIR_DEST/Designs
chmod 644 $DIR_DEST/Designs/*

echo PostBuild.sh END ===========
