<?php
set_time_limit(0);

set_error_handler( "log_error" );
set_exception_handler( "log_exception" );

require_once("general.php");

$html_header =  <<<EOT
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />

<link rel='stylesheet' href='bootstrap.min.css' type='text/css'/>
<title>Wonderboy - Job List</title>
</head>
EOT;


$rss=false;

if (isset($_GET['rss']))
	$rss=true;

//construct date between
$to = date("Y-m-d 23:59");
$from = date("Y-m-d 00:00", strtotime($to."-2 days"));

//connect to database
$db = new dbase();
$db->connect();

if (isset($_GET['id'])) {
/*
https://domain.com/jobs/?id=15
or
https://domain.com/jobs/?id=15&rss=1
*/
	$feed_id = intval($_GET['id']);

	$records = $db->getSet("select title,link,guid,daterec,feeds.site from feed_items
						left join feeds on feeds.id = feed_items.site_id
						where feed_items.site_id=? and daterec between '$from' and '$to'
						order by daterec desc", array($feed_id));
}
//search by keyword
else if (isset($_GET['s'])) {
/*
enables :
https://domain.com/jobs/?rss=1&s=remote
or
https://domain.com/jobs/?s=remote
*/

	$keyword = '%'.trim($_GET['s']).'%';
	$keyword2 = '';

	if ($keyword=="remote")
		$keyword2 = '%no office%';

	$records = $db->getSet("select title,link,guid,daterec,feeds.site from feed_items
						left join feeds on feeds.id = feed_items.site_id
						where feeds.isvisible=1 and daterec between '$from' and '$to' and (title like ? or title like ?)
						order by daterec desc", array($keyword, $keyword2));
} else {
	$records = $db->getSet("select title,link,guid,daterec,feeds.site from feed_items
						left join feeds on feeds.id = feed_items.site_id
						where feeds.isvisible=1 and daterec between '$from' and '$to'
						order by daterec desc", null);
}

if (!$rss) {

	header("Expires: Tue, 03 Jul 2001 06:00:00 GMT");
	header("Last-Modified: " . gmdate("D, d M Y H:i:s") . " GMT");
	header("Cache-Control: no-store, no-cache, must-revalidate, max-age=0");
	header("Cache-Control: post-check=0, pre-check=0", false);
	header("Pragma: no-cache");

	//signalize today entries with yellow bg
	$array_keywords = ['infosys', 'developer','c#', '.net', 'php', 'greek', 'javascript', 'js', 'typescript', 'angular', 'support', 'specialist', 'remote', 'no office', 'l2', 'l3'];

	echo $html_header;

	foreach ($records as $ad) {

		$title = $ad['title'];
		$link = $ad['link'];

		if (array_in_string($title, $array_keywords)) {
			$bg = 'background-color: yellow';
		} else {
			 $bg="";}

		$template =  <<<EOT
<div class="col-sm-12">
<div class="card">

<div class="card-body">
<h5 class="card-title" style="$bg"><span style="margin-right:10px;color:red;">0</span><a href="http://nullrefer.com/?$link" target="_blank">$title</a></h5>

</div>
</div>
</div>
EOT;

		echo $template;
	}
} else {
	//https://www.carronmedia.com/create-an-rss-feed-with-php/
	header("Content-Type: application/rss+xml; charset=UTF-8");

	//RSS
	//https://stackoverflow.com/a/6768831
	//$actual_link = (isset($_SERVER['HTTPS']) && $_SERVER['HTTPS'] === 'on' ? "https" : "http") . "://$_SERVER[HTTP_HOST]$_SERVER[REQUEST_URI]";
	echo "<?xml version=\"1.0\" encoding=\"UTF-8\" ?><rss version='2.0' xmlns:atom='http://www.w3.org/2005/Atom'>\n";
	echo "<channel>\n";
	//echo "\t<atom:link href='$actual_link' rel='self' type='application/rss+xml'/>\n";

	echo "<title>Wonderboy RSS Feed</title>\n";
	echo "<description>Wonderboy - Job List</description>\n";
	echo "<link>http://www.na0mi.com</link>\n";
	//RSS


	foreach ($records as $ad) {
		$title = $ad['title'];
		$link = unescape4xml($ad['link']);
		$guid = $ad['guid'];
		$dt = DateTime::createFromFormat('Y-m-d H:i:s', $ad['daterec'])->format(DateTime::RSS);
		$site = $ad['site'];

		$template =  <<<EOT
<item>
<title>$title</title>
<description></description>
<pubDate>$dt</pubDate>
<link>$link</link>
<guid>$guid</guid>
<dc:creator>$site</dc:creator>
</item>
EOT;

		echo $template;

	}


	echo "</channel>\n";
	echo "</rss>\n";
}


function unescape4xml($s)
{
	return str_replace(array('&','>','<','"', '\''), array('&amp;','&gt;','&lt;','&quot;', '&apos;'), $s);
}


function array_in_string($str, array $arr)
{
	foreach ($arr as $arr_value) {
		 //start looping the array
		if (stripos($str,$arr_value) !== false)
			return true; //if $arr_value is found in $str return true
	}
	return false; //else return false
}

function log_error( $num, $str, $file, $line, $context = null )
{
	log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( $e )
{

	$message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
	file_put_contents('index_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

	header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
	exit(1);
}