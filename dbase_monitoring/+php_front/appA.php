<?php
@session_start();

if (!(isset($_SESSION["id"]) && $_SESSION["id"] == "40bdb06018487838c520ab7419d2accf")) {
	header("Location: index.php");
	exit ;
}

// include DB
require_once ('config.php');

$db             = connect();

$recs = getSet($db,"select aaa_id, bbb_file, ccc_file, ddd_file, honey_file_count, honey_sums, db_honey_bbb_sum, db_honey_ccc_ddd_sum, bbb_file_sum, bbb_db_sum, ccc_file_sum, ccc_db_sum, ddd_file_sum, ddd_db_sum, customer_file, product_file, invoice_file, first_invoice, last_invoice, chaos_invoice_count, stage_invoice_count, aaa_tt_log, replication_log, date_rec, flat_unique_inv, POP_vaaaus_flat_not_exist, POP_vaaaus_flat, stage_vaaaus_flat, sapinv_vaaaus_flat, odetails_duplicate, payments_count, payments_detail_count, flat_cust, cust_db, aaa_tt_log_report, tt_aaa_report, stage_proceedn, stage_proceedy, stage_invoice_types, dashboard_count, stage_invoice_OOO, error from aaa order by date_rec DESC", null);


?>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
<title>aaa Bot</title>

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
						"<td>" + jArray_recs[i]["aaa_id"] + "</td>" +
						"<td>" + jArray_recs[i]["date_rec"] + "</td>" +
						"<td>" + jArray_recs[i]["bbb_file"] + "</td>" +
						"<td>" + jArray_recs[i]["ccc_file"] + "</td>" +
						"<td>" + jArray_recs[i]["ddd_file"] + "</td>" +
						"<td>" + jArray_recs[i]["honey_file_count"] + "</td>" +
						"<td>" + jArray_recs[i]["honey_sums"] + "</td>" +
						"<td>" + jArray_recs[i]["db_honey_bbb_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["db_honey_ccc_ddd_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["customer_file"] + "</td>" +
						"<td>" + jArray_recs[i]["product_file"] + "</td>" +
						"<td>" + jArray_recs[i]["invoice_file"] + "</td>" +
						"<td>" + jArray_recs[i]["first_invoice"] + "</td>" +
						"<td>" + jArray_recs[i]["last_invoice"] + "</td>" +
						"<td>" + jArray_recs[i]["flat_unique_inv"] + "</td>" +
						"<td>" + jArray_recs[i]["POP_vaaaus_flat_not_exist"] + "</td>" +
						"<td>" + jArray_recs[i]["POP_vaaaus_flat"] + "</td>" +
						"<td>" + jArray_recs[i]["dashboard_count"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_vaaaus_flat"] + "</td>" +
						"<td>" + jArray_recs[i]["sapinv_vaaaus_flat"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_invoice_types"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_proceedn"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_proceedy"] + "</td>" +
						"<td>" + jArray_recs[i]["payments_count"] + "</td>" +
						"<td>" + jArray_recs[i]["payments_detail_count"] + "</td>" +
						"<td>" + jArray_recs[i]["odetails_duplicate"] + "</td>" +
						"<td>" + jArray_recs[i]["chaos_invoice_count"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_invoice_count"] + "</td>" +
						"<td>" + jArray_recs[i]["aaa_tt_log"] + "</td>" +
						"<td>" + jArray_recs[i]["replication_log"] + "</td>" +
						"<td>" + jArray_recs[i]["bbb_file_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["bbb_db_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["ccc_file_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["ccc_db_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["ddd_file_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["ddd_db_sum"] + "</td>" +
						"<td>" + jArray_recs[i]["flat_cust"] + "</td>" +
						"<td>" + jArray_recs[i]["cust_db"] + "</td>" +
						"<td>" + jArray_recs[i]["aaa_tt_log_report"] + "</td>" +
						"<td>" + jArray_recs[i]["tt_aaa_report"] + "</td>" +
						"<td>" + jArray_recs[i]["error"] + "</td>" +
						"<td>" + jArray_recs[i]["stage_invoice_OOO"] + "</td>" +
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
				return '<i title="file copied from boots" class="glyphicon glyphicon-ok-sign"></i> '
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
		<div class="container-fluid" style="bottom: 20px;">
			<table id="tbl" data-striped="true" data-height="800">
				<thead>
					<tr>
						<th>ID</th>
						<th>date_record</th>
						<th data-formatter="checkFormatter">bbb_file</th>
						<th data-formatter="checkFormatter">ccc_file</th>
						<th data-formatter="checkFormatter">ddd_file</th>
						<th>honey_file_count</th>
						<th title='the application result of which file(s) equal with dbase values (db_honey_bbb_sum, db_honey_ccc_ddd_sum)'>honey_sums</th>
						<th>db_honey_bbb_sum</th>
						<th>db_honey_ccc_ddd_sum</th>
						<th data-formatter="checkFormatter">customer_file</th>
						<th data-formatter="checkFormatter">product_file</th>
						<th data-formatter="checkFormatter">invoice_file</th>
						<th title='Flat file SAP start invoice number'>first_invoice</th>
						<th title='Flat file SAP last invoice number'>last_invoice</th>
						<th title='Flat file (invoice*.dat) unique invoices sent by SAP'>flat_unique_inv</th>
						<th title='how many records NOT exist to DBASE, chaos table, vaaaus the flat x.dat'>POP_vaaaus_flat_not_exist</th>
						<th title='how many records is in the aaa_chaos table, compared to the flat x.dat'>POP_vaaaus_flat</th>
						<th title='query by fgdgd'>dashboard</th>
						<th title='how many records is in the aaa_INVOICES_RRR table, compared to the flat x.dat'>stage_vaaaus_flat</th>
						<th title='how many records is in the aaa_SAP_INVOICES table, compared to the flat x.dat'>sapinv_vaaaus_flat</th>
						<th title='group by invoice types as STAGE table'>stage_inv_types</th>
						<th title='proceed N to STAGE table'>stage_proceed_no</th>
						<th title='proceed Y to STAGE table'>stage_proceed_yes</th>
						<th title='how many record exist for yesterday to tt_aaa_PAYMENTS--C table'>payments_count</th>
						<th title='how many record exist for yesterday to tt_aaa_PAYMENTDETAILS--C table'>payments_detail_count</th>
						<th data-formatter="checkFormatter" title='Batman verification for Order Details duplicate'>odetails_duplicate</th>
						<th title='a count(*) where dfsdf between #first_invoice# and #last_invoice#'>chaos_invoice_count</th>
						<th title='a count(*) where sdfsdfsd between #first_invoice# and #last_invoice#'>stage_invoice_count</th>
						<th title='validation query as per specs, if count of the records > 0 there is an error'>aaa_tt_log</th>
						<th title='query sdf--C (aka sdf), this always should be 1'>replication_log</th>
						<th>bbb_file_sum</th>
						<th>bbb_db_sum</th>
						<th>ccc_file_sum</th>
						<th>ccc_db_sum</th>
						<th>ddd_file_sum</th>
						<th>ddd_db_sum</th>
						<th title='line count of customer flat'>flat_cust</th>
						<th title='record count of aaa_sap_customerhierarchy table'>cust_db</th>
						<th title='record count for message like %Finished% or %report generated%'>aaa_tt_log_report</th>
						<th title='record count of tt_aaa_systemvars--c table'>tt_aaa_report</th>
						<th title='describes any error happened to the BOT'>error</th>
						<th title='OOO invoices found in STAGE table with PROCESSED_YN=Y'>stage_OOO</th>
					</tr>
				</thead>

				<tbody id="tbl_rows"></tbody>
			</table>
		</div>
		
		
	</body>
</html>
</body>