<?php

$player    = $_GET['username'];
$score    = $_GET['score']; 

mysql_connect( "mysql12.uni5.net", "spacecatstudio01", "a1b2c3d4e5" ) or die( mysql_error() );
mysql_select_db( "spacecatstudio01" ) or die( mysql_error() );


$query = "INSERT INTO Ranking (Name, Score) VALUES ('".$player."', '".$score."')";
mysql_query($query);
echo 'OK';
?>