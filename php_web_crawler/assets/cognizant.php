<?php

$minute = intval(date('i')); //get minute

if ($minute > 20 && $minute < 40)
	return;

set_time_limit(120);

set_error_handler( "log_error" );
set_exception_handler( "log_exception" );

require_once ('../general.php');

class Record
{
    public $title;
    public $link;
}

//connect to database
$db = new dbase();
$db->connect();

get_articles('https://careers.cognizant.com/widgets');


function get_articles($srcURL)
{
    global $objArr, $db ;

    $json =  make_post_request($srcURL);
	
    $jobs = $json->refineSearch->data->jobs;
	
    foreach ($jobs as $item) {
        $obj = new Record;
        
        $obj->title = $item->title.' - '.$item->city;
		$obj->link = 'https://careers.cognizant.com/global/en/job/'.$item->jobId;
        //$objArr[] = $obj;
		$db->addRecord($obj->title, $obj->link, 4);
    }

}

/*
with Brave v1.4.96 32bit

we go to https://careers.cognizant.com/global/en/c/it-infrastructure-jobs
select countries and sort newest, on debug console, we capture the request done @ 
careers.cognizant.com/widgets

	with ddoKey => refineSearch

we click the request and going to 'Request Payload' > clicking 'view source' (is on the same bar)
give us pure json.

we going to convert the JSON to PHP array through : 
https://wtools.io/convert-json-to-php-array

replace the array here.
*/

function make_post_request($url) {

	$params = array (
					  'lang' => 'en_global',
					  'deviceType' => 'desktop',
					  'country' => 'global',
					  'ddoKey' => 'refineSearch',
					  'sortBy' => 'Most recent',
					  'subsearch' => '',
					  'from' => 0,
					  'jobs' => true,
					  'counts' => true,
					  'all_fields' => 
					  array (
						0 => 'category',
						1 => 'location',
						2 => 'empStatus',
						3 => 'industryName',
						4 => 'minExp',
					  ),
					  'pageName' => 'IT Infrastructure',
					  'pageType' => 'category',
					  'size' => 50,
					  'clearAll' => false,
					  'jdsource' => 'facets',
					  'isSliderEnable' => false,
					  'keywords' => '',
					  'global' => true,
					  'selected_fields' => 
					  array (
						'category' => 
						array (
						  0 => 'IT Infrastructure',
						),
						'location' => 
						array (
						  0 => 'Prague, Prague, Czech Republic',
						  1 => 'Budapest, Budapest, Hungary',
						),
					  ),
					  'sort' => 
					  array (
						'order' => 'desc',
						'field' => 'postedDate',
					  ),
					);
				
	$curl = curl_init();
	curl_setopt($curl, CURLOPT_URL, $url);
	curl_setopt($curl, CURLOPT_POST, true);


	$params = json_encode($params);
	curl_setopt($curl, CURLOPT_POSTFIELDS, $params);
	curl_setopt($curl, CURLOPT_HTTPHEADER, array('Content-Type: application/json; charset=UTF-8', 'X-Accept: application/json'));


	// display header
	// curl_setopt( $curl , CURLOPT_HEADER, 1 ) ; 
	curl_setopt( $curl , CURLOPT_CUSTOMREQUEST , 'POST');
	curl_setopt( $curl , CURLOPT_SSL_VERIFYPEER , false ) ;								
	curl_setopt( $curl , CURLOPT_RETURNTRANSFER , true ) ;								
	curl_setopt( $curl , CURLOPT_TIMEOUT , 5 ) ;

	$response = curl_exec($curl);
	
	//	http status code
	//	$status = curl_getinfo($c, CURLINFO_HTTP_CODE);
	//	var_dump($status);
	
	curl_close($curl);
	
	return json_decode($response);

}



function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('cognizant_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}