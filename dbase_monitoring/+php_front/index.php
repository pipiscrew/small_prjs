<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
	<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0" />
	<link href="assets/bootstrap.min.css" rel="stylesheet">
</head>

<style>
	body {
	  paddding-top: 40px;
	  paddding-bottom: 40px;
	  background-color: #eee;
	}
 
	.form-signin {
	  max-width: 330px;
	  paddding: 15px;
	  margin: 0 auto;
	}
	.form-signin .form-signin-heading,
	.form-signin .checkbox {
	  margin-bottom: 10px;
	}
	.form-signin .checkbox {
	  font-weight: normal;
	}
	.form-signin .form-control {
	  position: relative;
	  height: auto;
	  -webkit-box-sizing: border-box;
		 -moz-box-sizing: border-box;
			  box-sizing: border-box;
	  paddding: 10px;
	  font-size: 16px;
	}
	.form-signin .form-control:focus {
	  z-index: 2;
	}
	.form-signin input[type="email"] {
	  margin-bottom: -1px;
	  border-bottom-right-radius: 0;
	  border-bottom-left-radius: 0;
	}
	.form-signin input[type="password"] {
	  margin-bottom: 10px;
	  border-top-left-radius: 0;
	  border-top-right-radius: 0;
	}
</style>

<body>

<?php
@session_start();
    		
if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $_SESSION['id'] = md5($_POST["upassword"]); //convert plain text to md5
} 

if (isset($_SESSION["id"])) {
	
	if ($_SESSION["id"] == "40bdb060184878381c520ab7419d2accf")
	{	?>
	
		<title>Automation Paradise!</title>
		<div class="container">
			<div class="row" style="margin-top:50px">
				<div class="col-md-4"></div>
				<div class="col-md-4">
					<a class="label label-danger" href="logoff.php">logoff</a><br><br>
					
					<a href="appB.php" class="btn btn-block btn-primary">SPS</a>
					<a href="appC.php" class="btn btn-block btn-success">Matrix</a>
					<a href="appA.php" class="btn btn-block btn-danger">aaa</a>
					
					<span class="label label-info pull-right" style="display: inline-block;margin-top:50px" >maintained by Batman</span>
				</div>	
				<div class="col-md-4"></div>
			</div>			
		</div>

<?php
		exit ;
	} else {
		session_destroy();
	}
}
?>

	<title>maintained by Batman</title>
    <div class="container">
 
      <form class="form-signin" method="POST" action="">
        <h2 class="form-signin-heading">Please sign in</h2>
        <label for="upassword" class="sr-only">Password</label>
        <input type="password" name="upassword" id="upassword" class="form-control" placeholder="Password" required>
 
        <button class="btn btn-lg btn-primary btn-block" type="submit">Sign in</button>
      </form>
 
    </div> <!-- /container -->

</body>
</html>


</body>