<?php
error_reporting(0);
                        //hostadı //adminadı//adminsifre //db
$baglanti = new mysqli("localhost","root","","unity_oyun");

// Check connection
if ($baglanti->connect_error) {
  echo"Connection failed: " . $baglanti->connect_error;
}


if($_POST['unity']=="kayitOlma"){

  $kullaniciAdi = $_POST['kullaniciAdi'];   //
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama
  $tarih = date('y-m-d');     
  
  //$kulad=htmlspecialchars(strip_tags($_POST["kullaniciAdi"]));  
  //$sifre=md5(sha1(md5(sha1(htmlspecialchars(strip_tags($_POST["sifre"]))))));  MD5 şifreleme
  //$mail=htmlspecialchars(strip_tags($_POST["mail"]));


  $sorgu = "insert into kayitlar(kul_adi,password,kayitTarihi) values ('$kullaniciAdi','$sifre','$tarih')";

  $sorgusonucu = $baglanti -> query($sorgu);

  if ($sorgusonucu)
  {
    echo "kayıt başarılı";

  }else
  {   //echo"lütfen farklı bir kullanıcı adı seçiniz";
    //if($baglanti->errno==1062)
    //{
        //echo"lütfen farklı bir kullanıcı adı seçiniz";
    //}else{
   // echo"başka hata";
   // }
  
  }
    

}



if($_POST['unity']=="girisYapma"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1(htmlspecialchars(strip_tags($_POST["sifre"]))))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "select * from kayitlar where kul_adi='$kullaniciAdi' and password='$sifre' ";

  $sorgusonucu = $baglanti->query($sorgu);

  if($sorgusonucu-> num_rows>0){

    echo "giriş başarılı"; 
  }
  else{

    echo "giriş başarısız! kullanıcıadı veya şifre hatalı";

   }
}

if($_POST['unity']=="Puan_cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT puan  FROM `skorlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu1 = $baglanti->query($sorgu);
  //$sorgusonucu = mysqli_query($baglanti , $sorgu)->fetch(PDO::FETCH_ASSOC);


  if($sorgusonucu1->num_rows>0 ){

  
    $srow = mysqli_fetch_assoc($sorgusonucu1);

    echo $srow["puan"];

  }
  else{

    echo "başarısız";

   }
}
if($_POST['unity']=="Altin_cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT altin_toplam  FROM `skorlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu2 = $baglanti->query($sorgu);
  


  if($sorgusonucu2->num_rows>0 ){

   
    $srow = mysqli_fetch_assoc($sorgusonucu2);

    echo $srow["altin_toplam"];

  }
  else{

    echo "başarısız";

   }
}

if($_POST['unity']=="Elmas_cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT elmas  FROM `skorlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu3 = $baglanti->query($sorgu);
  


  if($sorgusonucu3->num_rows>0 ){

   
    $srow = mysqli_fetch_assoc($sorgusonucu3);

    echo $srow["elmas"];

  }
  else{

    echo "başarısız";

   }
}
if($_POST['unity']=="ilk_skorlar_ekleme"){  // veritabanına güncellenmiş şekilde ekler
	$kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

	$puan = 0;
	$toplamaltin =0;
	$top_elmas =0;


	//$sorgu = " INSERT INTO skorlar(id ,puan, altin_toplam,elmas) VALUES((select id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ),$puan, $toplamaltin, $top_elmas) ";
  $sorgu = "INSERT INTO skorlar(id ,puan, altin_toplam,elmas)   
  SELECT id, 0, 0, 0 FROM kayitlar WHERE kul_adi='$kullaniciAdi' and password ='$sifre' "; //ilk skorlar ekler

  $sorgu2 = "INSERT INTO silahlar(id ,ak47, m416, m16a4 ,ump45) 
  SELECT id, 0, 0, 0,1 FROM kayitlar WHERE kul_adi='$kullaniciAdi' and password ='$sifre' ";//ilk silahlar ekler sadece ump45 olacak
  $sorgu3 = "INSERT INTO bolum2(id ,gecti) 
  SELECT id,0 FROM kayitlar WHERE kul_adi='$kullaniciAdi' and password ='$sifre' ";//ilk bolum 2 kilitli olarak gösterecek

	$sorgusonucu4 = $baglanti->query($sorgu);
  $sorgusonucu41 = $baglanti->query($sorgu2);
  $sorgusonucu42 = $baglanti->query($sorgu3);

	
	if($sorgusonucu4){
		echo "İLK skorlar ve ilk silahlar kayit etme başarılı :";
	}
	else{
		echo "İLK skorlar ve ilk silahlar kayit etme başarısız";
	}



}
if($_POST['unity']=="skorlar_ekleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $puan = (int)$_POST['puan'];
  $toplamaltin = (int)$_POST['toplamAltin'];
  $top_elmas = (int)$_POST['top_elmas'];


  $sorgu = " UPDATE  skorlar SET puan=puan+$puan, altin_toplam=altin_toplam+$toplamaltin, elmas=elmas+$top_elmas WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu4 = $baglanti->query($sorgu);
  


  if($sorgusonucu4){
    echo "skorlar kayit etme başarılı :";
  }
  else{
    echo "skorlar kayit etme başarısız";
  }
}


if($_POST['unity']=="AltinGuncelleme_ekleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $toplamaltin =$_POST['DusecekAltinmiktari'];



  $sorgu = " UPDATE  skorlar SET  altin_toplam=altin_toplam-$toplamaltin WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5 = $baglanti->query($sorgu);
  


  if($sorgusonucu5){
    echo "alTİN Güncelleme Başarılı";
  }
  else{
    echo "alTİN Güncelleme başarısız";
  }
}




if($_POST['unity']=="ElmasGuncelleme_ekleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $toplamElmas =$_POST['DusecekElmas_miktari'];



  $sorgu = " UPDATE  skorlar SET  elmas=elmas-$toplamElmas WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5 = $baglanti->query($sorgu);
  


  if($sorgusonucu5){
    echo "elmas Güncelleme Başarılı";
  }
  else{
    echo "elmas Güncelleme başarısız";
  }
}



if($_POST['unity']=="donusturme1_altin_elmas_guncelleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $toplamElmas =$_POST['Dusecekelmasmiktari'];



  $sorgu = " UPDATE  skorlar SET  elmas=elmas-$toplamElmas,altin_toplam=altin_toplam+10000 WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5 = $baglanti->query($sorgu);
  


  if($sorgusonucu5){
    echo "Dönüştürme1 elmas-altin Güncelleme Başarılı";
  }
  else{
    echo "Dönüştürme1 elmas-altin Güncelleme Başarısız !";
  }
}



if($_POST['unity']=="donusturme2_altin_elmas_guncelleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $toplamElmas =$_POST['Dusecekelmasmiktari'];



  $sorgu = " UPDATE  skorlar SET  elmas=elmas-$toplamElmas,altin_toplam=altin_toplam+1000 WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5 = $baglanti->query($sorgu);
  


  if($sorgusonucu5){
    echo "Dönüştürme2 elmas-altin Güncelleme Başarılı";
  }
  else{
    echo "Dönüştürme2 elmas-altin Güncelleme Başarısız !";
  }
}


if($_POST['unity']=="donusturme3_altin_elmas_guncelleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $toplamaltinx =$_POST['Dusecek_Altin_miktari'];



  $sorgu = " UPDATE  skorlar SET  elmas=elmas+200,altin_toplam=altin_toplam-5000 WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5 = $baglanti->query($sorgu);
  


  if($sorgusonucu5){
    echo "Dönüştürme3 elmas-altin Güncelleme Başarılı";
  }
  else{
    echo "Dönüştürme3 elmas-altin Güncelleme Başarısız !";
  }
}

if($_POST['unity']=="YeniBolumKilitAcma"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  $sorgu = " UPDATE  bolum2 SET  gecti=1 WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu5x = $baglanti->query($sorgu);
  

  if($sorgusonucu5x){
    echo "Yeni bölüm kilit açma Güncelleme Başarılı";
  }
  else{
    echo "Yeni bölüm kilit açma Güncelleme Başarısız !";
  }
}


if($_POST['unity']=="silahSatinAlma_Guncelleme"){  // veritabanına güncellenmiş şekilde ekler


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama


  if($_POST['ak47']!=""){ //ak47 gönderilmiş ise yani satın alınmışsa 

    $sorgu = " UPDATE  silahlar SET ak47=1  WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";
    $silah='ak47';
  } 
  else if($_POST['m416']!=""){//m416 gönderilmiş ise yani satın alınmışsa 

    $sorgu = " UPDATE  silahlar SET m416=1   WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";
    $silah='m416';
  }
  else if($_POST['m16a4']!=""){//m16a4 gönderilmiş ise yani satın alınmışsa 

    $sorgu = " UPDATE  silahlar SET m16a4=1  WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";
    $silah='m16a4';
  }   

  $sorgusonucu5 = $baglanti->query($sorgu);
  
  if($sorgusonucu5){
    echo $silah." silahı satın alma Güncelleme başarılı ";
  }
  else{
    echo "silahı satın alma  Güncelleme Başarısız !";
  }
}

if($_POST['unity']=="Silah1Cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT ak47  FROM `silahlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu1 = $baglanti->query($sorgu);

  if($sorgusonucu1->num_rows>0 ){

  
    $srow = mysqli_fetch_assoc($sorgusonucu1);

    echo $srow["ak47"];

  }
  else{

    echo "başarısız";

   }
}

if($_POST['unity']=="Silah2Cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT m416  FROM `silahlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu1 = $baglanti->query($sorgu);

  if($sorgusonucu1->num_rows>0 ){

  
    $srow = mysqli_fetch_assoc($sorgusonucu1);

    echo $srow["m416"];

  }
  else{

    echo "başarısız";

   }
}

if($_POST['unity']=="Silah3Cekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT m16a4  FROM `silahlar` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu1 = $baglanti->query($sorgu);

  if($sorgusonucu1->num_rows>0 ){

  
    $srow = mysqli_fetch_assoc($sorgusonucu1);

    echo $srow["m16a4"];

  }
  else{

    echo "başarısız";

   }
}

if($_POST['unity']=="YeniBolumKilitCekme"){


  $kullaniciAdi = $_POST['kullaniciAdi'];
  $sifre =md5(sha1(md5(sha1($_POST["sifre"]))));  //MD5 ve Sha1 ile şifrelenmiş şifreyi atama

  $sorgu = "SELECT gecti  FROM `bolum2` WHERE id=(select id from kayitlar where  kul_adi='$kullaniciAdi' and password ='$sifre' ) ";

  $sorgusonucu1 = $baglanti->query($sorgu);

  if($sorgusonucu1->num_rows>0 ){

  
    $srow = mysqli_fetch_assoc($sorgusonucu1);

    echo $srow["gecti"];

  }
  else{

    echo "başarısız";

   }
}

?>