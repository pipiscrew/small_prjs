<?php

/*

Pocket API v3 as datecode 2020_02_05

refs : 
https://getpocket.com/developer/docs/authentication
https://getpocket.com/developer/docs/v3/retrieve
https://getpocket.com/developer/docs/v3/modify
https://getpocket.com/connected_applications


1-at first run the this php without any parameters - plain index.php, this will go to getpocket asking to authorize, once you click authorize
will come to callback index.php?validate=1
2-then you can execute 
index.php?add=1
index.php?get=1

*/

@session_start();

error_reporting(E_ALL);
ini_set('display_errors', '1');

$consumer_key = '';
$redirect_url = '~~this url~~';
$access_token = '';

if (isset($_GET['get'])) {

	$params = array();
	$params['consumer_key'] = $consumer_key;
	$params['access_token'] = $access_token;
	$params['sort'] = 'newest';
	$params['count'] = 10;
	$params['detailType'] = 'simple';
	
	$resp = make_post_request("https://getpocket.com/v3/get", $params);

	var_dump($resp);
}
else if (isset($_GET['add'])) {

	$params = array();
	$params['consumer_key'] = $consumer_key;
	$params['access_token'] = $access_token;
	$params['url'] = "http://yahoo.com";
	$params['title'] = "iTeaching: The New Pedagogy";
	$params['time'] = strtotime('today UTC');
	
	$resp = make_post_request("https://getpocket.com/v3/add", $params);
	
	var_dump($resp);
}
else if (isset($_GET['validate'])) {
	//callback

	//verify to pocket
	$params = array();
	$params['consumer_key'] = $consumer_key;
	$params['code'] = $_SESSION['code'];

	$resp = make_post_request("https://getpocket.com/v3/oauth/authorize", $params);
	
	//one time - here get the access_token, assing it to variable on the top, having the access_token you can do any action provided afterwards.
	var_dump($resp);
}
else {
	//authorize
	
	$params = array();
	$params['consumer_key'] = $consumer_key;
	$params['redirect_uri'] = $redirect_url;

	$resp = make_post_request('https://getpocket.com/v3/oauth/request', $params);

	//store to session, catch it back on authorized callback
	$_SESSION['code'] =  $resp->code;

	//redirect user to pocket authorize the app
	header('Location: ' . 'https://getpocket.com/auth/authorize?request_token='.$resp->code."&redirect_uri=$redirect_url");

}


function make_post_request($url, $params) {
	
	$params = json_encode($params);
	
	$c = curl_init();
	curl_setopt($c, CURLOPT_URL, $url);
	curl_setopt($c, CURLOPT_POST, true);
	curl_setopt($c, CURLOPT_POSTFIELDS, $params);
	curl_setopt($c, CURLOPT_RETURNTRANSFER, true);
	curl_setopt($c, CURLOPT_FOLLOWLOCATION, true);
	curl_setopt($c, CURLINFO_HEADER_OUT, true);
	curl_setopt($c, CURLOPT_HTTPHEADER, array('Content-Type: application/json; charset=UTF-8', 'X-Accept: application/json'));
	
	$response = curl_exec($c);
	
	curl_close($c);
	
	return json_decode($response);

}