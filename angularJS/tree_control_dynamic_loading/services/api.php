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
        from codes where parentnode=? 
        order by isfolder, nodename', array($_GET['id']));
    
        echo json_encode($rows);

        //$item = str_replace('{{videotitle}}', 'test', $item);
        //echo str_replace('"[]"', '[{}]', $output);
}
