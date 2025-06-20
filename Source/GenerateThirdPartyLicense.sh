#!bash -e

# SPDX-FileCopyrightText: © 2022-2025 Peter Kirmeier
# SPDX-License-Identifier: MIT

#
#  How to run:  "./GenerateThirdPartyLicense.sh > ../THIRDPARTYLICENSEREADME"
#

function grepkey {
	grep "<$1>" "$2" | sed -e "s,^.*<$1>\([^<^>]*\)</$1>,\1,"
}
function grepinline {
	grep "<$1" "$3" | sed -e "s,^.*<$1[^>]*$2=[\'\"]\([^\'^\"]*\)[\'\"].*,\1,"
}

export DOTNET_CLI_TELEMETRY_OPTOUT=TRUE

echo Printing Non-NuGet info 1>&2

cat <<EOF
Licence information:

The software aims to only use licenses that allow commercial and non-commercial use.
Moreover it aims to use the whole software without any charge.
Therefore it should be free for everyone to use it without any costs.
DISCLAIMER: Above notice comes without warranty, so please refer to the explicit licenses below.

==============================================================================
Application

Title: HitCounterManager
Author: Peter Kirmeier
Source: https://github.com/topeterk/HitCounterManager
Licence: MIT

==============================================================================
Application Icon

Title: Pioneer Camp
Author: Iconka  http://www.iconka.com
Source: https://iconka.com/en/269/
License: Iconka-Linkware

==============================================================================
Icon image of info button ("i")

Title: Info Solid SVG Vector
Author: Icons8
Source: https://www.svgrepo.com/svg/314210/info-solid
Licence: MIT

==============================================================================
Icon image of dark mode button (Flashlight)

Title: Flashlight SVG Vector 14
Author: SVG Repo
Source: https://www.svgrepo.com/svg/64840/flashlight
Licence: CC0-1.0

==============================================================================
Icon image of SP button (24 Hours Circle)

Title: 24 Hours Service SVG Vector 3
Author: SVG Repo
Source: https://www.svgrepo.com/svg/116010/24-hours-service
Licence: CC0-1.0

==============================================================================
Icon image of inactive button (Dashed Circle)

Title: Circle Dashed SVG Vector
Author: jtblabs
Source: https://www.svgrepo.com/svg/473116/circle-dashed
Licence: MIT

==============================================================================
Icon image of update button (Cloud)

Title: Network Public SVG Vector
Author: Carbon Design
Source: https://www.svgrepo.com/svg/340717/network-public
Licence: Apache-2.0

==============================================================================
Icon image of reset button (Double Arrows)

Title: Repeat One SVG Vector
Author: Remix Design
Source: https://www.svgrepo.com/svg/347343/repeat-one
Licence: Apache-2.0

==============================================================================
Icon image of PB button (Trophy)

Title: Trophy SVG Vector
Author: Neuicons
Source: https://www.svgrepo.com/svg/487929/trophy
Licence: MIT

==============================================================================
Icon image of timer ON button (Clock In Motion)

Title: Time Fast SVG Vector
Author: IonutNeagu
Source: https://www.svgrepo.com/svg/490407/time-fast
Licence: CC0-1.0

==============================================================================
Icon image of timer OFF button (Sleeping Z)

Title: Snooze SVG Vector 
Author: Microsoft
Source: https://www.svgrepo.com/svg/311218/snooze
Licence: MIT

==============================================================================
Icon image of hit button (Sword In Hand)

Title: Sword Brandish SVG Vector
Author: game-icons.net
Source: https://www.svgrepo.com/svg/323419/sword-brandish
Licence: CC-BY-4.0

==============================================================================
Icon image of way hit button (Stumbling Man)

Title: Falling SVG Vector 
Author: SVG Repo
Source: https://www.svgrepo.com/svg/74979/falling
Licence: CC0-1.0

==============================================================================
Icon image of split button (Man On Stairs)

Title: Man Climbing Stairs SVG Vector 
Author: SVG Repo
Source: https://www.svgrepo.com/svg/113316/man-climbing-stairs
Licence: CC0-1.0

==============================================================================
AutoSplitter Button Image

Title: icon-autosplitter-32
Author: Neimex23
Licence: MIT

==============================================================================
Icon image of Team Hitless button

Title: team_hitless_logo.svg
Author: Team Hitless
Source: https://discord.com/channels/689574638396899342/1084446670357540876/1350973621085933598
Licence: TeamHitless

==============================================================================
Icon images of remaining minor buttons

Title: -
Author(s): Avalonia Team
Source: https://avaloniaui.github.io/icons.html
License: MIT

==============================================================================
Design image showing a check mark

Title: -
Author: OpenClipart-Vectors
Source: https://pixabay.com/p-1292787/
Licence: Pixabay

==============================================================================
Design image showing a cross

Title: -
Author: Clker-Free-Vector-Images
Source: https://pixabay.com/p-304374/
Licence: Pixabay

==============================================================================
Design image showing a bar

Modification based on Design image showing a cross

==============================================================================
Design image showing a flag pole and Design image showing a 24 in a circle

Title: -
Author: icons8.com
Source: https://icons8.com/
Licence: CC-BY-ND-3.0

==============================================================================
Setup Banner Image

Title: -
Author: Free-Photos
Source: https://pixabay.com/photos/bonfire-fire-man-dancing-rituals-1209269/
Licence: Pixabay

==============================================================================
C# JSON parser

Title: Tiny Json
Author: Alex Parker
Source: https://github.com/zanders3/json
Licence: MIT

==============================================================================
Embedded License: MIT

EOF
# https://opensource.org/licenses/MIT
cat LICENSES/MIT.txt
cat <<EOF

==============================================================================
Embedded License: CC0-1.0

EOF
# https://creativecommons.org/publicdomain/zero/1.0/
cat LICENSES/CC0-1.0.txt
cat <<EOF

==============================================================================
Embedded License: CC-BY-4.0

EOF
# https://creativecommons.org/licenses/by/4.0/
cat LICENSES/CC-BY-4.0.txt
cat <<EOF

==============================================================================
Embedded License: CC-BY-ND-3.0

EOF
# https://creativecommons.org/licenses/by-nd/3.0/
cat LICENSES/CC-BY-ND-3.0.txt
cat <<EOF

==============================================================================
Embedded License: TeamHitless

EOF
cat LICENSES/LicenseRef-TeamHitless.txt
cat <<EOF

==============================================================================
Embedded License: Pixabay

EOF
# https://pixabay.com/service/license/
cat LICENSES/LicenseRef-Pixabay.txt
cat <<EOF

==============================================================================
Embedded License: Iconka-Linkware

EOF
cat LICENSES/LicenseRef-Iconka-Linkware.txt
cat <<EOF

==============================================================================
Embedded License: Apache-2.0

EOF
# https://www.apache.org/licenses/LICENSE-2.0.txt
cat LICENSES/Apache-2.0.txt
echo ""

echo Fetching NuGet packages 1>&2

LICENSES=""
NUGET_PACKAGES_DIR=$(dotnet nuget locals -l global-packages | sed -e 's,^[^ ]* ,,' -e 's,[ \t/\\]*$,,' -e 's,\\,/,g')
NUGET_PACKAGES_SPECS=$(dotnet list $(pwd)/HitCounterManager.sln package --include-transitive --format json | grep -e '"id":\|"resolvedVersion":' | sed -e 's,.*: "\(.*\)"\,*$,\1,' | xargs -n2 -d'\n' | sed -e 's,\([^ ]*\) \(.*\),\L\1/\2/\L\1.nuspec,' | sort -u)

echo Printing NuGet packages 1>&2

for nuspec in ${NUGET_PACKAGES_SPECS}
do
	xml="${NUGET_PACKAGES_DIR}/${nuspec}"

	id=$(grepkey id "${xml}")
	echo ==============================================================================
	echo NuGet Package: ${id} $(grepkey version "${xml}")
	echo ""
	echo Author\(s\): $(grepkey authors "${xml}")
	if [ "${id}" = "Avalonia.Angle.Windows.Natives" ] ; then
		# https://github.com/AvaloniaUI/Avalonia/discussions/6122
		echo Source: $(grepinline repository url "${xml}")
		lic=https://raw.githubusercontent.com/AvaloniaUI/angle/master/LICENSE
	elif [ "${id}" = "Avalonia.BuildServices" ] ; then
		# https://github.com/AvaloniaUI/Avalonia/issues/11984
		echo Source: https://avaloniaui.net/
		lic=https://licenses.nuget.org/MIT
	elif [ "${id}" = "Tmds.DBus" ] ; then
		echo Source: $(grepinline repository url "${xml}")
		lic=$(grepkey licenseUrl "${xml}")
	else
		echo Source: $(grepkey projectUrl "${xml}")
		lic=$(grepkey licenseUrl "${xml}")
	fi
	GITHUB_RAWURL=$(echo ${lic} | sed -n 's,https://github.com/\(.*\)/blob/\(.*\),https://raw.githubusercontent.com/\1/\2,p')
	if [ ! -z ${GITHUB_RAWURL} ] ; then
		lic=${GITHUB_RAWURL}
	fi
	# Split dual licences
	if [ "${lic}" = "https://licenses.nuget.org/MIT%20AND%20Apache-2.0" ] ; then
		lic="https://licenses.nuget.org/MIT https://licenses.nuget.org/Apache-2.0"
	fi
	for lic in ${lic}
	do
		echo License: ${lic}
		LICENSES="${LICENSES} ${lic}"
	done
done

echo Printing NuGet licenses 1>&2

for lic in $(echo $LICENSES | xargs -n 1 echo | sort -u)
do
	if [ "${lic}" = "https://licenses.nuget.org/MIT" ] ; then
		# Already attached above
		continue
	elif [ "${lic}" = "https://licenses.nuget.org/Apache-2.0" ] ; then
		# Already attached above
		continue
	elif [ ! -z $(echo ${lic} | sed -n 's,^https://go.microsoft.com/fwlink/.*,true,p') ] ; then
		MSSHORT_URL=$(curl -sL ${lic} | sed -n 's,.*"rawBlobUrl"[ \t]*:[ \t]*"\([^"]*\)".*,\1,p')
		if [ ! -z ${MSSHORT_URL} ] ; then
			lic=${MSSHORT_URL}
		fi
	fi
	echo ==============================================================================
	echo Embedded License: ${lic}
	echo ""
	curl -sL ${lic}
	echo ""
done

echo Finished 1>&2
