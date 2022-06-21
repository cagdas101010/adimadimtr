<?php
$servername = "localhost";
$username = "root";
$password = "";

// Create connection
$conn = new mysqli($servername, $username, $password,"adimadimtrdb");

// Check connection
if ($conn->connect_error) {
  echo "Connection failed: " . $conn->connect_error;
  exit();
}

if($_POST['unity']=="kayitol"){
	$nick=$_POST['nick'];
	
	$sorgu="insert into kullanicilar (nick,skor) values('$nick',0)";
	
	$sorgusonuc=$conn->query($sorgu);
	if($sorgusonuc){
		echo "Kayıt Başarılı!";
	}
	else {
		echo "Farklı bir kullanıcı adı giriniz!";
		
	}
	
	
}

else if($_POST['unity']=='sorular'){
	
	$sorgu="select * from sorular";
$sorgusonuc=$conn->query($sorgu);
if($sorgusonuc->num_rows>0){
$butunsatirlar=array();
while($satirlar=$sorgusonuc->fetch_object()){
	
	array_push($butunsatirlar,array(
	"soru"=>$satirlar->soru,
	"a_cevap"=>$satirlar->a_cevap,
	"b_cevap"=>$satirlar->b_cevap,
	"c_cevap"=>$satirlar->c_cevap,
	"d_cevap"=>$satirlar->d_cevap,
	"dogrucevap"=>$satirlar->dogrucevap,
	"saniye"=>$satirlar->saniye
	)
	);
}
 echo json_encode(array("butunSorular"=>$butunsatirlar));
}
}


else if($_POST['unity']=="skorumugetir"){
	
	$nick=$_POST['nick'];
	
	$sorgu="select * from kullanicilar where nick='$nick'";
	
	$sorgusonuc=$conn->query($sorgu);
	
	if($sorgusonuc){
		
		echo $sorgusonuc->fetch_object()->skor;
	}
	else{
		echo "Skor Hatası!";
	}
}

else if($_POST['unity']=="skorumuGuncelle"){
	$nick=$_POST['nick'];
	$skor=$_POST['skor'];
	
	$sorgu="Update kullanicilar set skor='$skor' where  nick='$nick' ";
	
	$sorgusonuc=$conn->query($sorgu);
	if($sorgusonuc){
		echo "Güncellendi!";
	}
	else {
		echo "Güncelleme Hatası!";
		
	}
	
}
else if($_POST['unity']=='SkorlariGetir'){
	
$sorgu="select * from kullanicilar order by skor DESC";
$sorgusonuc=$conn->query($sorgu);
if($sorgusonuc->num_rows>0){
$butunsatirlar=array();
while($satirlar=$sorgusonuc->fetch_object()){
	
	array_push($butunsatirlar,array(
	"KullaniciNick"=>$satirlar->nick,
	"KullaniciSkor"=>$satirlar->skor
	)
	);
}
 echo json_encode(array("butunSkorlar"=>$butunsatirlar));
}
}



else if($_POST['unity']=='sorular2'){
	
	$sorgu="select * from sorularzor";
$sorgusonuc=$conn->query($sorgu);
if($sorgusonuc->num_rows>0){
$butunsatirlar=array();
while($satirlar=$sorgusonuc->fetch_object()){
	
	array_push($butunsatirlar,array(
	"soru"=>$satirlar->soru,
	"a_cevap"=>$satirlar->a_cevap,
	"b_cevap"=>$satirlar->b_cevap,
	"c_cevap"=>$satirlar->c_cevap,
	"d_cevap"=>$satirlar->d_cevap,
	"dogrucevap"=>$satirlar->dogrucevap,
	"saniye"=>$satirlar->saniye
	)
	);
}
 echo json_encode(array("butunSorular2"=>$butunsatirlar));
}
}

else if($_POST['unity']=='sorular1'){
	
	$sorgu="select * from sorularorta";
$sorgusonuc=$conn->query($sorgu);
if($sorgusonuc->num_rows>0){
$butunsatirlar=array();
while($satirlar=$sorgusonuc->fetch_object()){
	
	array_push($butunsatirlar,array(
	"soru"=>$satirlar->soru,
	"a_cevap"=>$satirlar->a_cevap,
	"b_cevap"=>$satirlar->b_cevap,
	"c_cevap"=>$satirlar->c_cevap,
	"d_cevap"=>$satirlar->d_cevap,
	"dogrucevap"=>$satirlar->dogrucevap,
	"saniye"=>$satirlar->saniye
	)
	);
}
 echo json_encode(array("butunSorular1"=>$butunsatirlar));
}
}

else if($_POST['unity']=='sorular3'){
	
	$sorgu="select * from sorulargenel";
$sorgusonuc=$conn->query($sorgu);
if($sorgusonuc->num_rows>0){
$butunsatirlar=array();
while($satirlar=$sorgusonuc->fetch_object()){
	
	array_push($butunsatirlar,array(
	"soru"=>$satirlar->soru,
	"a_cevap"=>$satirlar->a_cevap,
	"b_cevap"=>$satirlar->b_cevap,
	"c_cevap"=>$satirlar->c_cevap,
	"d_cevap"=>$satirlar->d_cevap,
	"dogrucevap"=>$satirlar->dogrucevap,
	"saniye"=>$satirlar->saniye
	)
	);
}
 echo json_encode(array("butunSorular3"=>$butunsatirlar));
}
}
$conn->close();

?>