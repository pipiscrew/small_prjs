<?php
@session_start();

require_once ('general.php');

if (!isset($_SESSION["id"])) {
	header("Location: login.php");
	exit ;
}

if (!isset($_GET["start_date"]) || !isset($_GET["end_date"]))
{
    die("error 08x398493");
    exit;
}

$table_columns = array(
    "match_id",
    "mdate",
    "mtime",
    "champion",
    "team1",
    "team2"
    );

$db = new dbase();
$db->connect_mysql();


$start_dt = $_GET["start_date"];
$end_dt = $_GET["end_date"];



$sql="select a.match_id,a.mdate, a.mtime,champion, team1, team2, b.team1_points, b.team2_points, b.total_points, b.over as dover, b.step from history_match_start_data a
left join history_match_ingame_data_ev30sec  as b on b.match_id = a.match_id 
 where (date_rec BETWEEN  '$start_dt 00:00' AND '$end_dt 23:59') order by a.match_id, step DESC";


//////////////////////////////////////FETCH ROWS
$stmt = $db->getConnection()->prepare($sql);
$stmt->execute();
$rows_sql = $stmt->fetchAll();
$rows     = array();
$x=-1;
$last_match_id = -1;
$prin_details = false;

if (!$rows_sql)
{
    die("man plans, the god laughs!");
    exit;
}

foreach($rows_sql as $row_key){
	
	if ($last_match_id != $row_key["match_id"])
	{	
		$prin_details = false;
		$x+=1;
	}
    
    if (!$prin_details) {
        
        for($i = 0; $i < 6; $i++) //set the default table fields
        {
            $rows[$x][$table_columns[$i]] = $row_key[$table_columns[$i]];         
        }

        $prin_details = true;
    }

    

    //when is null, doesnt create cell @ fputcsv
    if ($row_key["team1_points"] === NULL) {
        $row_key["team1_points"] = "-";
    }

    if ($row_key["team2_points"] === NULL) {
        $row_key["team2_points"] = "-";
    }
    if ($row_key["total_points"] === NULL) {
        $row_key["total_points"] = "-";
    }
    if ($row_key["dover"] === NULL) {
        $row_key["dover"] = "-";
    }
    
	$step = $row_key["step"];
	$rows[$x][$step."_t1_points"] = $row_key["team1_points"];
	$rows[$x][$step."_t2_points"] = $row_key["team2_points"];
	$rows[$x][$step."_total_points"] = $row_key["total_points"];
	$rows[$x][$step."_over"] = $row_key["dover"];
	// $rows[$x][$step."step"] = $row_key["step"];
	
	$last_match_id = $row_key["match_id"]; //match_id
}



// Write to memory (unless buffer exceeds 2mb when it will write to /tmp) https://stackoverflow.com/a/31118307
$fp = fopen('php://temp', 'w+');
fwrite($fp, "match_id,mdate,mtime,champion,team1,team2,0_t1_points,0_t2_points,0_total_points,0_over,1_t1_points,1_t2_points,1_total_points,1_over,2_t1_points,2_t2_points,2_total_points,2_over,3_t1_points,3_t3_points,3_total_points,3_over,4_t1_points,4_t4_points,4_total_points,4_over,5_t1_points,5_t5_points,5_total_points,5_over,6_t1_points,6_t2_points,6_total_points,6_over,7_t1_points,7_t2_points,7_total_points,7_over,8_t1_points,8_t2_points,8_total_points,8_over,9_t1_points,9_t2_points,9_total_points,9_over,10_t1_points,10_t2_points,10_total_points,10_over,11_t1_points,11_t2_points,11_total_points,11_over,12_t1_points,12_t2_points,12_total_points,12_over,13_t1_points,13_t2_points,13_total_points,13_over,14_t1_points,14_t2_points,14_total_points,14_over,15_t1_points,15_t2_points,15_total_points,15_over,16_t1_points,16_t2_points,16_total_points,16_over,17_t1_points,17_t2_points,17_total_points,17_over,18_t1_points,18_t2_points,18_total_points,18_over,19_t1_points,19_t2_points,19_total_points,19_over,20_t1_points,20_t2_points,20_total_points,20_over,21_t1_points,21_t2_points,21_total_points,21_over,22_t1_points,22_t2_points,22_total_points,22_over,23_t1_points,23_t2_points,23_total_points,23_over,24_t1_points,24_t2_points,24_total_points,24_over,25_t1_points,25_t2_points,25_total_points,25_over,26_t1_points,26_t2_points,26_total_points,26_over,27_t1_points,27_t2_points,27_total_points,27_over,28_t1_points,28_t2_points,28_total_points,28_over,29_t1_points,29_t2_points,29_total_points,29_over,30_t1_points,30_t2_points,30_total_points,30_over,31_t1_points,31_t2_points,31_total_points,31_over,32_t1_points,32_t2_points,32_total_points,32_over,33_t1_points,33_t2_points,33_total_points,33_over,34_t1_points,34_t2_points,34_total_points,34_over,35_t1_points,35_t2_points,35_total_points,35_over,36_t1_points,36_t2_points,36_total_points,36_over,37_t1_points,37_t2_points,37_total_points,37_over,38_t1_points,38_t2_points,38_total_points,38_over,39_t1_points,39_t2_points,39_total_points,39_over,40_t1_points,40_t2_points,40_total_points,40_over,41_t1_points,41_t2_points,41_total_points,41_over,42_t1_points,42_t2_points,42_total_points,42_over,43_t1_points,43_t2_points,43_total_points,43_over,44_t1_points,44_t2_points,44_total_points,44_over,45_t1_points,45_t2_points,45_total_points,45_over,46_t1_points,46_t2_points,46_total_points,46_over,47_t1_points,47_t2_points,47_total_points,47_over,48_t1_points,48_t2_points,48_total_points,48_over,49_t1_points,49_t2_points,49_total_points,49_over,50_t1_points,50_t2_points,50_total_points,50_over,51_t1_points,51_t2_points,51_total_points,51_over,52_t1_points,52_t2_points,52_total_points,52_over,53_t1_points,53_t2_points,53_total_points,53_over,54_t1_points,54_t2_points,54_total_points,54_over,55_t1_points,55_t2_points,55_total_points,55_over,56_t1_points,56_t2_points,56_total_points,56_over,57_t1_points,57_t2_points,57_total_points,57_over,58_t1_points,58_t2_points,58_total_points,58_over,59_t1_points,59_t2_points,59_total_points,59_over,60_t1_points,60_t2_points,60_total_points,60_over,61_t1_points,61_t2_points,61_total_points,61_over,62_t1_points,62_t2_points,62_total_points,62_over,63_t1_points,63_t2_points,63_total_points,63_over,64_t1_points,64_t2_points,64_total_points,64_over,65_t1_points,65_t2_points,65_total_points,65_over,66_t1_points,66_t2_points,66_total_points,66_over,67_t1_points,67_t2_points,67_total_points,67_over,68_t1_points,68_t2_points,68_total_points,68_over,69_t1_points,69_t2_points,69_total_points,69_over,70_t1_points,70_t2_points,70_total_points,70_over,71_t1_points,71_t2_points,71_total_points,71_over,72_t1_points,72_t2_points,72_total_points,72_over,73_t1_points,73_t2_points,73_total_points,73_over,74_t1_points,74_t2_points,74_total_points,74_over,75_t1_points,75_t2_points,75_total_points,75_over,76_t1_points,76_t2_points,76_total_points,76_over,77_t1_points,77_t2_points,77_total_points,77_over,78_t1_points,78_t2_points,78_total_points,78_over,79_t1_points,79_t2_points,79_total_points,79_over,80_t1_points,80_t2_points,80_total_points,80_over,81_t1_points,81_t2_points,81_total_points,81_over,82_t1_points,82_t2_points,82_total_points,82_over,83_t1_points,83_t2_points,83_total_points,83_over" . PHP_EOL);
$x=-1;
foreach ($rows as $fields) {
    $tmp_line=null;
    $x += 1;

    //set the default table fields
	for($i = 0; $i < 6; $i++)
	{
        $tmp_line[$table_columns[$i]] = $fields[$table_columns[$i]];        
    }

    //for only the 0-83 steps
	for($i = 0; $i < 84; $i++)
	{
        //checking if has the 'group of fields', if the 'X_t1_point' doesnt exist means that also the other fields dont exist. so jump the record and ask for X+1 (some situations saw that in the middle sometimes there is no record 0123_t1_points,the 4th doesnt exist,567_t1_points)
       if (!isset($fields[$i."_t1_points"]))
       {   
            $tmp_line[$i."_t1_points"] = "-";
            $tmp_line[$i."_t2_points"] = "-";
            $tmp_line[$i."_total_points"] = "-";
            $tmp_line[$i."_over"] = "-";
            continue;
       }

        $tmp_line[$i."_t1_points"] = $fields[$i."_t1_points"];
        $tmp_line[$i."_t2_points"] = $fields[$i."_t2_points"];
        $tmp_line[$i."_total_points"] = $fields[$i."_total_points"];
        $tmp_line[$i."_over"] = $fields[$i."_over"];
    }

    // Add row to CSV buffer
    fputcsv($fp, $tmp_line);
}
rewind($fp); // Set the pointer back to the start
$csv_contents = stream_get_contents($fp); // Fetch the contents of our CSV
fclose($fp); // Close our pointer and free up memory and /tmp space

$filename ="ingame_".$start_dt."__".$end_dt.".xls";
header('Content-type: application/ms-excel');
header('Content-Disposition: attachment; filename='.$filename);
echo $csv_contents;
