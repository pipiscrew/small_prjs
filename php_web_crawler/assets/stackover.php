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

get_articles('https://stackoverflow.com/jobs?sort=p');

/*
working local when online lists only the new jobs, crying.
//CZ
get_articles('https://stackoverflow.com/jobs?l=Czech+Republic&d=20&u=Km&sort=p');

//GR
get_articles('https://stackoverflow.com/jobs?l=Greece&d=20&u=Km&sort=p');*/


function get_articles($srcURL)
{
   global $objArr, $db ;

   $html =  file_get_html($srcURL);

   $array_exclude = ["Texas", "Canada","California",", AL",", AK",", AZ",", AR",", CA",", CO",", CT",", DE",", FL",", GA",", HI",", ID",", IL",", IN",", IA",", KS",", KY",", LA",", ME",", MD",", MA",", MI",", MN",", MS",", MO",", MT",", NE",", NV",", NH",", NJ",", NM",", NY",", NC",", ND",", OH",", OK",", OR",", PA",", RI",", SC",", SD",", TN",", TX",", UT",", VT",", VA",", WA",", WV",", WI",", WY",", DC",", NJ", ", Japan", ", South Korea"];
   
   foreach($html->find('div.-job.js-result') as $article) {

    $g = $article->prev_sibling();

    //site puts a line and listing by other countries ads, always in the end of your search, these ifs identify and exit
/*    if ($g!=null)
        if ($g->hasAttribute("class"))
            if ($g->getAttribute("class") == "secondary-job-results-identifier" )
                break;*/

        $company = $article->find('h3.fc-black-700.fs-body1.mb4', 0);
        $position = $article->find('h2.mb4.fc-black-800.fs-body3>a',0);
        $timeposted = $article->find('div.grid--cell.fc-orange-400.fw-bold',0);
       
        if($company!=null && $position!=null && $timeposted!=null) {

            $company = str_replace('  ', ' ', trim($company->plaintext));
            $company = str_replace('  ', ' ', $company);$company = str_replace('  ', ' ', $company);$company = str_replace('  ', ' ', $company);
            $company = str_replace('  ', ' ', $company);$company = str_replace('  ', ' ', $company);$company = str_replace('  ', ' ', $company);

            $link = $position->href;
            $position = trim($position->plaintext);
            
            
	        //exclude USA states
	        $found_keyword = array_in_string($company, $array_exclude);

	        if ($found_keyword)
	            continue;
	            
            $obj = new Record;
            
            $obj->title = "$company - $position";
            $obj->link = "https://stackoverflow.com".$link;
    
            $db->addRecord($obj->title, $obj->link, 12);
        }
   }
}

function array_in_string($str, array $arr) {
    foreach($arr as $arr_value) { //start looping the array
        if (stripos($str,$arr_value) !== false) return true; //if $arr_value is found in $str return true
    }
    return false; //else return false
}


function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( Exception $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('stack_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}