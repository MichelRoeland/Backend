<?php
	$client = new SoapClient("http://10.0.170.151/BlockChainApi.svc?wsdl");
	$response = $soapclient->GetAllPatiens();
	var_dump($response);
?>