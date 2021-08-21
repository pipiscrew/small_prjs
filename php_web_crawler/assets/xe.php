<?php

/*error_reporting(E_ALL);
ini_set('display_errors', '1');*/

/*date_default_timezone_set("Europe/Athens");

$h = intval(date('H'));

if ($h > -1 && $h < 8)
	return;


$minute = intval(date('i')); //get minute

if ($minute > 20 && $minute < 40)
	return;
*/

//set_time_limit(120);

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

get_articles("https://www.xe.gr/property/search?Geo.area_id_new__hierarchy=82431&Geo.area_id_new__hierarchy=82448&Geo.area_id_new__hierarchy=82444&System.item_type=re_residence&Transaction.price.to=400&Transaction.type_channel=117541&sort_by=Publication.effective_date_start&sort_direction=desc");


function get_articles($srcURL)
{
    global $objArr, $db ;

    $html_source  =  getSslPage($srcURL);

    $html = str_get_html($html_source);

    foreach($html->find('div.resultItems>article') as $article) {
        $obj = new Record;
        
        $title = $article->find('div > h1 > span', 0);
        $subtitle = $article->find('div > div > span', 0);
        $location =  $article->find('div.articleInfo > a', 0);
       
        $link = $article->href;

        $offer = $article->find('div.deal-list-price', 0);

        if ($title!=null) {
            $title = trim($title->plaintext);
        } 
        else
            continue;

        if ($article->href!=null) {
            $link = 'https://www.xe.gr'.$article->href;
        } 
        else
            continue;

        if ($subtitle!=null) {
            $subtitle = trim($subtitle->plaintext);
        }
        else 
            $subtitle = '';

        if ($location!=null) {
            $location = trim($location->plaintext);
        }
        else 
            $location = '';


        $obj->title = "$title $subtitle ($location)";
		$obj->link = $link;

        $db->addRecord($obj->title, $obj->link, 16);
    }
}

function getSslPage($url) {
      
        //$url = 'https://www.xe.gr/property/search?Geo.area_id_new__hierarchy=82431&System.item_type=re_residence&Transaction.price.to=400&Transaction.type_channel=117541&sort_by=Publication.effective_date_start&sort_direction=desc';
        $userAgent = 'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31';
        $ch = curl_init();
        

/*        curl_setopt($ch, CURLOPT_STDERR, fopen("xe_debug.txt", "w+"));
        curl_setopt($ch, CURLOPT_VERBOSE, 1); */
        
        
        curl_setopt($ch, CURLOPT_URL, $url);
        
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
        
        // Blindly accept the certificate
        curl_setopt($ch, CURLOPT_SSL_VERIFYPEER, false);
        curl_setopt($ch, CURLOPT_SSL_VERIFYHOST, false);
        curl_setopt($ch, CURLOPT_USERAGENT, $userAgent);
        // decode response
        curl_setopt($ch, CURLOPT_ENCODING, true);
        $response = curl_exec($ch);
        curl_close($ch);
    
        return $response;
}

function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('xe_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    //header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}