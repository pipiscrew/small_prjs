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

get_articles("https://job-board-v3.workable.com/api/v1/jobs?query=&location=brno&orderBy=relevance+desc");
get_articles("https://job-board-v3.workable.com/api/v1/jobs?query=&location=greece&orderBy=relevance+desc");



function get_articles($srcURL)
{
    global $objArr, $db ;

    $html =  file_get_html($srcURL);

    $json = json_decode($html, true);
	
    foreach($json['jobs'] as $article) {
        $obj = new Record;
        
        if (isset($article['company']['title']))
            $company = $article['company']['title'];
        else 
            $company = "unknown company";
        
        if (isset($article['title'], $article['department']))
            $position = $article['title'] . ' (' . $article['department'] . ')';
        else if (isset($article['title']))
        {
            $position = $article['title'];
        }
        else 
            $position = "unknown position";

        if (isset($article['locations']))
            if (sizeof($article['locations']) > 0)
                $location = $article['locations'][0];
            else 
                $position = "unknown location";
        else 
            $position = "unknown location";

        if (isset($article['url'])){
            $obj->link = $article['url'];
        }
        else 
            $position .= " unknown url";

        $obj->title = "$company - $position - $location";
		
        $db->addRecord($obj->title, $obj->link, 14);
    }

}



function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('workable_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}