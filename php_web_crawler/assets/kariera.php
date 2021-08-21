<?php

date_default_timezone_set("Europe/Athens");

$h = intval(date('H'));

if ($h > -1 && $h < 8)
	return;

$minute = intval(date('i')); //get minute

if ($minute > 20 && $minute < 40)
	return;

set_time_limit(120);

set_error_handler( "log_error" );
set_exception_handler( "log_exception" );

require_once ('../general.php');

require_once("simple_html_dom.php");

class Record
{
    public $title;
    public $link;
}

//connect to database
$db = new dbase();
$db->connect();

//C#
get_articles("https://www.kariera.gr/%CE%B8%CE%AD%CF%83%CE%B5%CE%B9%CF%82-%CE%B5%CF%81%CE%B3%CE%B1%CF%83%CE%AF%CE%B1%CF%82?utf8=%E2%9C%93&q=c%23&loc=%CE%91%CF%84%CF%84%CE%B9%CE%BA%CE%AE-%CE%91%CE%B8%CE%AE%CE%BD%CE%B1&posted=3&filter_category=jn008");

//JS
get_articles("https://www.kariera.gr/%CE%B8%CE%AD%CF%83%CE%B5%CE%B9%CF%82-%CE%B5%CF%81%CE%B3%CE%B1%CF%83%CE%AF%CE%B1%CF%82?utf8=%E2%9C%93&q=javascript&loc=%CE%91%CF%84%CF%84%CE%B9%CE%BA%CE%AE-%CE%91%CE%B8%CE%AE%CE%BD%CE%B1&posted=3");

//php
get_articles("https://www.kariera.gr/%CE%B8%CE%AD%CF%83%CE%B5%CE%B9%CF%82-%CE%B5%CF%81%CE%B3%CE%B1%CF%83%CE%AF%CE%B1%CF%82?utf8=%E2%9C%93&q=php&loc=%CE%91%CF%84%CF%84%CE%B9%CE%BA%CE%AE-%CE%91%CE%B8%CE%AE%CE%BD%CE%B1&posted=3");




function get_articles($srcURL)
{
    global $objArr, $db ;

    $html =  file_get_html($srcURL);
    
   foreach($html->find('div.job') as $article) {

        $obj = new Record;

        $item = $article->find('a.job-title', 0);
        $company = $article->find('div.snapshot-item a', 0);

        if ($item == null )
            continue;

        if ($company!=null)
            $company = "(" . $company->plaintext . ") ";
        else 
            $company = "";

        $obj->title = $company . trim($item->plaintext);
        $obj->link = 'https://www.kariera.gr' . trim($item->href);

        $db->addRecord($obj->title, $obj->link, 8);
   }
}


function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('kariera_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}