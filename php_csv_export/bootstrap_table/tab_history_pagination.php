<?php
@session_start();

if (!isset($_SESSION["id"])) {
	header("Location: login.php");
	exit ;
}

// include your code to connect to DB.
include ('general.php');

$table_columns = array(
"match_id",
"mdate",
"mtime",
"champion",
"team1",
"team2",
"0_t1_points",
"0_t2_points",
"0_total_points",
"0_over",
"1_t1_points",
"1_t2_points",
"1_total_points",
"1_over",
"2_t1_points",
"2_t2_points",
"2_total_points",
"2_over"
);

$db = new dbase();
$db->connect_mysql();

///////////////////////////////////extra-dates
$start_dt = $_GET["start_date"];
$end_dt = $_GET["end_date"];
$where ="";

if (!empty($start_dt) && !empty($end_dt))
{
	$where = " where (date_rec BETWEEN '$start_dt' AND '$end_dt') ";
}


$sql="select a.match_id,a.mdate, a.mtime,champion, team1, team2, b.team1_points, b.team2_points, b.total_points, b.over as dover, b.step from history_match_start_data a
left join history_match_ingame_data_ev30sec  as b on b.match_id = a.match_id 
 {$where} order by a.match_id, step DESC"; //date_rec desc, a.match_id, step ";

$count_query_sql = "select count(*) from history_match_start_data {$where}";

//$total_query_sql = "select FORMAT(sum(offer_total_amount),2, 'de_DE') as gen_total from offers where is_deleted=0 and approval_user_date is null {$where}";
//////////////////////////////////////WHEN SEARCH TEXT SPECIFIED
/*if (isset($_GET["search"]) && !empty($_GET["search"]))
{
	$sdafsd= $_GET["search"];
	$like_str = " or #field# like :searchTerm";
	$where = " 0=1 ";
	foreach($table_columns as $col)
	{
		$where.= str_replace("#field#",$col, $like_str);
	}
	$sql.= " where ". $where;
	$count_query_sql.= " where ". $where;
}*/
//////////////////////////////////////WHEN SORT COLUMN NAME SPECIFIED
if (isset($_GET["name"]) && isset($_GET["order"]))
{
	$name= $_GET["name"];
	$order= $_GET["order"];
	$sql.= " order by {$_GET["name"]} {$_GET["order"]}";
}
//////////////////////////////////////PREPARE
$stmt = $db->getConnection()->prepare($sql); //." limit :offset,:limit");
//////////////////////////////////////WHEN SEARCH TEXT SPECIFIED *BIND*
if (isset($_GET["search"]) && !empty($_GET["search"]))
	$stmt->bindValue(':searchTerm', '%'.$_GET["search"].'%');
//////////////////////////////////////WHEN COLSORT
//if (isset($_GET["name"]) && isset($_GET["order"]))
//{
//	$stmt->bindValue(':col_name', $name);
//	$stmt->bindValue(':col_order', $order);
//}

//////////////////////////////////////FETCH ROWS
$stmt->execute();
$rows_sql = $stmt->fetchAll();
$rows     = array();
$x=-1;
$last_match_id = -1;
$prin_details = false;

foreach($rows_sql as $row_key){
	
	if ($last_match_id != $row_key["match_id"])
	{	
		$prin_details = false;
		$x+=1;
	}
	
	//set the default table fields
	for($i = 0; $i < 7; $i++)
	{
		if (!$prin_details) {

			if ($table_columns[$i] == "0_t1_points")
			{
				$prin_details = true;
				break;
			}
			else 	
				$rows[$x][$table_columns[$i]] = $row_key[$table_columns[$i]];
		} else 
			break;
	}

	$step = $row_key["step"];
	$rows[$x][$step."_t1_points"] = $row_key["team1_points"];
	$rows[$x][$step."_t2_points"] = $row_key["team2_points"];
	$rows[$x][$step."_total_points"] = $row_key["total_points"];
	$rows[$x][$step."_over"] = $row_key["dover"];
	$rows[$x][$step."step"] = $row_key["step"];

	
	
	$last_match_id = $row_key["match_id"]; //match_id
}



//////////////////////////////////////COUNT TOTAL 
if (isset($_GET["search"]) && !empty($_GET["search"]))
	$count_recs = $db->getScalar($count_query_sql, array(':searchTerm' => '%'.$_GET["search"].'%'));
else
	$count_recs = $db->getScalar($count_query_sql, null);

//$total_amount = getScalar($conn,$total_query_sql,null);
//////////////////////////////////////JSON ENCODE
$arr = array('total'=> $count_recs,'rows' => $rows,'total_amount' => $count_recs);
header("Content-Type: application/json", true);
echo json_encode($arr);
?>
