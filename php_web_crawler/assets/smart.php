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

get_articles("https://jobs.smartrecruiters.com/");



function get_articles($srcURL)
{
   global $objArr, $db ;

   $html =  file_get_html($srcURL);
    
   $array_keywords = ["infosys", "developer","c#", ".net", "php", "greek", "javascript", "js", "typescript", "angular", "support", "l2", "l3"];
   $array_exclude = ["Texas", "Canada","California", "Michigan", "New York", ", AL",", AK",", AZ",", AR",", CA",", CO",", CT",", DE",", FL",", GA",", HI",", ID",", IL",", IN",", IA",", KS",", KY",", LA",", ME",", MD",", MA",", MI",", MN",", MS",", MO",", MT",", NE",", NV",", NH",", NJ",", NM",", NY",", NC",", ND",", OH",", OK",", OR",", PA",", RI",", SC",", SD",", TN",", TX",", UT",", VT",", VA",", WA",", WV",", WI",", WY",", DC",", NJ"];

   foreach($html->find('li.jobs-item') as $article) {

        $company = $article->find('div.job-logo>img', 0)->alt;
        $position = $article->find('h3.job-title', 0)->plaintext;
        $location = $article->find('p.job-location', 0)->plaintext;
        $link = $article->find('a.job', 0)->href;

        //needed keywords @ position
        $found_keyword = array_in_string($position, $array_keywords);

        if (!$found_keyword)
            continue;

        //exclude USA states
        $found_keyword = array_in_string_ending($location, $array_exclude);

        if ($found_keyword)
            continue;

        $obj = new Record;

        $obj->title = "$company - $position - $location";
        $obj->link = $link;

        $db->addRecord($obj->title, $obj->link, 11);
   }
   
}

function array_in_string($str, array $arr) {
    foreach($arr as $arr_value) { //start looping the array
        if (stripos($str,$arr_value) !== false) return true; //if $arr_value is found in $str return true
    }
    return false; //else return false
}

function array_in_string_ending($str, array $arr) {
    //https://stackoverflow.com/a/57693605/1320686
    foreach($arr as $arr_value) { //start looping the array
       if (($offset = strlen($str) - strlen($arr_value)) >= 0 && strpos($str, $arr_value, $offset) !== false)
                return true;
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
    file_put_contents('smart_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}