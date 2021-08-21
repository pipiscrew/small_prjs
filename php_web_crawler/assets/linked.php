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

$objArr = array();
//CZ
//brno - get_articles("https://www.linkedin.com/jobs/search/?location=Brno&redirect=false&position=1&pageNum=0&f_TP=1");
get_articles("https://www.linkedin.com/jobs/search/?keywords=develop&location=Czech%20Republic&redirect=false&position=1&pageNum=0&f_TP=1");
get_articles("https://www.linkedin.com/jobs/search/?location=Czech%20Republic&redirect=false&position=1&pageNum=0&f_TP=1");



//GR
get_articles("https://www.linkedin.com/jobs/search/?keywords=develop&location=Greece&redirect=false&position=1&pageNum=0&f_TP=1");
get_articles("https://www.linkedin.com/jobs/search/?location=Greece&redirect=false&position=1&pageNum=0&f_TP=1");



function get_articles($srcURL)
{
    global $objArr, $db ;

    $html_source =  getSslPage($srcURL);

    $html = str_get_html($html_source);

   foreach($html->find('li.result-card') as $article) {

        $obj = new Record;

        //job position
        $item = $article->find('h3.job-result-card__title', 0);

        //company name
        $citem = $article->find('a.job-result-card__subtitle-link', 0);
        
        //location
        $location = $article->find('span.job-result-card__location', 0);

        //link
        $link = $article->find('a.result-card__full-card-link', 0);
        

        if (!$item)
            $item= "unknown";
        else 
            $item=trim($item->plaintext);

        if (!$citem) {
			$citem = $article->find('h4.job-result-card__subtitle', 0);
			
			if (!$citem) {
				$citem = "unknown";
			}
			else 
				$citem = trim($citem->plaintext);
		}            
        else 
            $citem = trim($citem->plaintext);

        if (!$location)
            $location = "unknown";
        else 
            $location = " (" . $location->plaintext . ")";
        
        if (!$link)
        {    //write it to front
            $citem = "invalid href";
            $link = "http://google";
        }
        else 
           $link = $link->href;
        

        $obj->title = $citem . ' - ' .$item . $location;
        $obj->link = cut_url($link);

        if (array_in_string($obj->link, $objArr)) {
            $obj->title = $obj->title . " **this is double";
            return;
        }
		
        $db->addRecord($obj->title, $obj->link, 9);
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
    
function cut_url($url){
    if ($url != null) {

        $position = stripos($url, "?");  

        if ($position == true){
            return substr($url, 0,$position);
        }

        return $url;
    }
}

function array_in_string($link, array $arr) {
    foreach($arr as $arr_value) { 
       if ($arr_value->link == $link) return true;        
    }
    return false;
}



function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('linked_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}