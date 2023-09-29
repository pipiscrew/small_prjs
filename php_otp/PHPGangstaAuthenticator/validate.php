<?php
@session_start();

require_once('GoogleAuthenticator.php');

$ga = new PHPGangsta_GoogleAuthenticator();

if (!isset($_SESSION['secret']))
    die('no sevret found');

if (!isset($_GET['passw2FA']))
    die('no user password found');
else 
{


$secret = (string) $_SESSION['secret'];
$otp = (string) $_GET['passw2FA'];


$checkResult = $ga->verifyCode($secret, $otp, 2);    // 2 = 2*30sec clock tolerance

if ($checkResult) {
    echo 'OK';
} else {
    echo 'FAILED';
}


}
