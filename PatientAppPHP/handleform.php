<?php
	if($_SERVER['REQUEST_METHOD'] === "POST"){
		$count = (count($_POST) -2) /3;		
		$file = fopen("data/insightrequests.txt", "w+");
		for($i = 0; $i <= $count; $i++){			
			fwrite($file, $_POST['doctor_'.$i].";".$_POST['permission_'.$i].";".$_POST['datetime_'.$i]."***");
		}
		header("Location: ../insightrequests.php");
	} else{
		echo "Fu*k off!";
	}
?>