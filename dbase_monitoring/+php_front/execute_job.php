<?php

/* working only on windows based PHP server 

when someone hit this file, execute an EXE

*/

error_reporting(-1);
ini_set( 'display_errors', 1 );

try {
$answer = exec("C:\apps\bot\bot.exe -monitoring -nomail");

echo "Ended with".$answer."</br>"; 
}
catch (Exception $e) {
	echo 'Caught exception: ',  $e->getMessage(), "\n";
    echo json_encode(null);
		
	}