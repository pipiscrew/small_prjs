<?php
set_time_limit(300); //5 minutes (5 * 60);

$urls = array('https://domain.com/jobs/assets/citrix.php',
'https://domain.com/jobs/assets/cleverism.php',
'https://domain.com/jobs/assets/cognizant.php',
'https://domain.com/jobs/assets/collegelink.php',
'https://domain.com/jobs/assets/expats.php',
'https://domain.com/jobs/assets/glass.php',
'https://domain.com/jobs/assets/kariera.php',
'https://domain.com/jobs/assets/linked.php',
'https://domain.com/jobs/assets/neuvoo.php',
'https://domain.com/jobs/assets/smart.php',
'https://domain.com/jobs/assets/stackover.php',
'https://domain.com/jobs/assets/startupjobs.php',
'https://domain.com/jobs/assets/workable.php',
'https://domain.com/jobs/assets/amnanews.php',
'https://domain.com/jobs/assets/ifarmakeia.php',
'https://domain.com/jobs/assets/xe.php');

    foreach($urls as $u)
    {
        dothecall($u);
    }


function dothecall($url){
	try {
	// create a new cURL resource
	$ch = curl_init();

	//instead of printing it on the screen
	curl_setopt($ch, CURLOPT_RETURNTRANSFER, true);
	
	// set URL and other appropriate options
	curl_setopt($ch, CURLOPT_URL, $url);
	curl_setopt($ch, CURLOPT_HEADER, 0);

	// grab URL and pass it to the browser
	curl_exec($ch);

	// close cURL resource, and free up system resources
	curl_close($ch);
	} catch (Exception $e) {
		$message = date("Y-m-d O H:i:0")." Type: " . get_class( $e ) . "; Message: {$e->getMessage()}; File: {$e->getFile()}; Line: {$e->getLine()};";
		file_put_contents('dacron_errors.txt',  $message.PHP_EOL , FILE_APPEND | LOCK_EX);
	}

}