<?php

$game= $_GET['game'];

mysql_connect( "", "", "" ) or die( mysql_error() );
mysql_select_db( "" ) or die( mysql_error() );

$amount = 1;

$query = "SELECT Score FROM Ranking WHERE Name = 'MemeMatch' ORDER BY Score DESC LIMIT " . $amount;
		
	$result = mysql_query( $query );
		
	$top = mysql_fetch_array($result, MYSQL_NUM);
		
	if(mysql_error() || $top == NULL)
	{
		$return = "not available";
		echo($return);
	}
	else
	{
		$ranking = "";
			
		do
		{
			$ranking = $ranking . $top[0];
                
		} while(($top = mysql_fetch_array($result, MYSQL_NUM)) != NULL);

		echo($ranking);
	}

?>
