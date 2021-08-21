<?php

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

get_articles("https://www.cleverism.com/jobs/?locations=country_Greece&sorting=newest");
get_articles("https://www.cleverism.com/jobs/?locations=city_Brno%2Ccountry_Czechia&sorting=newest");



function get_articles($srcURL)
{
    global $objArr, $db ;
    
    $html =  file_get_html($srcURL);    
    
    
       foreach($html->find('.job-card') as $article) {

            $obj = new Record;
        
            //job position
            $item = $article->find('.job-card__header a', 0);
    
            //company name
            $citem = $article->find('.job-card__meta-company a', 0);

            if (!$citem)
            $citem = $article->find('.job-card__meta li.job-card__meta-company', 0);
            
            //location
            $location = $article->find('.job-card__meta li.job-card__meta-address', 0);
    
 
            if (!$item)
            {    //write it to front
                $item = "invalid href";
                $link = "http://google";
            }
            else 
                $link = $item->href;
        
            if (!$item)
                $item= "unknown";
            else 
                $item=trim($item->plaintext);
        
            if (!$citem)
                $citem = "unknown";
            else 
                $citem = trim($citem->plaintext);
        
            if (!$location)
                $location = "unknown";
            else 
                $location = " (" . trim(substr($location->plaintext, 2,strlen($location->plaintext))) . ")";
            
        
            $obj->title = $citem . ' - ' .$item . $location;
            $obj->link = ($link);
        
            $db->addRecord($obj->title, $obj->link, 3);
       }
    

}

function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception(  $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('cleverism_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}
