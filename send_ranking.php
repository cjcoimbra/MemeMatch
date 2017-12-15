<?php

$player    = $_GET['username'];
$score    = $_GET['score']; 

mysql_connect( "...", "...", "..." ) or die( mysql_error() );
mysql_select_db( "..." ) or die( mysql_error() );


$query = "INSERT INTO Ranking (Name, Score) VALUES ('".$player."', '".$score."')";
mysql_query($query);
echo 'OK';
?>
