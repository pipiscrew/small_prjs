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

get_articles("https://neuvoo.cz/pr%C3%A1ce/?k=&l=Brno%2C+Jihomoravsk%C3%BD&p=1&date=1d&field=&company=&source_type=&radius=&from=&test=&iam=&is_category=no");




function get_articles($srcURL)
{
    global $objArr, $db ;

    $html_source =  getSslPage($srcURL);

    $html = str_get_html($html_source);

   foreach($html->find('.card.card__job') as $article) {
    
    
        if (!$article)
            return;

        $obj = new Record;
    
        //job position
        $item = $article->find('.card__job-title a', 0);

        //company name
        $citem = $article->find('.card__job-empname-label', 0);
        
        //location
        $location = 'Brno';
    
        if (!$item)
        {    //write it to front
            $citem = "invalid href";
            $link = "http://google";
        }
        else 
            $link = 'https://neuvoo.cz'.$item->href;
    
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
            $location = " (" . trim($location) . ")";
        
    
        $obj->title = $citem . ' - ' .$item . $location;
//        $obj->link = ($link);
		$obj->link = cut_unwanted_vars($link); //remove jpos vars

		$db->addRecord($obj->title, $obj->link, 10);
   }

}

function cut_unwanted_vars($url)
{
	$position = strpos($url, 'neuvoo.cz');

	if ($position == true) {

		$parts = parse_url($url);
		parse_str($parts['query'], $query);
		return 'https://neuvoo.cz/job.php?id='.$query['id'];
		
	} else {
		return $url;
	}
}

function getSslPage($url) {
    
            $userAgent = 'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31';
            $ch = curl_init();
            /*
	        curl_setopt($ch, CURLOPT_STDERR, fopen("linked_debug.txt", "w+"));
	        curl_setopt($ch, CURLOPT_VERBOSE, 1); 
        */
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

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('neuvoo_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}