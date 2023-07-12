using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu_paneli : MonoBehaviour
{   
    AudioSource sesKaynak; // audiosource ye default olarak menu sesi attýk sahne kýsmýnda ve sürekli çalacak diye ayarladýk
    public AudioClip SatýnAlmaSesi;
    public AudioClip ClickSesi;

    public TextMeshProUGUI puan, toplam_altin ,elmas  ,satinalmamesaj1, satinalmamesaj2, satinalmamesaj3 ,altin1_Mesaj, altin2_Mesaj,elmas_mesaj;
    public TextMeshProUGUI kullaniciadi_txt_hosgeldin;
    public TextMeshProUGUI market_btn ,Cevirme_btn;
    public GameObject marketpaneli,Conver_Paneli;
    public GameObject kareker1_satinAlmaPaneli, kareker2_satinAlmaPaneli, kareker3_satinAlmaPaneli, kareker4_satinAlmaPaneli;
    public GameObject altin1_satinalmapaneli, altin2_satinalmapaneli, elmas_satinalmapaneli;


    public string kuladi;
    public string sifre;

    public int  toplamAltin,toplamElmas;

    public static bool bolum1;

    public GameObject ak47_satin_almabtn, m416_satin_almabtn, m16a4_satin_almabtn, ak47_satin_alýndi_btn, m416_satin_alýndibtn, m16a4_satin_alýndibtn;
    public  string a, b, c;
    public GameObject kilitÝmg;
    private string Bolumgecti;
    
    
    public void Start()
    {
        
        sesKaynak = gameObject.GetComponent<AudioSource>();

         // Veriyi al
        kuladi = Login.V_kullanici_degeri;
        sifre = Login.V_sifre_degeri;

        PlayerPrefs.DeleteKey("Altin_verisi"); // deðerleri siler 
        PlayerPrefs.DeleteKey("Puan_verisi");
        PlayerPrefs.DeleteKey("Elmas_verisi");
       
        // puan.text = kuladi;
        Debug.Log(kuladi);
        Debug.Log(sifre);
        kullaniciadi_txt_hosgeldin.text = kuladi;

        //skor iþlemleri skor çekme iþlemi
        StartCoroutine(puan_cekme());
        StartCoroutine(toplam_altin_cekme());
        StartCoroutine(toplam_elmas_cekme());
    }
     
    void Update()
    {

        
        StartCoroutine(puan_cekme());
        StartCoroutine(toplam_altin_cekme());
        StartCoroutine(toplam_elmas_cekme());
        StartCoroutine(Silah1Cekme());
        StartCoroutine(Silah2Cekme());
        StartCoroutine(Silah3Cekme());

        StartCoroutine(bolumKilidiAcmaCekmeÝslemi());

        if (a == "1")
        {
            ak47_satin_almabtn.SetActive(false);
            ak47_satin_alýndi_btn.SetActive(true);
            
        }
        if (b == "1")
        {
            m416_satin_almabtn.SetActive(false);
            m416_satin_alýndibtn.SetActive(true);
            
        }
        if (c == "1")
        {
            m16a4_satin_almabtn.SetActive(false);
            m16a4_satin_alýndibtn.SetActive(true);
           
        }
        if (Bolumgecti == "1")
        {
            kilitÝmg.SetActive(false);
        }
       
    }
    public void karekter1_satin_AL()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        kareker1_satinAlmaPaneli.SetActive(true); // karekter1 satýnalma paneli açar
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
    }
    
    public void karekter2_satin_AL()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        kareker2_satinAlmaPaneli.SetActive(true);// karekter2 satýnalma paneli açar
        kareker1_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
    }
    public void karekter3_satin_AL()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        kareker3_satinAlmaPaneli.SetActive(true); // karekter3 satýnalma paneli açar
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker1_satinAlmaPaneli.SetActive(false);
    }
    public void karekter4_satin_AL()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        kareker4_satinAlmaPaneli.SetActive(true); // karekter4 satýnalma paneli açar
        kareker3_satinAlmaPaneli.SetActive(false); 
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker1_satinAlmaPaneli.SetActive(false);
    }

    public void hayir_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        kareker1_satinAlmaPaneli.SetActive(false); 
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
        kareker4_satinAlmaPaneli.SetActive(false);
    }
    public void karekterpanelleri_kapat()
    {

        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar

        kareker1_satinAlmaPaneli.SetActive(false);
        kareker2_satinAlmaPaneli.SetActive(false);
        kareker3_satinAlmaPaneli.SetActive(false);
        kareker4_satinAlmaPaneli.SetActive(false);

        altin1_satinalmapaneli.SetActive(false);
        altin2_satinalmapaneli.SetActive(false);
        elmas_satinalmapaneli.SetActive(false);       
    }

    public IEnumerator bolumKilidiAcmaCekmeÝslemi()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "YeniBolumKilitCekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                a = www.downloadHandler.text;
                Bolumgecti = a;
            }
        }
    }
    public void karekter1_evet_btn()
    {
        if (toplamAltin >= 5000)
        {
            StartCoroutine(AltinGuncelleme_ekleme(5000));
            StartCoroutine(SilahlarGuncelleme_ekleme(1)); //akm47 old. belirtmek için

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            satinalmamesaj1.text = "ak47 silahý Satýn Alýndý .";
            Debug.Log("*** ak47 satin alindi ***");
        }
        else
        {
            satinalmamesaj1.text = "ak47 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" silah satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }

        // karekter2_buy = false;
    }
    public void karekter2_evet_btn()
    {
        if (toplamElmas >= 200)
        {
            StartCoroutine(ElmasGuncelleme_ekleme());
            StartCoroutine(SilahlarGuncelleme_ekleme(2));//m416 old. belirtmek için

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar       

            satinalmamesaj2.text = "m416 silahý Satýn Alýndý .";
            Debug.Log("*** m416 satin alindi ***");
        }
        else
        {
            satinalmamesaj2.text = "m416 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" m416 satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }
    }

    public void karekter3_evet_btn()
    {
        if (toplamAltin >= 5000)
        {
            StartCoroutine(AltinGuncelleme_ekleme(5000));
            StartCoroutine(SilahlarGuncelleme_ekleme(3));//m16a4 old. belirtmek için

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            satinalmamesaj3.text = "m16a4 silah Satýn Alýndý .";
            Debug.Log("*** m16a4 satin alindi ***");
        }
        else
        {
            satinalmamesaj3.text = "m16a4 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" m16a4 satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }
    }
    public void karekter4_evet_btn()
    {
        if (toplamAltin >= 2000)
        {
            StartCoroutine(AltinGuncelleme_ekleme(2000));

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            satinalmamesaj3.text = "Ump-45 silahý Satýn Alýndý .";
            Debug.Log("*** Ump-45 satin alindi ***");
        }
        else
        {
            satinalmamesaj3.text = "ump-45 satýn alma baþarýsýz ! altýnýnýz yetersiz!";
            Debug.Log(" Ump-45 satýn alma baþarýsýz ! altýnýnýz yetersiz!");
        }
    }
    public void altina_cevir1_evet_btn()
    {
        if (toplamElmas >= 500)
        {
            StartCoroutine(donusturme1_altin_elmas_guncelleme());

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            altin1_Mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme1 baþarýlý ***");
        }
        else
        {
            altin1_Mesaj.text = "Dönüþtürme baþarýsýz ! elmasýnýz yetersiz!";
            Debug.Log(" Dönüþtürme1 baþarýsýz ! elmasýnýz yetersiz!");
        }
    }
    public void altina_cevir2_evet_btn()
    {
        if (toplamElmas >= 100)
        {
            StartCoroutine(donusturme2_altin_elmas_guncelleme());

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            altin2_Mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme2 baþarýlý ***");
        }
        else
        {
            altin2_Mesaj.text = "Dönüþtürme baþarýsýz ! elmasýnýz yetersiz!";
            Debug.Log(" Dönüþtürme2 baþarýsýz ! elmasýnýz yetersiz!");
        }
    }
    public void elmasa_cevir1_evet_btn()
    {
        if (toplamAltin >= 5000)
        {
            StartCoroutine(Donusturme3_altin_elmas_guncelleme());

            sesKaynak.PlayOneShot(SatýnAlmaSesi);//1 kere sesi çalar

            elmas_mesaj.text = "Dönüþtürme baþarýlý ";
            Debug.Log("*** Dönüþtürme3 baþarýlý ***");
        }
        else
        {
            elmas_mesaj.text = "Dönüþtürme baþarýsýz ! altinýnýz yetersiz!";
            Debug.Log(" Dönüþtürme3 baþarýsýz ! altýnýnýz yetersiz!");
        }
    }
    IEnumerator SilahlarGuncelleme_ekleme(int sayi)
    {        
        WWWForm form = new WWWForm();
        form.AddField("unity", "silahSatinAlma_Guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        if (sayi == 1)
        {
            form.AddField("ak47",1);
        }
        else if (sayi == 2)
        {
            form.AddField("m416",1);
        }
        else if (sayi == 3)
        {
            form.AddField("m16a4",1);
        }

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {                  
                Debug.Log("Silahý satýn alma  sonucu :" + www.downloadHandler.text);              
            }
        }
    }
    IEnumerator Silah1Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah1Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);
         
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {               
                a = www.downloadHandler.text;
            }
        }
    }
    IEnumerator Silah2Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah2Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                b = www.downloadHandler.text;
            }
        }
    }
    IEnumerator Silah3Cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Silah3Cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                c = www.downloadHandler.text;
            }
        }
    }
    IEnumerator donusturme1_altin_elmas_guncelleme() 
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme1_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecekelmasmiktari", 500);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme1 sonucu :" + www.downloadHandler.text);
            }
        }
    }

    IEnumerator donusturme2_altin_elmas_guncelleme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme2_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecekelmasmiktari", 100);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme2 sonucu :" + www.downloadHandler.text);
            }
        }
    }

    IEnumerator Donusturme3_altin_elmas_guncelleme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "donusturme3_altin_elmas_guncelleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("Dusecek_Altin_miktari", 5000);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("donusturme3 sonucu :" + www.downloadHandler.text);
            }
        }
    }

    public void cevirme_Paneli_AC_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        Conver_Paneli.SetActive(true);  
    }
    public void Karekter_Paneli_AC_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        marketpaneli.SetActive(true);
        Conver_Paneli.SetActive(false);
    }

    public void market_paneli()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar     
        market_btn.color = Color.red;
        marketpaneli.SetActive(true);       
    }
    public void market_paneli_kapat()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        marketpaneli.SetActive(false);
        Conver_Paneli.SetActive(false);     
        market_btn.color = Color.yellow;   
    }
  
    IEnumerator puan_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Puan_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string a = www.downloadHandler.text;
                if (a!= "baþarýsýz")
                {
                    puan.text = a.ToString();
                }              
            }
        }
    }

    IEnumerator toplam_altin_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Altin_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string  b = www.downloadHandler.text;
                if (b!= "baþarýsýz")
                {
                    toplam_altin.text = b.ToString();
                    toplamAltin = Convert.ToInt32(b); // toplanan altin deðeri  satýn alma panelinde kullanmak için
                }            
            }
        }
    }

    IEnumerator toplam_elmas_cekme()
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "Elmas_cekme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
               // Debug.Log("Sorgu Sonucu:" + www.downloadHandler.text);
                string c = www.downloadHandler.text;
                if (c!= "baþarýsýz")
                {
                    elmas.text = c.ToString();
                    toplamElmas = Convert.ToInt32(c);
                }                        
            }
        }
    }

    IEnumerator AltinGuncelleme_ekleme(int sayi) // Karekter satýn alýndýktan sonra top altýn günceller
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "AltinGuncelleme_ekleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);
      
        form.AddField("DusecekAltinmiktari", sayi);
     
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Karekter satin alma sonucu :" + www.downloadHandler.text);
            }
        }
    }

    IEnumerator ElmasGuncelleme_ekleme() // Karekter satýn alýndýktan sonra top elmas günceller
    {
        WWWForm form = new WWWForm();
        form.AddField("unity", "ElmasGuncelleme_ekleme");
        form.AddField("kullaniciAdi", kuladi);
        form.AddField("sifre", sifre);

        form.AddField("DusecekElmas_miktari", 200);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/Unity_DB/user.php", form))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Karekter satin alma sonucu :" + www.downloadHandler.text);
            }
        }
    }
  
    public void bolum1_basla()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Bolum1");
        bolum1 = true;
        Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
    }
    public void bolum2_basla()
    {
        if (Bolumgecti == "0") //bolum geçilmedi ise açmaz
        {
            Debug.Log("Bölüm2 Kilitli !");
        }
        else  // 1. bolüm geçildi ise açar
        {
            sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("bolum2");
            bolum1 = false;
            Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
        }
        
    }
    public void loginegit()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        SceneManager.LoadScene("login_scene");
    }
    public void Exit()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        Application.Quit();
    }

    public void ses_kapat()
    {
       
        if (sesKaynak.enabled==true)
        {
            sesKaynak.enabled = false;      
        }
        else
        {
            sesKaynak.enabled = true;
        }       
    }

    public void gold1_satinal_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        altin1_satinalmapaneli.SetActive(true);
    }
    public void gold2_satinal_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        altin2_satinalmapaneli.SetActive(true);
    }
    public void elmas_satinal_btn()
    {
        sesKaynak.PlayOneShot(ClickSesi);//1 kere click sesi çalar
        elmas_satinalmapaneli.SetActive(true);
    }
}
