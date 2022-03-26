<?php

//////// MINIMAL AUTOLOADER [start]

// Add the appropiate extra include paths here
$includePaths = array(
    __DIR__."MaxMind/",
);
 
// Add the above paths to the global include path
set_include_path(implode(PATH_SEPARATOR, $includePaths));
 
// Register the psr-0 autoloader for ease of use 
spl_autoload_register(function($c){@include preg_replace('#\\\|_(?!.+\\\)#','/',$c).'.php';}); 

//////// MINIMAL AUTOLOADER [end]

// require_once('MaxMind/Db/Reader.php');
// require_once('MaxMind/Db/Reader/Decoder.php');
// require_once('MaxMind/Db/Reader/Util.php');
// require_once('MaxMind/Db/Reader/Metadata.php');
// require_once('MaxMind/Db/Reader/InvalidDatabaseException.php');

use MaxMind\Db\Reader;


$maxmind = (new Reader('assets/GeoLite2-Country.mmdb'))->get(get_ip());



/////////// VALIDATION /////////// 
$country = isset($maxmind) && isset($maxmind['country']) ? $maxmind['country']['iso_code'] : null;

//check for blacklisted countries
$blacklisted_countries = array('AU','NL');

//alternative - https://www.ip2location.com/free/visitor-blocker
if($country && in_array($country, $blacklisted_countries ?? [])) {
    die('content not allowed by your country');
}



echo '------------------ COUNTRY DETAILS ------------------<br>';
echo '<pre>';
print_r($maxmind);
echo '</pre>';


////////////////////////

function get_ip() {
    if(array_key_exists('HTTP_X_FORWARDED_FOR', $_SERVER)) {

        if(mb_strpos($_SERVER['HTTP_X_FORWARDED_FOR'], ',')) {
            $ips = explode(',', $_SERVER['HTTP_X_FORWARDED_FOR']);

            return trim(reset($ips));
        } else {
            return $_SERVER['HTTP_X_FORWARDED_FOR'];
        }

    } else if (array_key_exists('REMOTE_ADDR', $_SERVER)) {
        return $_SERVER['REMOTE_ADDR'];
    } else if (array_key_exists('HTTP_CLIENT_IP', $_SERVER)) {
        return $_SERVER['HTTP_CLIENT_IP'];
    }

    return '';
}

