<?php
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

get_articles('https://www.startupjobs.cz/api/nabidky?location[]=ChIJi3lwCZyTC0cRkEAWZg-vAAQ&location[]=ChIJEVE_wDqUEkcRsLEUZg-vAAQ&page=1');

//brno only
// get_articles("https://www.startupjobs.cz/api/nabidky?location[]=ChIJEVE_wDqUEkcRsLEUZg-vAAQ&page=1");

function get_articles($srcURL)
{
    global $objArr, $db ;

    $html =  file_get_html($srcURL);

    $json = json_decode($html, true);
	
    foreach($json['resultSet'] as $article) {
		
        $obj = new Record;
        
        if (isset($article['name']))
            $position = $article['name'];
        else 
            $position = "unknown position";
		
        if (isset($article['companyName']))
            $company = $article['companyName'];
        else 
            $company = "unknown company";

        if (isset($article['locations']))
            if (sizeof($article['locations']) > 0)
                $location = $article['locations'][0];
            else 
                $position = "unknown location";
        else 
            $position = "unknown location";

        if (isset($article['id'])){
            $obj->link = 'https://www.startupjobs.cz/nabidka/'.$article['id'];
        }
        else 
            $position .= " unknown url";

        $obj->title = "$company - $position - $location";
		
        $db->addRecord($obj->title, $obj->link, 13);
    }

}


function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('startupjobs_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}