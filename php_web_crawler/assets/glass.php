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

//days before
$d = 2;

//cz
get_jobs("https://www.glassdoor.com/Job/czech-republic-software-develop-engineer-jobs-SRCH_IL.0,14_IN77_KO15,40.htm?fromAge=$d");

//brno
//get_jobs("https://www.glassdoor.de/Job/brno-software-develop-engineer-jobs-SRCH_IL.0,4_IC2297933_KO5,30.htm?fromAge=$d");

//budapest
get_jobs("https://www.glassdoor.com/Job/budapest-software-develop-engineer-jobs-SRCH_IL.0,8_IC3125126_KO9,34.htm?fromAge=$d");

//Tbilisi
get_jobs("https://www.glassdoor.com/Job/georgia-software-development-engineer-jobs-SRCH_IL.0,7_IN90_KO8,37.htm?fromAge=$d");

//Sofia
get_jobs("https://www.glassdoor.com/Job/sofia-software-develop-engineer-jobs-SRCH_IL.0,5_IC2552422_KO6,31.htm?fromAge=$d");


function get_jobs($srcURL)
{
    global $objArr, $db ;

    $html_source = getSslPage($srcURL);

    $arr = explode("<li class='jl", $html_source);

	if (sizeof($arr)==1) {
		
        //echo("no recs @ $srcURL <br>");
        return;
    }

    //check if pagination is enabled and make a recursion loop
    $arrPG = explode("<div class='pagingControls", $html_source);
    if (sizeof($arrPG)==2)
    {
        
        $html = str_get_html("<div class='pagingControls".$arrPG[1]);
        $h = $html->find('li.next>a', 0);

        if ($h != null ) {
            $link = "https://www.glassdoor.com" . $html->find('li.next>a', 0)->href;

            get_jobs($link);
        }
    }
	
    //remove the first dummy element
    $arr = array_slice($arr,1);

    $obj = null;

    foreach($arr as $ad) {
        $obj = new Record;

        //convert string to html
        $html = str_get_html("<li class='jl".$ad);

        $company = $html->find('div.jobInfoItem', 0)->innertext;
    
        $position = $html->find('a.jobLink.jobInfoItem.jobTitle', 1)->innertext;

        $city = $html->find('span.subtle.loc', 0)->innertext;

        $obj->link = "https://www.glassdoor.com" . $html->find('a.jobLink', 0)->href;
		$obj->link = cut_unwanted_vars($obj->link); //hold only [pos / ao / s / jobListingId] vars
		
		$obj->title = $company.' - '.$position.' - '.$city;
		
        $db->addRecord($obj->title, $obj->link, 7);
    }
}

function cut_unwanted_vars($url){
	
	$position = strpos($url, 'glassdoor.com');
	
	if ($position == true) {
		
		$parts = parse_url($url);
		parse_str($parts['query'], $query);
		$jobListingId = $query['jobListingId'];
//		$pos = $query['pos'];
		$ao = $query['ao'];
		$s = $query['s'];

		return "https://www.glassdoor.com/partner/jobListing.htm?ao=$ao&s=$s&jobListingId=$jobListingId";
		
	} else {
		return $url;
	}
		
}

function getSslPage($url) {

        $userAgent = 'Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.31 (KHTML, like Gecko) Chrome/26.0.1410.64 Safari/537.31';
        $ch = curl_init();
        
/*        curl_setopt($ch, CURLOPT_STDERR, fopen("glass_debug.txt", "w+"));
        curl_setopt($ch, CURLOPT_VERBOSE, 1); */
        
        curl_setopt($ch, CURLOPT_URL, $url);
        
        curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
		curl_setopt($ch, CURLOPT_FOLLOWLOCATION, true); //added coz going to .de (?)
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
    file_put_contents('glass_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}
