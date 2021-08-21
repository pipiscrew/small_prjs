<?php
class dbase{
	private $db;

	function connect() {
		$this->db = new PDO('sqlite:'.__DIR__.'/dbase.db',null,null,array(
			//PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
			PDO::ATTR_EMULATE_PREPARES => false,
			PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC));
	}
    
	function addRecord($title, $link, $site_id){
		$guid = md5($link);
		$daterec = date('Y-m-d H:i:s');
		
		$sql = "insert into feed_items (title,site_id,link,guid,daterec) VALUES (:title, :site_id, :link, :guid, :daterec)";
		$stmt = $this->db->prepare($sql);
		$stmt->bindValue(':title', $title);
		$stmt->bindValue(':site_id', $site_id);
		$stmt->bindValue(':link', $link);
		$stmt->bindValue(':guid', $guid);
		$stmt->bindValue(':daterec', $daterec);
		$stmt->execute();
	}
	
	function getScalar($sql, $params) {
		if ($stmt = $this->db -> prepare($sql)) {
	 
			$stmt->execute($params);
	 
			return $stmt->fetchColumn();
		} else
			return 0;
	}
	
	function getSet($sql, $params) {
		if ($stmt = $this->db -> prepare($sql)) {

			$stmt->execute($params);
	 
		  return $stmt->fetchAll();
		} else
			return 0;
	}
	
	function executeSQL($sql, $params) {
		if ($stmt = $this->db -> prepare($sql)) {
	 
			$stmt->execute($params);
	 
			return $stmt->rowCount();
		} else
			return false;
	}
}