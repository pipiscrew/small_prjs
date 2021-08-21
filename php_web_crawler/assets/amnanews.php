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


get_articles();


function get_articles()
{
    $urls = array("https://www.amna.gr/feeds/getarticle.php?rolecat=650&infolevel=BASIC", 
    "https://www.amna.gr/feeds/getarticle.php?rolecat=601&infolevel=BASIC",
    "https://www.amna.gr/feeds/getarticle.php?rolecat=602&infolevel=BASIC",
    "https://www.amna.gr/feeds/getarticle.php?rolecat=603&infolevel=BASIC",
    "https://www.amna.gr/feeds/getarticle.php?rolecat=750&infolevel=BASIC",
    "https://www.amna.gr/feeds/getarticle.php?rolecat=550&infolevel=BASIC",
    "https://www.amna.gr/feeds/getfolder.php?id=145320&infolevel=INTERMEDIATE&offset=0&numrows=1&kind=article&byrole=false&subfolders=false&order=[[%22v_order%22,%22asc%22],[%22id%22,%22desc%22]]&exclude=",
    "https://www.amna.gr/feeds/getfolder.php?id=102783&infolevel=INTERMEDIATE&offset=0&numrows=1&kind=article&byrole=false&subfolders=false&order=[[%22v_order%22,%22asc%22],[%22id%22,%22desc%22]]&exclude=",
    "https://www.amna.gr/feeds/newsbyroles.php?id=20&infolevel=BASIC&offset=3&numrows=15&category=health");

    //VA news
    //"https://www.amna.gr/feeds/getfolder.php?id=147202&infolevel=INTERMEDIATE&offset=0&numrows=20&kind=videos&byrole=false&subfolders=true&order=[[%22v_order%22,%22asc%22],[%22c_timestamp%22,%22desc%22]]&exclude=156898");

    foreach($urls as $u)
    {
        $json = file_get_contents($u);
        $arr = json_decode($json,true);

        parse_article($arr);
    }

}

function parse_article($arr)
{
    global $objArr, $db ;



    if (!array_key_exists("id",$arr)){//is_array($arr[0])) {
        foreach($arr as $item) {
            parse_article($item);
        }
    }
    else 
    {
        if (!$arr["id"])
            return;

        $obj = new Record;

        $obj->title = $arr["title"];

        //find article kind, when is coming from bundle or arrays
        $article_kind = "article";
        if (array_key_exists("kind",$arr)) 
            $article_kind = $arr["kind"];

            
        if (array_key_exists("parent_title",$arr)) {
			if ($arr["parent_title"] == "Γραφήματα")
				$article_kind = "graphics";
		}
		
        $category = $arr["note2"]; //health
        $rec_id = $arr["id"];
        $rec_url_title = $arr["note3"];
        
        $obj->category = $arr["parent_title"];
        $obj->title = '('.$obj->category.') '.$obj->title;
        $obj->link = "https://www.amna.gr/" . $category . "/" . $article_kind . "/" . $rec_id . "/" . $rec_url_title;
        
        $db->addRecord($obj->title, $obj->link, 1);
    }
}




function log_error( $num, $str, $file, $line, $context = null )
{
    log_exception( new ErrorException( $str, 0, $num, $file, $line ) );
}

function log_exception( $e )
{

    $message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
    file_put_contents('amna_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);

    header($_SERVER['SERVER_PROTOCOL'] . ' 500 Internal Server Error', true, 500);
    exit(1);
}
