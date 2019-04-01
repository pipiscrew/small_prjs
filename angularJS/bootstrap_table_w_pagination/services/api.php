<?php

// header("HTTP/1.0 404 Not Found");
// return;
require_once('general.php');

$db = new dbase();

$db->connect_sqlite();

getRootItems();

function getRootItems() {
    global $db;

    $rows = $db->getSet('select nodename as label, nodeid as id, isfolder    
        from codes where parentnode=0  
        order by isfolder, nodename limit 5', null);
    
        $output = json_encode(array('employees'=> $rows,'totalCount' => 30)); 

        //$item = str_replace('{{videotitle}}', 'test', $item);
        echo str_replace('"[]"', '[{}]', $output);
}
