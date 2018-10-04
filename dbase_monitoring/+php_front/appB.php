<?php
@session_start();

if (!(isset($_SESSION["id"]) && $_SESSION["id"] == "40bdb060184878381c520ab7419d2accf")) {
	header("Location: index.php");
	exit ;
}

// include DB
require_once ('config.php');

$db             = connect();

$recs = getSet($db,"select id,field1,field2,CONCAT(field3,' vs ',(field4 - field5) +1) as file_invoices,tbl_pp_invoices,tbl_ww,tbl_sap_inv,tbl_xx_sap_customer,jj_records,missed_file_past_2days,error from wonderland order by date_rec DESC", null);


?>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>SPS Bot</title>

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
			"<td>" + jArray_recs[i]["customer_file"] + "</td>" +
			"<td>" + jArray_recs[i]["product_file"] + "</td>" +
			"<td>" + jArray_recs[i]["invoice_file"] + "</td>" +
			"<td>" + jArray_recs[i]["field1"] + "</td>" +
			"<td>" + jArray_recs[i]["field2"] + "</td>" +
			"<td>" + jArray_recs[i]["field3"] + "</td>" +
			"<td>" + jArray_recs[i]["field4"] + "</td>" +
			"<td>" + jArray_recs[i]["first_invoice"] + "</td>" +
			"<td>" + jArray_recs[i]["last_invoice"] + "</td>" +
			"<td>" + jArray_recs[i]["file_invoices"] + "</td>" +
			"<td>" + jArray_recs[i]["tbl_pp_invoices"] + "</td>" +
			"<td>" + jArray_recs[i]["tbl_ww"] + "</td>" +
			"<td>" + jArray_recs[i]["tbl_sap_inv"] + "</td>" +
			"<td>" + jArray_recs[i]["tbl_xx_sap_customer"] + "</td>" +
			"<td>" + jArray_recs[i]["jj_records"] + "</td>" +
			"<td>" + jArray_recs[i]["missed_file_past_2days"] + "</td>" +
			"<td>" + jArray_recs[i]["error"] + "</td>" +
			"</tr>"
		 }

		 $("#tbl_rows").html(tbl_rows);
		 ///////////////////////////////////////////////////////////// FILL grid



		 $("#tbl").bootstrapTable();
		
	})
	
    function checkFormatter(value, row) {
		console.log(value);
        //var icon = value == 1 ? 'glyphicon-ok' : 'glyphicon-remove';
		var icon; 
		
		switch (value) {
			case '1' :
				return '<i title="file copied" class="glyphicon glyphicon-ok"></i> '
				break;
			case '0' :
				return '<i title="file not exists" class="glyphicon glyphicon-remove"></i> '
				break;
			case '2' :
				return '<i title="file copied by batman" class="glyphicon glyphicon-ok-sign"></i> '
				break;		
		}
		/*
		if (value!=2)
			return ;
		else 
			return '<i title="file copied from boots" class="glyphicon ' + icon + '"></i> ';
		*/
    }
</script>

	</head>
	
	<body>
		
		<div class="container-fluid">
			<table id="tbl" data-striped="true" data-height="800">
				<thead>
					<tr>
						<th>ID</th>
						<th>date_record</th>
						<th data-formatter="checkFormatter">cust_file</th>
						<th data-formatter="checkFormatter">prod_file</th>
						<th data-formatter="checkFormatter">inv_file</th>
						<th data-formatter="checkFormatter">field1</th>
						<th>field2</th>
						<th>field3</th>
						<th>field4</th>
						<th>first_invoice</th>
						<th>last_invoice</th>
						<th title='file vs (last invoice - first invoice)'>file_invoices</th>
						<th>tbl_pp_invoices</th>
						<th>tbl_ww</th>
						<th>sap_inv</th>
						<th>tbl_xx_sap_customer</th>
						<th>jj_records</th>
						<th>missed_file</th>
						<th>error</th>
					</tr>
				</thead>

				<tbody id="tbl_rows"></tbody>
			</table>
		</div>
		
		
	</body>
</html>
</body>