<?php
@session_start();

if (!(isset($_SESSION["id"]) && $_SESSION["id"] == "40bdb060184878381c520ab7419d2accf")) {
	header("Location: index.php");
	exit ;
}

// include DB
require_once ('config.php');

$db             = connect();

$recs = getSet($db,"select * from matrix order by date_rec DESC", null);


?>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>Matrix Bot</title>

<script src='assets/jquery-3.1.1.min.js'></script>
<script src='assets/bootstrap.min.js'></script>
<script src='assets/bootstrap-table.min.js'></script>
<link rel="stylesheet" href="assets/bootstrap.min.css">
<link rel="stylesheet" href="assets/bootstrap-table.min.css">

<script>

    $(function() {
		 ///////////////////////////////////////////////////////////// FILL grid
		 var jArray_recs =   <?php echo json_encode($recs); ?>;

		 var tbl_rows = "";
		 for (var i = 0; i < jArray_recs.length; i++)
		 {
			tbl_rows += "<tr>" + 
			"<td>" + jArray_recs[i]["id"] + "</td>" +
			"<td>" + jArray_recs[i]["date_rec"] + "</td>" +
			"<td>" + jArray_recs[i]["filename"] + "</td>" +
			"<td>" + jArray_recs[i]["xml_unique_records"] + "</td>" +
			"<td>" + jArray_recs[i]["db_processed"] + "</td>" +
			"<td>" + jArray_recs[i]["db_non_processed"] + "</td>" +
			"<td>" + jArray_recs[i]["db_semi_processed"] + "</td>" +
			"<td>" + jArray_recs[i]["error"] + "</td>" +
			"</tr>"
		 }

		 $("#tbl_rows").html(tbl_rows);
		 ///////////////////////////////////////////////////////////// FILL grid



		 $("#tbl").bootstrapTable();
		
	})

</script>

	</head>
	
	<body>
		
		<div class="container-fluid">
			<table id="tbl" data-striped="true" data-height="800">
				<thead>
					<tr>
						<th>ID</th>
						<th>date_record</th>
						<th>filename</th>
						<th>xml_unique</th>
						<th>db_yes</th>
						<th>db_no</th>
						<th>db_semi</th>
						<th>error</th>
					</tr>
				</thead>

				<tbody id="tbl_rows"></tbody>
			</table>
		</div>
		
		
	</body>
</html>
</body>