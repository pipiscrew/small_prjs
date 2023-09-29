<?php
@session_start();

if (!isset($_SESSION['secret']))
die('no sevret found');

if (!isset($_GET['passw2FA']))
die('no user password found');

require_once('Google2FA.php');


$google2fa = new Google2FA();

$valid = $google2fa->verifyKey($_SESSION['secret'], $_GET['passw2FA']);
echo '<br>xxxxxx';
var_Dump( $valid);
echo 'yyyyyy';