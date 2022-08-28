
<!DOCTYPE html>
<html lang="en" >

<head>
  <meta charset="UTF-8">
  <title>News Minimalistic</title>
  <link href='https://fonts.googleapis.com/css?family=Quicksand' rel='stylesheet' type='text/css'>
   <base target="_blank">  
<style>
a:visited {
  color: #336699;
}
body {
  font-family: Quicksand;
  margin-left: 40px;
  padding: 0;
  color: #fff;
  background: linear-gradient(0, #281130 50%, #030133);
}
h1 {
  position: relative;
  font-size: 45px;
  margin: 15px 0;
  display: inline-block;
}
h1:after {
  content: '';
  position: absolute;
  bottom: -2px;
  left: 0;
  right: 0;
  height: 2px;
  background-color: #fff;
  border-radius: 5px;
}
.link-cont {
  position: relative;
  font-size: 24px;
}
.link {
  display: inline-block;
  position: relative;
  text-decoration: none;
  padding: 10px 0;
  color: #fff;
}
.link-wrapper {
  position: relative;
  display: block;
  padding: 20px 0;
}
.inner-wrapper {
  position: relative;
  display: inline-block;
}
/* hover styles */
.hover-1:after {
  content: '';
  position: absolute;
  width: 100%;
  height: 3px;
  bottom: 0;
  left: 0;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.hover-1:hover:after {
  transform-origin: bottom left;
  transform: scaleX(1);
}
.hover-2:after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  width: 100%;
  height: 3px;
  transform: scaleX(0);
  background-color: #20C2F7;
  transition: transform 0.3s;
}
.hover-2:hover:after {
  transform: scaleX(1);
}
.hover-3:after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #37D631;
  transform: scaleX(0);
  transform-origin: bottom left;
  transition: transform 0.3s;
}
.hover-3:hover:after {
  transform: scaleX(1);
}
.hover-4:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.hover-4:hover:after {
  transform: scaleX(1);
}
.hover-5:after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  width: 100%;
  height: 3px;
  transform: scaleY(0);
  background-color: #E2061B;
  transition: transform 0.3s;
}
.hover-5:hover:after {
  transform: scaleY(1);
}
.hover-6:before {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  right: 50%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transform-origin: bottom left;
  transition: transform 0.3s;
}
.hover-6:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  left: 50%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.hover-6:hover:before {
  transform: scaleX(1);
}
.hover-6:hover:after {
  transform: scaleX(1);
}
.hover-7:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #37D631;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.hover-7:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #37D631;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.hover-7:hover:before {
  transform: scaleX(1);
}
.hover-7:hover:after {
  transform: scaleX(1);
}
.hover-8:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.hover-8:after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.hover-8:hover:before,
.hover-8:hover:after {
  transform: scaleX(1);
}
.hover-9:before {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: top right;
  transition: transform 0.3s;
}
.hover-9:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: top right;
  transition: transform 0.3s;
}
.hover-9:hover:before,
.hover-9:hover:after {
  transform: scaleX(1);
}
.hover-10:before {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transition: transform 0.3s;
}
.hover-10:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transition: transform 0.3s;
}
.hover-10:hover:before,
.hover-10:hover:after {
  transform: scaleX(1);
}
.hover-11:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  width: 100%;
  height: 3px;
  transform: scaleY(0);
  background-color: #37D631;
  transition: transform 0.3s;
}
.hover-11:after {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  right: 0;
  width: 100%;
  height: 3px;
  transform: scaleY(0);
  background-color: #37D631;
  transition: transform 0.2s;
}
.hover-11:hover:before,
.hover-11:hover:after {
  transform: scaleY(1);
}
.hover-12:before {
  content: '';
  position: absolute;
  width: 100%;
  height: 3px;
  top: 0;
  left: 0;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.hover-12:after {
  content: '';
  position: absolute;
  width: 100%;
  height: 3px;
  bottom: 0;
  right: 0;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.hover-12:hover:before {
  transform-origin: top right;
  transform: scaleX(1);
}
.hover-12:hover:after {
  transform-origin: bottom left;
  transform: scaleX(1);
}
.wrapper-13:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 50%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.wrapper-13:after {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  left: 50%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: top right;
  transition: transform 0.3s;
}
.wrapper-13 .hover-13:before {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  right: 50%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: bottom left;
  transition: transform 0.3s;
}
.wrapper-13 .hover-13:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  left: 50%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.wrapper-13 .hover-13:hover:before,
.wrapper-13 .hover-13:hover:after {
  transform: scaleX(1);
}
.wrapper-13:hover:before,
.wrapper-13:hover:after {
  transform: scaleX(1);
}
.wrapper-14:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 3px;
  height: 100%;
  background-color: #20C2F7;
  transform: scaleY(0);
  transition: transform 0.3s;
}
.wrapper-14:after {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 3px;
  height: 100%;
  background-color: #20C2F7;
  transform: scaleY(0);
  transition: transform 0.3s;
}
.wrapper-14 .hover-14 {
  padding: 10px;
}
.wrapper-14 .hover-14:before {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transition: transform 0.3s;
}
.wrapper-14 .hover-14:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #20C2F7;
  transform: scaleX(0);
  transition: transform 0.3s;
}
.wrapper-14:hover:before,
.wrapper-14:hover:after {
  transform: scaleY(1);
}
.wrapper-14:hover .hover-14:before,
.wrapper-14:hover .hover-14:after {
  transform: scaleX(1);
}
.wrapper-15:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 3px;
  height: 100%;
  background-color: #37D631;
  transform: scaleY(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.wrapper-15:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 3px;
  height: 100%;
  background-color: #37D631;
  transform: scaleY(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.wrapper-15 .hover-15 {
  padding: 10px;
}
.wrapper-15 .hover-15:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #37D631;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.wrapper-15 .hover-15:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #37D631;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.wrapper-15:hover:before,
.wrapper-15:hover:after {
  transform: scaleY(1);
}
.wrapper-15:hover .hover-15:before,
.wrapper-15:hover .hover-15:after {
  transform: scaleX(1);
}
.wrapper-16:before {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  width: 3px;
  height: 100%;
  background-color: #ffcc00;
  transform: scaleY(0);
  transform-origin: bottom left;
  transition: transform 0.3s;
}
.wrapper-16:after {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 3px;
  height: 100%;
  background-color: #ffcc00;
  transform: scaleY(0);
  transform-origin: top right;
  transition: transform 0.3s;
}
.wrapper-16 .hover-16 {
  padding: 10px;
}
.wrapper-16 .hover-16:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.3s;
}
.wrapper-16 .hover-16:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #ffcc00;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.3s;
}
.wrapper-16:hover:before,
.wrapper-16:hover:after {
  transform: scaleY(1);
}
.wrapper-16:hover .hover-16:before,
.wrapper-16:hover .hover-16:after {
  transform: scaleX(1);
}
.wrapper-17:before {
  content: '';
  position: absolute;
  bottom: 0;
  left: 0;
  width: 3px;
  height: 100%;
  background-color: #E2061B;
  transform: scaleY(0);
  transform-origin: bottom left;
  transition: transform 0.2s;
}
.wrapper-17:after {
  content: '';
  position: absolute;
  top: 0;
  right: 0;
  width: 3px;
  height: 100%;
  background-color: #E2061B;
  transform: scaleY(0);
  transform-origin: top right;
  transition: transform 0.2s 0.2s;
}
.wrapper-17 .hover-17 {
  padding: 10px;
}
.wrapper-17 .hover-17:before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: top left;
  transition: transform 0.2s 0.3s;
}
.wrapper-17 .hover-17:after {
  content: '';
  position: absolute;
  bottom: 0;
  right: 0;
  width: 100%;
  height: 3px;
  background-color: #E2061B;
  transform: scaleX(0);
  transform-origin: bottom right;
  transition: transform 0.2s 0.1s;
}
.wrapper-17:hover:before {
  transform: scaleY(1);
  transition: transform 0.2s 0.3s;
}
.wrapper-17:hover:after {
  transform: scaleY(1);
  transition: transform 0.2s 0.1s;
}
.wrapper-17:hover .hover-17:before {
  transform: scaleX(1);
  transition: transform 0.2s;
}
.wrapper-17:hover .hover-17:after {
  transform: scaleX(1);
  transition: transform 0.2s 0.2s;
}
</style>

<script>
//test - no needed
function test(e){
    // <a href='#' onclick='return test(event);'>test</a>
    console.log(e);
    console.log(e.previousSibling);
    return false;
}
</script>
</head>



<?php

class dbase{

	private $db;

    function connect_sqlite() {
        $this->db = new PDO('sqlite:database.db',null,null,array(
            PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
            PDO::ATTR_EMULATE_PREPARES => false,
            PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC));
    }

	function getSet($sql, $params) {
		if ($stmt = $this->db -> prepare($sql)) {
			$stmt->execute($params);
	 
		  return $stmt->fetchAll();
		} else
			return 0;
	}
} //class ends

?>

<h1>News Redefined</h1>

<!-- big fonts -->
<div class="link-cont">

<?php

$db = new dbase();
$db->connect_sqlite();

if (isset($_GET['id'])) {

    //query
    $feedSet = $db->getSet("select title,url from messages where is_deleted=0 and feed={$_GET['id']} order by date_created desc limit 500", null);

    //loop
    foreach($feedSet as $r){

        $url = $r['url'];
        $title = $r['title'];

        $line =  <<<EOT
        <div class='link-wrapper'>
            <a class='link hover-12' href='$url'>$title</a>
        </div>

    EOT;

        echo $line;
    }


}
else {

    //query
    $feedSet = $db->getSet('select * from feeds where update_type<>0 order by id', null);

    //loop
    foreach($feedSet as $r){
        $id = "?id={$r['id']}";
        $title = $r['title'];

        $line =  <<<EOT
        <div class='link-wrapper'>
            <a class='link hover-12' href='$id'>$title</a>
        </div>

    EOT;

        echo $line;
    }
}
