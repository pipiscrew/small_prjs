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

require_once('../general.php');

require_once("simple_html_dom.php");

class Record
{
    public $title;
    public $link;
}

//connect to database
$db = new dbase();
$db->connect();

get_articles("https://www.ifarmakeia.gr/%CF%80%CF%81%CE%BF%CF%83%CF%86%CE%BF%CF%81%CE%AD%CF%82-%CF%80%CF%81%CE%BF%CF%8A%CF%8C%CE%BD%CF%84%CF%89%CE%BD/");


function get_articles($srcURL)
{
    global $objArr, $db ;

    $html =  file_get_html($srcURL);

    foreach($html->find('ul.large-block-grid-3>li') as $article) {
        $obj = new Record;
        
        $product = $article->find('div.deal-list-title>a', 0);
        $title = $product->plaintext;
        $link = $product->href;

        $offer = $article->find('div.deal-list-price', 0);

        if ($offer!=null)
            $offer = '('.trim($offer->plaintext).') ';
        else 
            $offer = '';

        $location = $article->find('div.deal-list-location', 0)->plaintext;

        $obj->title = "$title $offer- $location";
		$obj->link = $link;
		
        $db->addRecord($obj->title, $obj->link, 15);
    }

}



function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('workable_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}