<?php
@session_start();

if (!isset($_SESSION["id"])) {
	header("Location: login.php");
	exit ;
}


?>
    
<script>

    $(function() {
    	
				$('[name=start_date],[name=end_date]').datetimepicker({
			        weekStart: 1,
			        todayBtn:  1,
					autoclose: 1,
					todayHighlight: 1,
					startView: 2,
					minView: 2,
					
					forceParse: 1
			    });
			    
			    	 $('#filter_userid').on('change', function() {
							refresh_grid();
			    	 });
    
					 //convert2magic!
					 $("#history_tbl").bootstrapTable();
					
		}); //jQuery ends 
	
		function refresh_grid()
		{
			$('#history_tbl').bootstrapTable('refresh');
		}
		
		//bootstrap-table
		function queryParamsOFFERS(params)
		{
			var s,e;
				if ($("#start_date").val().trim().length > 0 && $("#end_date").val().trim().length == 0)
				{
					alert("Please fill 'end date'");
					return false;
				}
				else if ($("#end_date").val().trim().length > 0 && $("#start_date").val().trim().length == 0)
				{
					alert("Please fill 'start date'");
					return false;
				}
				else if ($("#start_date").val().trim().length > 0 && $("#end_date").val().trim().length > 0)
				{
					s=$("#start_date").val() + " 00:00";
					e=$("#end_date").val() + " 23:59";
				}
				
			var q = {
				"limit": params.limit,
				"offset": params.offset,
				"search": params.search,
				"name": params.sort,
				"order": params.order,
                //
                "start_date" : s,
                "end_date" : e
			};
			return q;
		}
		
		// server side: return object with rows and total params
	    function responseHandler(res) {
	    	
	    	$("#total_amount").html("records : " + res.total_amount);
	    	
	        return {
	            rows: res.rows,
	            total: res.total
	        }
	    }


    
</script>

		<div class="container">
		
<div id="custom-toolbar">
	<div class="row" >
		<div class="col-md-2">
			<div class='form-group'>
				<label>
					Start Date :
				</label><br>
				<input type="text" id="start_date" name="start_date" class="form-control" data-date-format="yyyy-mm-dd" value="<?= date('Y-m-d', strtotime(date('Y-m-d'). ' - 3 days')); ?>" readonly class="form_datetime">
			</div>
		</div>
		<div class="col-md-2">
			<div class='form-group' >
				<label>
					End Date :
				</label><br>
				<input type="text" id="end_date" name="end_date" class="form-control" data-date-format="yyyy-mm-dd" value="<?= date('Y-m-d');?>"  readonly class="form_datetime">
			</div>
		</div>

		<div class="col-md-2">
			<button onclick="refresh_grid()" class="btn btn-primary" style="margin-top:25px">Refresh</button>
		</div>
	</div>
</div>

				<div class="row">					
					<table id="history_tbl"
						data-url="tab_history_pagination.php"
						data-pagination="false"
						data-page-size="50"
						data-side-pagination="server"
						data-query-params="queryParamsOFFERS"
			            data-striped="true"						
						data-show-export="true"						
			            data-response-handler="responseHandler">
						<thead>
							<tr>
								<th data-field="match_id" data-sortable="true" data-visible="true">match_id</th> 
								<th data-field="mdate" data-sortable="true" data-visible="true">mdate</th> 
								<th data-field="mtime" data-sortable="true" data-visible="true">mtime</th> 
								<th data-field="champion" data-sortable="true" data-visible="true">champion</th> 
								<th data-field="team1" data-sortable="true" data-visible="true">team1</th> 
								<th data-field="team2" data-sortable="true" data-visible="true">team2</th>
								<th data-field="0_t1_points" data-sortable="true" data-visible="true">0_t1_points</th>
								<th data-field="0_t2_points" data-sortable="true" data-visible="true">0_t2_points</th>
								<th data-field="0_total_points" data-sortable="true" data-visible="true">0_total_points</th>
								<th data-field="0_over" data-sortable="true" data-visible="true">0_over</th>
								<th data-field="1_t1_points" data-sortable="true" data-visible="true">1_t1_points</th>
								<th data-field="1_t2_points" data-sortable="true" data-visible="true">1_t2_points</th>
								<th data-field="1_total_points" data-sortable="true" data-visible="true">1_total_points</th>
								<th data-field="1_over" data-sortable="true" data-visible="true">1_over</th>
								<th data-field="2_t1_points" data-sortable="true" data-visible="true">2_t1_points</th>
								<th data-field="2_t2_points" data-sortable="true" data-visible="true">2_t2_points</th>
								<th data-field="2_total_points" data-sortable="true" data-visible="true">2_total_points</th>
								<th data-field="2_over" data-sortable="true" data-visible="true">2_over</th>
								<th data-field="3_t1_points" data-sortable="true" data-visible="true">3_t1_points</th>
								<th data-field="3_t3_points" data-sortable="true" data-visible="true">3_t3_points</th>
								<th data-field="3_total_points" data-sortable="true" data-visible="true">3_total_points</th>
								<th data-field="3_over" data-sortable="true" data-visible="true">3_over</th>
								<th data-field="4_t1_points" data-sortable="true" data-visible="true">4_t1_points</th>
								<th data-field="4_t4_points" data-sortable="true" data-visible="true">4_t4_points</th>
								<th data-field="4_total_points" data-sortable="true" data-visible="true">4_total_points</th>
								<th data-field="4_over" data-sortable="true" data-visible="true">4_over</th>
								<th data-field="5_t1_points" data-sortable="true" data-visible="true">5_t1_points</th>
								<th data-field="5_t5_points" data-sortable="true" data-visible="true">5_t5_points</th>
								<th data-field="5_total_points" data-sortable="true" data-visible="true">5_total_points</th>
								<th data-field="5_over" data-sortable="true" data-visible="true">5_over</th>

								<th data-field="6_t1_points" data-sortable="true" data-visible="true">6_t1_points</th>
								<th data-field="6_t2_points" data-sortable="true" data-visible="true">6_t2_points</th>
								<th data-field="6_total_points" data-sortable="true" data-visible="true">6_total_points</th>
								<th data-field="6_over" data-sortable="true" data-visible="true">6_over</th>
								<th data-field="7_t1_points" data-sortable="true" data-visible="true">7_t1_points</th>
								<th data-field="7_t2_points" data-sortable="true" data-visible="true">7_t2_points</th>
								<th data-field="7_total_points" data-sortable="true" data-visible="true">7_total_points</th>
								<th data-field="7_over" data-sortable="true" data-visible="true">7_over</th>
								<th data-field="8_t1_points" data-sortable="true" data-visible="true">8_t1_points</th>
								<th data-field="8_t2_points" data-sortable="true" data-visible="true">8_t2_points</th>
								<th data-field="8_total_points" data-sortable="true" data-visible="true">8_total_points</th>
								<th data-field="8_over" data-sortable="true" data-visible="true">8_over</th>
								<th data-field="9_t1_points" data-sortable="true" data-visible="true">9_t1_points</th>
								<th data-field="9_t2_points" data-sortable="true" data-visible="true">9_t2_points</th>
								<th data-field="9_total_points" data-sortable="true" data-visible="true">9_total_points</th>
								<th data-field="9_over" data-sortable="true" data-visible="true">9_over</th>
								<th data-field="10_t1_points" data-sortable="true" data-visible="true">10_t1_points</th>
								<th data-field="10_t2_points" data-sortable="true" data-visible="true">10_t2_points</th>
								<th data-field="10_total_points" data-sortable="true" data-visible="true">10_total_points</th>
								<th data-field="10_over" data-sortable="true" data-visible="true">10_over</th>
								<th data-field="11_t1_points" data-sortable="true" data-visible="true">11_t1_points</th>
								<th data-field="11_t2_points" data-sortable="true" data-visible="true">11_t2_points</th>
								<th data-field="11_total_points" data-sortable="true" data-visible="true">11_total_points</th>
								<th data-field="11_over" data-sortable="true" data-visible="true">11_over</th>
								<th data-field="12_t1_points" data-sortable="true" data-visible="true">12_t1_points</th>
								<th data-field="12_t2_points" data-sortable="true" data-visible="true">12_t2_points</th>
								<th data-field="12_total_points" data-sortable="true" data-visible="true">12_total_points</th>
								<th data-field="12_over" data-sortable="true" data-visible="true">12_over</th>
								<th data-field="13_t1_points" data-sortable="true" data-visible="true">13_t1_points</th>
								<th data-field="13_t2_points" data-sortable="true" data-visible="true">13_t2_points</th>
								<th data-field="13_total_points" data-sortable="true" data-visible="true">13_total_points</th>
								<th data-field="13_over" data-sortable="true" data-visible="true">13_over</th>
								<th data-field="14_t1_points" data-sortable="true" data-visible="true">14_t1_points</th>
								<th data-field="14_t2_points" data-sortable="true" data-visible="true">14_t2_points</th>
								<th data-field="14_total_points" data-sortable="true" data-visible="true">14_total_points</th>
								<th data-field="14_over" data-sortable="true" data-visible="true">14_over</th>
								<th data-field="15_t1_points" data-sortable="true" data-visible="true">15_t1_points</th>
								<th data-field="15_t2_points" data-sortable="true" data-visible="true">15_t2_points</th>
								<th data-field="15_total_points" data-sortable="true" data-visible="true">15_total_points</th>
								<th data-field="15_over" data-sortable="true" data-visible="true">15_over</th>
								<th data-field="16_t1_points" data-sortable="true" data-visible="true">16_t1_points</th>
								<th data-field="16_t2_points" data-sortable="true" data-visible="true">16_t2_points</th>
								<th data-field="16_total_points" data-sortable="true" data-visible="true">16_total_points</th>
								<th data-field="16_over" data-sortable="true" data-visible="true">16_over</th>
								<th data-field="17_t1_points" data-sortable="true" data-visible="true">17_t1_points</th>
								<th data-field="17_t2_points" data-sortable="true" data-visible="true">17_t2_points</th>
								<th data-field="17_total_points" data-sortable="true" data-visible="true">17_total_points</th>
								<th data-field="17_over" data-sortable="true" data-visible="true">17_over</th>
								<th data-field="18_t1_points" data-sortable="true" data-visible="true">18_t1_points</th>
								<th data-field="18_t2_points" data-sortable="true" data-visible="true">18_t2_points</th>
								<th data-field="18_total_points" data-sortable="true" data-visible="true">18_total_points</th>
								<th data-field="18_over" data-sortable="true" data-visible="true">18_over</th>
								<th data-field="19_t1_points" data-sortable="true" data-visible="true">19_t1_points</th>
								<th data-field="19_t2_points" data-sortable="true" data-visible="true">19_t2_points</th>
								<th data-field="19_total_points" data-sortable="true" data-visible="true">19_total_points</th>
								<th data-field="19_over" data-sortable="true" data-visible="true">19_over</th>
								<th data-field="20_t1_points" data-sortable="true" data-visible="true">20_t1_points</th>
								<th data-field="20_t2_points" data-sortable="true" data-visible="true">20_t2_points</th>
								<th data-field="20_total_points" data-sortable="true" data-visible="true">20_total_points</th>
								<th data-field="20_over" data-sortable="true" data-visible="true">20_over</th>
								<th data-field="21_t1_points" data-sortable="true" data-visible="true">21_t1_points</th>
								<th data-field="21_t2_points" data-sortable="true" data-visible="true">21_t2_points</th>
								<th data-field="21_total_points" data-sortable="true" data-visible="true">21_total_points</th>
								<th data-field="21_over" data-sortable="true" data-visible="true">21_over</th>
								<th data-field="22_t1_points" data-sortable="true" data-visible="true">22_t1_points</th>
								<th data-field="22_t2_points" data-sortable="true" data-visible="true">22_t2_points</th>
								<th data-field="22_total_points" data-sortable="true" data-visible="true">22_total_points</th>
								<th data-field="22_over" data-sortable="true" data-visible="true">22_over</th>
								<th data-field="23_t1_points" data-sortable="true" data-visible="true">23_t1_points</th>
								<th data-field="23_t2_points" data-sortable="true" data-visible="true">23_t2_points</th>
								<th data-field="23_total_points" data-sortable="true" data-visible="true">23_total_points</th>
								<th data-field="23_over" data-sortable="true" data-visible="true">23_over</th>
								<th data-field="24_t1_points" data-sortable="true" data-visible="true">24_t1_points</th>
								<th data-field="24_t2_points" data-sortable="true" data-visible="true">24_t2_points</th>
								<th data-field="24_total_points" data-sortable="true" data-visible="true">24_total_points</th>
								<th data-field="24_over" data-sortable="true" data-visible="true">24_over</th>
								<th data-field="25_t1_points" data-sortable="true" data-visible="true">25_t1_points</th>
								<th data-field="25_t2_points" data-sortable="true" data-visible="true">25_t2_points</th>
								<th data-field="25_total_points" data-sortable="true" data-visible="true">25_total_points</th>
								<th data-field="25_over" data-sortable="true" data-visible="true">25_over</th>
								<th data-field="26_t1_points" data-sortable="true" data-visible="true">26_t1_points</th>
								<th data-field="26_t2_points" data-sortable="true" data-visible="true">26_t2_points</th>
								<th data-field="26_total_points" data-sortable="true" data-visible="true">26_total_points</th>
								<th data-field="26_over" data-sortable="true" data-visible="true">26_over</th>
								<th data-field="27_t1_points" data-sortable="true" data-visible="true">27_t1_points</th>
								<th data-field="27_t2_points" data-sortable="true" data-visible="true">27_t2_points</th>
								<th data-field="27_total_points" data-sortable="true" data-visible="true">27_total_points</th>
								<th data-field="27_over" data-sortable="true" data-visible="true">27_over</th>
								<th data-field="28_t1_points" data-sortable="true" data-visible="true">28_t1_points</th>
								<th data-field="28_t2_points" data-sortable="true" data-visible="true">28_t2_points</th>
								<th data-field="28_total_points" data-sortable="true" data-visible="true">28_total_points</th>
								<th data-field="28_over" data-sortable="true" data-visible="true">28_over</th>
								<th data-field="29_t1_points" data-sortable="true" data-visible="true">29_t1_points</th>
								<th data-field="29_t2_points" data-sortable="true" data-visible="true">29_t2_points</th>
								<th data-field="29_total_points" data-sortable="true" data-visible="true">29_total_points</th>
								<th data-field="29_over" data-sortable="true" data-visible="true">29_over</th>
								<th data-field="30_t1_points" data-sortable="true" data-visible="true">30_t1_points</th>
								<th data-field="30_t2_points" data-sortable="true" data-visible="true">30_t2_points</th>
								<th data-field="30_total_points" data-sortable="true" data-visible="true">30_total_points</th>
								<th data-field="30_over" data-sortable="true" data-visible="true">30_over</th>
								<th data-field="31_t1_points" data-sortable="true" data-visible="true">31_t1_points</th>
								<th data-field="31_t2_points" data-sortable="true" data-visible="true">31_t2_points</th>
								<th data-field="31_total_points" data-sortable="true" data-visible="true">31_total_points</th>
								<th data-field="31_over" data-sortable="true" data-visible="true">31_over</th>
								<th data-field="32_t1_points" data-sortable="true" data-visible="true">32_t1_points</th>
								<th data-field="32_t2_points" data-sortable="true" data-visible="true">32_t2_points</th>
								<th data-field="32_total_points" data-sortable="true" data-visible="true">32_total_points</th>
								<th data-field="32_over" data-sortable="true" data-visible="true">32_over</th>
								<th data-field="33_t1_points" data-sortable="true" data-visible="true">33_t1_points</th>
								<th data-field="33_t2_points" data-sortable="true" data-visible="true">33_t2_points</th>
								<th data-field="33_total_points" data-sortable="true" data-visible="true">33_total_points</th>
								<th data-field="33_over" data-sortable="true" data-visible="true">33_over</th>
								<th data-field="34_t1_points" data-sortable="true" data-visible="true">34_t1_points</th>
								<th data-field="34_t2_points" data-sortable="true" data-visible="true">34_t2_points</th>
								<th data-field="34_total_points" data-sortable="true" data-visible="true">34_total_points</th>
								<th data-field="34_over" data-sortable="true" data-visible="true">34_over</th>
								<th data-field="35_t1_points" data-sortable="true" data-visible="true">35_t1_points</th>
								<th data-field="35_t2_points" data-sortable="true" data-visible="true">35_t2_points</th>
								<th data-field="35_total_points" data-sortable="true" data-visible="true">35_total_points</th>
								<th data-field="35_over" data-sortable="true" data-visible="true">35_over</th>
								<th data-field="36_t1_points" data-sortable="true" data-visible="true">36_t1_points</th>
								<th data-field="36_t2_points" data-sortable="true" data-visible="true">36_t2_points</th>
								<th data-field="36_total_points" data-sortable="true" data-visible="true">36_total_points</th>
								<th data-field="36_over" data-sortable="true" data-visible="true">36_over</th>
								<th data-field="37_t1_points" data-sortable="true" data-visible="true">37_t1_points</th>
								<th data-field="37_t2_points" data-sortable="true" data-visible="true">37_t2_points</th>
								<th data-field="37_total_points" data-sortable="true" data-visible="true">37_total_points</th>
								<th data-field="37_over" data-sortable="true" data-visible="true">37_over</th>
								<th data-field="38_t1_points" data-sortable="true" data-visible="true">38_t1_points</th>
								<th data-field="38_t2_points" data-sortable="true" data-visible="true">38_t2_points</th>
								<th data-field="38_total_points" data-sortable="true" data-visible="true">38_total_points</th>
								<th data-field="38_over" data-sortable="true" data-visible="true">38_over</th>
								<th data-field="39_t1_points" data-sortable="true" data-visible="true">39_t1_points</th>
								<th data-field="39_t2_points" data-sortable="true" data-visible="true">39_t2_points</th>
								<th data-field="39_total_points" data-sortable="true" data-visible="true">39_total_points</th>
								<th data-field="39_over" data-sortable="true" data-visible="true">39_over</th>
								<th data-field="40_t1_points" data-sortable="true" data-visible="true">40_t1_points</th>
								<th data-field="40_t2_points" data-sortable="true" data-visible="true">40_t2_points</th>
								<th data-field="40_total_points" data-sortable="true" data-visible="true">40_total_points</th>
								<th data-field="40_over" data-sortable="true" data-visible="true">40_over</th>
								<th data-field="41_t1_points" data-sortable="true" data-visible="true">41_t1_points</th>
								<th data-field="41_t2_points" data-sortable="true" data-visible="true">41_t2_points</th>
								<th data-field="41_total_points" data-sortable="true" data-visible="true">41_total_points</th>
								<th data-field="41_over" data-sortable="true" data-visible="true">41_over</th>
								<th data-field="42_t1_points" data-sortable="true" data-visible="true">42_t1_points</th>
								<th data-field="42_t2_points" data-sortable="true" data-visible="true">42_t2_points</th>
								<th data-field="42_total_points" data-sortable="true" data-visible="true">42_total_points</th>
								<th data-field="42_over" data-sortable="true" data-visible="true">42_over</th>
								<th data-field="43_t1_points" data-sortable="true" data-visible="true">43_t1_points</th>
								<th data-field="43_t2_points" data-sortable="true" data-visible="true">43_t2_points</th>
								<th data-field="43_total_points" data-sortable="true" data-visible="true">43_total_points</th>
								<th data-field="43_over" data-sortable="true" data-visible="true">43_over</th>
								<th data-field="44_t1_points" data-sortable="true" data-visible="true">44_t1_points</th>
								<th data-field="44_t2_points" data-sortable="true" data-visible="true">44_t2_points</th>
								<th data-field="44_total_points" data-sortable="true" data-visible="true">44_total_points</th>
								<th data-field="44_over" data-sortable="true" data-visible="true">44_over</th>
								<th data-field="45_t1_points" data-sortable="true" data-visible="true">45_t1_points</th>
								<th data-field="45_t2_points" data-sortable="true" data-visible="true">45_t2_points</th>
								<th data-field="45_total_points" data-sortable="true" data-visible="true">45_total_points</th>
								<th data-field="45_over" data-sortable="true" data-visible="true">45_over</th>
								<th data-field="46_t1_points" data-sortable="true" data-visible="true">46_t1_points</th>
								<th data-field="46_t2_points" data-sortable="true" data-visible="true">46_t2_points</th>
								<th data-field="46_total_points" data-sortable="true" data-visible="true">46_total_points</th>
								<th data-field="46_over" data-sortable="true" data-visible="true">46_over</th>
								<th data-field="47_t1_points" data-sortable="true" data-visible="true">47_t1_points</th>
								<th data-field="47_t2_points" data-sortable="true" data-visible="true">47_t2_points</th>
								<th data-field="47_total_points" data-sortable="true" data-visible="true">47_total_points</th>
								<th data-field="47_over" data-sortable="true" data-visible="true">47_over</th>
								<th data-field="48_t1_points" data-sortable="true" data-visible="true">48_t1_points</th>
								<th data-field="48_t2_points" data-sortable="true" data-visible="true">48_t2_points</th>
								<th data-field="48_total_points" data-sortable="true" data-visible="true">48_total_points</th>
								<th data-field="48_over" data-sortable="true" data-visible="true">48_over</th>
								<th data-field="49_t1_points" data-sortable="true" data-visible="true">49_t1_points</th>
								<th data-field="49_t2_points" data-sortable="true" data-visible="true">49_t2_points</th>
								<th data-field="49_total_points" data-sortable="true" data-visible="true">49_total_points</th>
								<th data-field="49_over" data-sortable="true" data-visible="true">49_over</th>
								<th data-field="50_t1_points" data-sortable="true" data-visible="true">50_t1_points</th>
								<th data-field="50_t2_points" data-sortable="true" data-visible="true">50_t2_points</th>
								<th data-field="50_total_points" data-sortable="true" data-visible="true">50_total_points</th>
								<th data-field="50_over" data-sortable="true" data-visible="true">50_over</th>
								<th data-field="51_t1_points" data-sortable="true" data-visible="true">51_t1_points</th>
								<th data-field="51_t2_points" data-sortable="true" data-visible="true">51_t2_points</th>
								<th data-field="51_total_points" data-sortable="true" data-visible="true">51_total_points</th>
								<th data-field="51_over" data-sortable="true" data-visible="true">51_over</th>
								<th data-field="52_t1_points" data-sortable="true" data-visible="true">52_t1_points</th>
								<th data-field="52_t2_points" data-sortable="true" data-visible="true">52_t2_points</th>
								<th data-field="52_total_points" data-sortable="true" data-visible="true">52_total_points</th>
								<th data-field="52_over" data-sortable="true" data-visible="true">52_over</th>
								<th data-field="53_t1_points" data-sortable="true" data-visible="true">53_t1_points</th>
								<th data-field="53_t2_points" data-sortable="true" data-visible="true">53_t2_points</th>
								<th data-field="53_total_points" data-sortable="true" data-visible="true">53_total_points</th>
								<th data-field="53_over" data-sortable="true" data-visible="true">53_over</th>
								<th data-field="54_t1_points" data-sortable="true" data-visible="true">54_t1_points</th>
								<th data-field="54_t2_points" data-sortable="true" data-visible="true">54_t2_points</th>
								<th data-field="54_total_points" data-sortable="true" data-visible="true">54_total_points</th>
								<th data-field="54_over" data-sortable="true" data-visible="true">54_over</th>
								<th data-field="55_t1_points" data-sortable="true" data-visible="true">55_t1_points</th>
								<th data-field="55_t2_points" data-sortable="true" data-visible="true">55_t2_points</th>
								<th data-field="55_total_points" data-sortable="true" data-visible="true">55_total_points</th>
								<th data-field="55_over" data-sortable="true" data-visible="true">55_over</th>
								<th data-field="56_t1_points" data-sortable="true" data-visible="true">56_t1_points</th>
								<th data-field="56_t2_points" data-sortable="true" data-visible="true">56_t2_points</th>
								<th data-field="56_total_points" data-sortable="true" data-visible="true">56_total_points</th>
								<th data-field="56_over" data-sortable="true" data-visible="true">56_over</th>
								<th data-field="57_t1_points" data-sortable="true" data-visible="true">57_t1_points</th>
								<th data-field="57_t2_points" data-sortable="true" data-visible="true">57_t2_points</th>
								<th data-field="57_total_points" data-sortable="true" data-visible="true">57_total_points</th>
								<th data-field="57_over" data-sortable="true" data-visible="true">57_over</th>
								<th data-field="58_t1_points" data-sortable="true" data-visible="true">58_t1_points</th>
								<th data-field="58_t2_points" data-sortable="true" data-visible="true">58_t2_points</th>
								<th data-field="58_total_points" data-sortable="true" data-visible="true">58_total_points</th>
								<th data-field="58_over" data-sortable="true" data-visible="true">58_over</th>
								<th data-field="59_t1_points" data-sortable="true" data-visible="true">59_t1_points</th>
								<th data-field="59_t2_points" data-sortable="true" data-visible="true">59_t2_points</th>
								<th data-field="59_total_points" data-sortable="true" data-visible="true">59_total_points</th>
								<th data-field="59_over" data-sortable="true" data-visible="true">59_over</th>
								<th data-field="60_t1_points" data-sortable="true" data-visible="true">60_t1_points</th>
								<th data-field="60_t2_points" data-sortable="true" data-visible="true">60_t2_points</th>
								<th data-field="60_total_points" data-sortable="true" data-visible="true">60_total_points</th>
								<th data-field="60_over" data-sortable="true" data-visible="true">60_over</th>
								<th data-field="61_t1_points" data-sortable="true" data-visible="true">61_t1_points</th>
								<th data-field="61_t2_points" data-sortable="true" data-visible="true">61_t2_points</th>
								<th data-field="61_total_points" data-sortable="true" data-visible="true">61_total_points</th>
								<th data-field="61_over" data-sortable="true" data-visible="true">61_over</th>
								<th data-field="62_t1_points" data-sortable="true" data-visible="true">62_t1_points</th>
								<th data-field="62_t2_points" data-sortable="true" data-visible="true">62_t2_points</th>
								<th data-field="62_total_points" data-sortable="true" data-visible="true">62_total_points</th>
								<th data-field="62_over" data-sortable="true" data-visible="true">62_over</th>
								<th data-field="63_t1_points" data-sortable="true" data-visible="true">63_t1_points</th>
								<th data-field="63_t2_points" data-sortable="true" data-visible="true">63_t2_points</th>
								<th data-field="63_total_points" data-sortable="true" data-visible="true">63_total_points</th>
								<th data-field="63_over" data-sortable="true" data-visible="true">63_over</th>
								<th data-field="64_t1_points" data-sortable="true" data-visible="true">64_t1_points</th>
								<th data-field="64_t2_points" data-sortable="true" data-visible="true">64_t2_points</th>
								<th data-field="64_total_points" data-sortable="true" data-visible="true">64_total_points</th>
								<th data-field="64_over" data-sortable="true" data-visible="true">64_over</th>
								<th data-field="65_t1_points" data-sortable="true" data-visible="true">65_t1_points</th>
								<th data-field="65_t2_points" data-sortable="true" data-visible="true">65_t2_points</th>
								<th data-field="65_total_points" data-sortable="true" data-visible="true">65_total_points</th>
								<th data-field="65_over" data-sortable="true" data-visible="true">65_over</th>
								<th data-field="66_t1_points" data-sortable="true" data-visible="true">66_t1_points</th>
								<th data-field="66_t2_points" data-sortable="true" data-visible="true">66_t2_points</th>
								<th data-field="66_total_points" data-sortable="true" data-visible="true">66_total_points</th>
								<th data-field="66_over" data-sortable="true" data-visible="true">66_over</th>
								<th data-field="67_t1_points" data-sortable="true" data-visible="true">67_t1_points</th>
								<th data-field="67_t2_points" data-sortable="true" data-visible="true">67_t2_points</th>
								<th data-field="67_total_points" data-sortable="true" data-visible="true">67_total_points</th>
								<th data-field="67_over" data-sortable="true" data-visible="true">67_over</th>
								<th data-field="68_t1_points" data-sortable="true" data-visible="true">68_t1_points</th>
								<th data-field="68_t2_points" data-sortable="true" data-visible="true">68_t2_points</th>
								<th data-field="68_total_points" data-sortable="true" data-visible="true">68_total_points</th>
								<th data-field="68_over" data-sortable="true" data-visible="true">68_over</th>
								<th data-field="69_t1_points" data-sortable="true" data-visible="true">69_t1_points</th>
								<th data-field="69_t2_points" data-sortable="true" data-visible="true">69_t2_points</th>
								<th data-field="69_total_points" data-sortable="true" data-visible="true">69_total_points</th>
								<th data-field="69_over" data-sortable="true" data-visible="true">69_over</th>
								<th data-field="70_t1_points" data-sortable="true" data-visible="true">70_t1_points</th>
								<th data-field="70_t2_points" data-sortable="true" data-visible="true">70_t2_points</th>
								<th data-field="70_total_points" data-sortable="true" data-visible="true">70_total_points</th>
								<th data-field="70_over" data-sortable="true" data-visible="true">70_over</th>
								<th data-field="71_t1_points" data-sortable="true" data-visible="true">71_t1_points</th>
								<th data-field="71_t2_points" data-sortable="true" data-visible="true">71_t2_points</th>
								<th data-field="71_total_points" data-sortable="true" data-visible="true">71_total_points</th>
								<th data-field="71_over" data-sortable="true" data-visible="true">71_over</th>
								<th data-field="72_t1_points" data-sortable="true" data-visible="true">72_t1_points</th>
								<th data-field="72_t2_points" data-sortable="true" data-visible="true">72_t2_points</th>
								<th data-field="72_total_points" data-sortable="true" data-visible="true">72_total_points</th>
								<th data-field="72_over" data-sortable="true" data-visible="true">72_over</th>
								<th data-field="73_t1_points" data-sortable="true" data-visible="true">73_t1_points</th>
								<th data-field="73_t2_points" data-sortable="true" data-visible="true">73_t2_points</th>
								<th data-field="73_total_points" data-sortable="true" data-visible="true">73_total_points</th>
								<th data-field="73_over" data-sortable="true" data-visible="true">73_over</th>
								<th data-field="74_t1_points" data-sortable="true" data-visible="true">74_t1_points</th>
								<th data-field="74_t2_points" data-sortable="true" data-visible="true">74_t2_points</th>
								<th data-field="74_total_points" data-sortable="true" data-visible="true">74_total_points</th>
								<th data-field="74_over" data-sortable="true" data-visible="true">74_over</th>
								<th data-field="75_t1_points" data-sortable="true" data-visible="true">75_t1_points</th>
								<th data-field="75_t2_points" data-sortable="true" data-visible="true">75_t2_points</th>
								<th data-field="75_total_points" data-sortable="true" data-visible="true">75_total_points</th>
								<th data-field="75_over" data-sortable="true" data-visible="true">75_over</th>
								<th data-field="76_t1_points" data-sortable="true" data-visible="true">76_t1_points</th>
								<th data-field="76_t2_points" data-sortable="true" data-visible="true">76_t2_points</th>
								<th data-field="76_total_points" data-sortable="true" data-visible="true">76_total_points</th>
								<th data-field="76_over" data-sortable="true" data-visible="true">76_over</th>
								<th data-field="77_t1_points" data-sortable="true" data-visible="true">77_t1_points</th>
								<th data-field="77_t2_points" data-sortable="true" data-visible="true">77_t2_points</th>
								<th data-field="77_total_points" data-sortable="true" data-visible="true">77_total_points</th>
								<th data-field="77_over" data-sortable="true" data-visible="true">77_over</th>
								<th data-field="78_t1_points" data-sortable="true" data-visible="true">78_t1_points</th>
								<th data-field="78_t2_points" data-sortable="true" data-visible="true">78_t2_points</th>
								<th data-field="78_total_points" data-sortable="true" data-visible="true">78_total_points</th>
								<th data-field="78_over" data-sortable="true" data-visible="true">78_over</th>
								<th data-field="79_t1_points" data-sortable="true" data-visible="true">79_t1_points</th>
								<th data-field="79_t2_points" data-sortable="true" data-visible="true">79_t2_points</th>
								<th data-field="79_total_points" data-sortable="true" data-visible="true">79_total_points</th>
								<th data-field="79_over" data-sortable="true" data-visible="true">79_over</th>
								<th data-field="80_t1_points" data-sortable="true" data-visible="true">80_t1_points</th>
								<th data-field="80_t2_points" data-sortable="true" data-visible="true">80_t2_points</th>
								<th data-field="80_total_points" data-sortable="true" data-visible="true">80_total_points</th>
								<th data-field="80_over" data-sortable="true" data-visible="true">80_over</th>
								<th data-field="81_t1_points" data-sortable="true" data-visible="true">81_t1_points</th>
								<th data-field="81_t2_points" data-sortable="true" data-visible="true">81_t2_points</th>
								<th data-field="81_total_points" data-sortable="true" data-visible="true">81_total_points</th>
								<th data-field="81_over" data-sortable="true" data-visible="true">81_over</th>
								<th data-field="82_t1_points" data-sortable="true" data-visible="true">82_t1_points</th>
								<th data-field="82_t2_points" data-sortable="true" data-visible="true">82_t2_points</th>
								<th data-field="82_total_points" data-sortable="true" data-visible="true">82_total_points</th>
								<th data-field="82_over" data-sortable="true" data-visible="true">82_over</th>
								<th data-field="83_t1_points" data-sortable="true" data-visible="true">83_t1_points</th>
								<th data-field="83_t2_points" data-sortable="true" data-visible="true">83_t2_points</th>
								<th data-field="83_total_points" data-sortable="true" data-visible="true">83_total_points</th>
								<th data-field="83_over" data-sortable="true" data-visible="true">83_over</th>
								<th data-field="84_t1_points" data-sortable="true" data-visible="true">84_t1_points</th>
								<th data-field="84_t2_points" data-sortable="true" data-visible="true">84_t2_points</th>
								<th data-field="84_total_points" data-sortable="true" data-visible="true">84_total_points</th>
								<th data-field="84_over" data-sortable="true" data-visible="true">84_over</th>
								<th data-field="85_t1_points" data-sortable="true" data-visible="true">85_t1_points</th>
								<th data-field="85_t2_points" data-sortable="true" data-visible="true">85_t2_points</th>
								<th data-field="85_total_points" data-sortable="true" data-visible="true">85_total_points</th>
								<th data-field="85_over" data-sortable="true" data-visible="true">85_over</th>
								<th data-field="86_t1_points" data-sortable="true" data-visible="true">86_t1_points</th>
								<th data-field="86_t2_points" data-sortable="true" data-visible="true">86_t2_points</th>
								<th data-field="86_total_points" data-sortable="true" data-visible="true">86_total_points</th>
								<th data-field="86_over" data-sortable="true" data-visible="true">86_over</th>
								<th data-field="87_t1_points" data-sortable="true" data-visible="true">87_t1_points</th>
								<th data-field="87_t2_points" data-sortable="true" data-visible="true">87_t2_points</th>
								<th data-field="87_total_points" data-sortable="true" data-visible="true">87_total_points</th>
								<th data-field="87_over" data-sortable="true" data-visible="true">87_over</th>
								<th data-field="88_t1_points" data-sortable="true" data-visible="true">88_t1_points</th>
								<th data-field="88_t2_points" data-sortable="true" data-visible="true">88_t2_points</th>
								<th data-field="88_total_points" data-sortable="true" data-visible="true">88_total_points</th>
								<th data-field="88_over" data-sortable="true" data-visible="true">88_over</th>
								<th data-field="89_t1_points" data-sortable="true" data-visible="true">89_t1_points</th>
								<th data-field="89_t2_points" data-sortable="true" data-visible="true">89_t2_points</th>
								<th data-field="89_total_points" data-sortable="true" data-visible="true">89_total_points</th>
								<th data-field="89_over" data-sortable="true" data-visible="true">89_over</th>
								<th data-field="90_t1_points" data-sortable="true" data-visible="true">90_t1_points</th>
								<th data-field="90_t2_points" data-sortable="true" data-visible="true">90_t2_points</th>
								<th data-field="90_total_points" data-sortable="true" data-visible="true">90_total_points</th>
								<th data-field="90_over" data-sortable="true" data-visible="true">90_over</th>
								<th data-field="91_t1_points" data-sortable="true" data-visible="true">91_t1_points</th>
								<th data-field="91_t2_points" data-sortable="true" data-visible="true">91_t2_points</th>
								<th data-field="91_total_points" data-sortable="true" data-visible="true">91_total_points</th>
								<th data-field="91_over" data-sortable="true" data-visible="true">91_over</th>
								<th data-field="92_t1_points" data-sortable="true" data-visible="true">92_t1_points</th>
								<th data-field="92_t2_points" data-sortable="true" data-visible="true">92_t2_points</th>
								<th data-field="92_total_points" data-sortable="true" data-visible="true">92_total_points</th>
								<th data-field="92_over" data-sortable="true" data-visible="true">92_over</th>
								<th data-field="93_t1_points" data-sortable="true" data-visible="true">93_t1_points</th>
								<th data-field="93_t2_points" data-sortable="true" data-visible="true">93_t2_points</th>
								<th data-field="93_total_points" data-sortable="true" data-visible="true">93_total_points</th>
								<th data-field="93_over" data-sortable="true" data-visible="true">93_over</th>
								<th data-field="94_t1_points" data-sortable="true" data-visible="true">94_t1_points</th>
								<th data-field="94_t2_points" data-sortable="true" data-visible="true">94_t2_points</th>
								<th data-field="94_total_points" data-sortable="true" data-visible="true">94_total_points</th>
								<th data-field="94_over" data-sortable="true" data-visible="true">94_over</th>
								<th data-field="95_t1_points" data-sortable="true" data-visible="true">95_t1_points</th>
								<th data-field="95_t2_points" data-sortable="true" data-visible="true">95_t2_points</th>
								<th data-field="95_total_points" data-sortable="true" data-visible="true">95_total_points</th>
								<th data-field="95_over" data-sortable="true" data-visible="true">95_over</th>
								<th data-field="96_t1_points" data-sortable="true" data-visible="true">96_t1_points</th>
								<th data-field="96_t2_points" data-sortable="true" data-visible="true">96_t2_points</th>
								<th data-field="96_total_points" data-sortable="true" data-visible="true">96_total_points</th>
								<th data-field="96_over" data-sortable="true" data-visible="true">96_over</th>
								<th data-field="97_t1_points" data-sortable="true" data-visible="true">97_t1_points</th>
								<th data-field="97_t2_points" data-sortable="true" data-visible="true">97_t2_points</th>
								<th data-field="97_total_points" data-sortable="true" data-visible="true">97_total_points</th>
								<th data-field="97_over" data-sortable="true" data-visible="true">97_over</th>
								<th data-field="98_t1_points" data-sortable="true" data-visible="true">98_t1_points</th>
								<th data-field="98_t2_points" data-sortable="true" data-visible="true">98_t2_points</th>
								<th data-field="98_total_points" data-sortable="true" data-visible="true">98_total_points</th>
								<th data-field="98_over" data-sortable="true" data-visible="true">98_over</th>
								<th data-field="99_t1_points" data-sortable="true" data-visible="true">99_t1_points</th>
								<th data-field="99_t2_points" data-sortable="true" data-visible="true">99_t2_points</th>
								<th data-field="99_total_points" data-sortable="true" data-visible="true">99_total_points</th>
								<th data-field="99_over" data-sortable="true" data-visible="true">99_over</th>

							</tr>
						</thead>
						<tbody id="history_rows"></tbody>
					</table>
					<br/>
					<center><span id="total_amount" class='label label-danger label'></span></center>
				</div>
</div>
