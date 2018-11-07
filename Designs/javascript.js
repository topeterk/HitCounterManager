// --- Initialization

var link_font = document.createElement('link');
var link_font_href = '';
var link_css = document.createElement('link');
var link_css_href = 'stylesheet.css';
var iframe;

link_font.rel  = 'stylesheet';
link_font.href = link_font_href;
document.getElementsByTagName('head')[0].appendChild(link_font);
link_css.rel  = 'stylesheet';
link_css.href = link_css_href;
document.getElementsByTagName('head')[0].appendChild(link_css);

function Start()
{
	iframe = document.getElementsByTagName('iframe')[0];
	iframe.src="../HitCounter.html";

	Watchdog(); // Starts actual eternal loop
}

// --- Helper functions

function IntToStringSigned(i) { if (i > 0) return "+" + i; else return i; }

function ShowCrossOrCheckMark(i) { return '<img src="' + (i > 0 ? 'img_cross.png" height="15px"' : 'img_check.png" height="21px"') + '>'; }

function ShowSessionProgress() { return '<img src="img_star.png" height="21px">'; }

// --- Watchdog

var heartbeat = 0;
var init_done = false;
function Watchdog()
{
	setTimeout(function() { Watchdog();	}, 1500); // refresh every second
	if (heartbeat <= 1)
		heartbeat++;
	else
	{
		if (!init_done) // reading the data file never worked before?
		{
			ShowHelpText('The browser or broadcasting software cannot read the hit counter data file.<br/>' +
			'Please try one of the following:<br/>&nbsp;<br>' +
			'- <u>SLOBS</u>: Make sure "local file" is check at your browser source<br/>' +
			'- <u>OBS Studio</u>: Make sure the URL of the browser source looks like this: <b>http://absolute/</b>C:/MyHitCounter/Designs/HitCounterNumeric.html<br/>' +
			'- <u>Chrome</u>: Make sure to start the browser with command line option <b>--allow-file-access-from-files</b><br/>' +
			'- <u>Others or not working?</u>: Please disable cross domain protection as reading local files don\'t have a "domain",' +
			'so the data file is treated being hosted on another domain which does not allow reading the file due to security reasons by most browsers\'  default.' +
			'Please look at the online readme on github for the latest instructions that may already contain additional instructions.<br/>');
		}
		iframe.src = '../HitCounter.html'; // retry reloading file in case of errors
	}
}

// --- Periodic update

function DoUpdate(data)
{
	heartbeat = 0; // reset heartbeat, because we are alive
	init_done = true; // the data file could be loaded successfully

	// reload new files if changed..
	if (link_font_href != data.font_url) link_font.href = link_font_href = data.font_url;
	if (link_css_href  != data.css_url) link_css.href  = link_css_href  = data.css_url;

	DoVisualUpdate(data); // build graphical content

	setTimeout(function() { iframe.src = '../HitCounter.html';}, 1500); // refresh around every second
}
