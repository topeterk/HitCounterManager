#!bash -e

#MIT License

#Copyright (c) 2022-2023 Peter Kirmeier

#Permission is hereby granted, free of charge, to any person obtaining a copy
#of this software and associated documentation files (the "Software"), to deal
#in the Software without restriction, including without limitation the rights
#to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
#copies of the Software, and to permit persons to whom the Software is
#furnished to do so, subject to the following conditions:

#The above copyright notice and this permission notice shall be included in all
#copies or substantial portions of the Software.

#THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
#IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
#FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
#AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
#LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
#OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
#SOFTWARE.

#
#  How to run:  "./GenerateThirdPartyLicense.sh > ../THIRDPARTYLICENSEREADME"
#

function grepkey {
	grep "<$1>" "$2" | sed -e "s,^.*<$1>\([^<^>]*\)</$1>,\1,"
}
function grepinline {
	grep "<$1" "$3" | sed -e "s,^.*<$1[^>]*$2=[\'\"]\([^\'^\"]*\)[\'\"].*,\1,"
}

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
License: Linkware

==============================================================================
Application Button Images

Title: -
Author: icons8.com
Source: https://icons8.com/
Licence: CC BY-ND 3.0  https://creativecommons.org/licenses/by-nd/3.0/

==============================================================================
Team Hitless Button Image

Title: -
Author: AronTheBaron
Source: Provided by the Discord user for this project
Licence: MIT

==============================================================================
Check Mark Image

Title: -
Author: OpenClipart-Vectors
Source: https://pixabay.com/p-1292787/
Licence: Pixabay License

==============================================================================
Cross Image

Title: -
Author: Clker-Free-Vector-Images
Source: https://pixabay.com/p-304374/
Licence: Pixabay License

==============================================================================
Bar Image

Modification based on Cross Image

==============================================================================
Setup Banner Image

Title: -
Author: Free-Photos
Source: https://pixabay.com/photos/bonfire-fire-man-dancing-rituals-1209269/
Licence: Pixabay License

==============================================================================
C# JSON parser

Title: Tiny Json
Author: Alex Parker
Source: https://github.com/zanders3/json
Licence: MIT

==============================================================================
Embedded License:  The MIT License (MIT)

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

URL: https://opensource.org/licenses/MIT

==============================================================================
Embedded License: Pixabay License

Images and Videos on Pixabay are made available under the Pixabay License on the following terms.Under the Pixabay License you are granted an irrevocable, worldwide, non-exclusive and royalty free right to use, download, copy, modify or adapt the Images and Videos for commercial or non-commercial purposes.Attribution of the photographer or Pixabay is not required but is always appreciated. 

The Pixabay License does not allow:
a.sale or distribution of Images or Videos as digital stock photos or as digital wallpapers;
b.sale or distribution of Images or Videos e.g. as a posters, digital prints or physical products, without adding any additional elements or otherwise adding value;
c.depiction of identifiable persons in an offensive, pornographic, obscene, immoral, defamatory or libelous way; or
d.any suggestion that there is an endorsement of products and services by depicted persons, brands, and organisations, unless permission was granted.

Please be aware that while all Images and Videos on Pixabay are free to use for commercial and non-commercial purposes, depicted items in the Images or Videos, such as identifiable people, logos, brands, etc. may be subject to additional copyrights, property rights, privacy rights, trademarks etc. and may require the consent of a third party or the license of these rights - particularly for commercial applications. Pixabay does not represent or warrant that such consents or licenses have been obtained, and expressly disclaims any liability in this respect.

URL: https://pixabay.com/de/service/license/
EOF

echo Fetching NuGet packages 1>&2

LICENSES=""
NUGET_PACKAGES_DIR=$(dotnet nuget locals -l global-packages | sed -e 's,^[^ ]* ,,' -e 's,[ \t/\\]*$,,' -e 's,\\,/,g')
NUGET_PACKAGES_SPECS=$(dotnet list $(pwd)/HitCounterManager.sln package --include-transitive --format json | grep -e '"id":\|"resolvedVersion":' | sed -e 's,.*: "\(.*\)"\,*$,\1,' | xargs -n2 -d'\n' | sed -e 's,\([^ ]*\) \(.*\),\L\1/\2/\L\1.nuspec,')

echo Printing NuGet packages 1>&2

for nuspec in ${NUGET_PACKAGES_SPECS}
do
	# (a) seems to be filler expression to whatever version is available, try detect folder using shell's star autocompletion
	xml="$(echo ${NUGET_PACKAGES_DIR}/${nuspec} | sed -e 's,/(a)/,/*/,g')"

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
		# MIT is already attached above
		continue
	elif [ "${lic}" = "https://licenses.nuget.org/Apache-2.0" ] ; then
		lic=https://www.apache.org/licenses/LICENSE-2.0.txt
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
