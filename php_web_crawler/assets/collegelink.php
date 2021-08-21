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

get_articles("https://www.collegelink.gr/jobs/");


function get_articles($srcURL)
{
    global $objArr, $db ;
    
    $html =  file_get_html($srcURL);    
    
    
       foreach($html->find('.job_position') as $article) {

            $obj = new Record;
        
            //job position
            $item = $article->find('.dispaly_job_headline', 0);
    
            //company name
            $citem = $article->find('.dispaly_company_name', 0);
 
            //location
            $location = $article->find('.dispaly_job_location', 0);

            //link
            $link = $article->find('.job_position_container a', 0);
    
 
   
            if (!$item)
                $item= "unknown";
            else 
                $item=trim($item->plaintext);
        
            if (!$link)
            {    //write it to front
                $item = "invalid href";
                $link = "http://google";
            }
            else 
                $link = "https://www.collegelink.gr".$link->href;

            if (!$citem)
                $citem = "unknown";
            else 
                $citem = trim($citem->plaintext);
        
            if (!$location)
                $location = "unknown";
            else 
                $location = " (" . trim($location->plaintext) . ")";
            
        
            $obj->title = $citem . ' - ' .$item . $location;
            $obj->link = ($link);
        
            $db->addRecord($obj->title, $obj->link, 5);
       }
    

}


function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception(  $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('collegelink_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}
