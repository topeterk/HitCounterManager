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
function Watchdog()
{
	setTimeout(function() { Watchdog();	}, 1500); // refresh every second
	if (heartbeat <= 1)
		heartbeat++;
	else
		iframe.src = '../HitCounter.html'; // retry reloading file in case of errors
}

// --- Periodic update

function DoUpdate(data)
{
	heartbeat = 0; // reset heartbeat, because we are alive

	// reload new files if changed..
	if (link_font_href != data.font_url) link_font.href = link_font_href = data.font_url;
	if (link_css_href  != data.css_url) link_css.href  = link_css_href  = data.css_url;

	DoVisualUpdate(data); // build graphical content

	setTimeout(function() { iframe.src = '../HitCounter.html';}, 1500); // refresh around every second
}
