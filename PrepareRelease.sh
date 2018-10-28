#!/bin/sh
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

echo PrepareRelease.sh START ===========

PR_FINAL=./FinalFiles
mkdir -p "$PR_FINAL"

echo Packing Mono Portable Release:
PR_BASE=bin/ReleaseMono
PR_TARGET=$PR_FINAL/ReleaseMonoPortable
PR_OUTPUT=$PR_FINAL/HitCounterManager_Mono_Portable_v1.x.y.z.tar.gz
rm -rf "$PR_TARGET" 2>/dev/null
mkdir "$PR_TARGET"
./PostBuild.sh ./ "$PR_BASE/" "$PR_TARGET/"
pushd $PR_TARGET >/dev/null
rm -f ../archive.tar.gz 2>/dev/null
tar -czf ../archive.tar.gz *
popd >/dev/null
rm -f "$PR_OUTPUT" 2>/dev/null
mv $PR_TARGET/../archive.tar.gz $PR_OUTPUT

echo PrepareRelease.sh END ===========
