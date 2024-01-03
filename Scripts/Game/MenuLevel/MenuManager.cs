using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour 
{   
    [SerializeField]
    private TextMeshProUGUI puanTxt, altinTxt, elmasTxt, silahSatinAlmaMesajTxt1, silahSatinAlmaMesajTxt2, silahSatinAlmaMesajTxt3, altin1_MesajTxt, altin2_MesajTxt, elmas_mesajTxt;

    [SerializeField]
    private TextMeshProUGUI kullaniciadi_txt;

    [SerializeField]
    private TextMeshProUGUI market_btnTxt, Cevirme_btnTxt;

    [SerializeField]
    private GameObject marketpaneli, Conver_Paneli;

    [SerializeField]
    private GameObject Silah1_satinAlmaPaneli, Silah2_satinAlmaPaneli, Silah3_satinAlmaPaneli;

    [SerializeField]
    private GameObject altin1_satinalmapaneli, altin2_satinalmapaneli, elmas_satinalmapaneli;

    public string kuladi;
    public string sifre;
    public static bool bolum1;
    private string Bolumgecti;
    public string a, b, c, X;

    [SerializeField]
    private int toplamAltin, toplamElmas;

    [SerializeField]
    private GameObject ak47_satin_almabtn, m416_satin_almabtn, m16a4_satin_almabtn, ak47_satin_alındi_btn, m416_satin_alındibtn, m16a4_satin_alındibtn;
       
    [SerializeField]
    private GameObject kilitİmg;

    SoundManager soundManager;

    [SerializeField]
    private bool isOnline;

    [SerializeField]
    private Button onlineOflineBtn;

    [SerializeField]
    private Button level2Btn;

    private void Awake()
    {
        soundManager = Object.FindObjectOfType<SoundManager>();
    }

    public void Start()
    {
        level2Btn.GetComponent<Button>().interactable = false;

        isOnline = false;

        //Veriyi al       
        kuladi = Login.V_kullanici_degeri;
        sifre = Login.V_sifre_degeri;

        PlayerPrefs.DeleteKey("Altin_verisi"); // değerleri siler 
        PlayerPrefs.DeleteKey("Puan_verisi");
        PlayerPrefs.DeleteKey("Elmas_verisi");

        Debug.Log(kuladi);
        Debug.Log(sifre);

        kullaniciadi_txt.text = kuladi;

        if (isOnline)
        {
            SkorlariCek(); //skor işlemleri skor çekme işlemleri           
        }         
    }

    public void OnlineOflineBtn()
    {
        isOnline = !isOnline;

        if (isOnline)
        {
            SkorlariCek();
            onlineOflineBtn.GetComponent<Image>().color = Color.green;
        }
        else
        {
            onlineOflineBtn.GetComponent<Image>().color = Color.red;
        }
    }
    void SkorlariCek()
    {
        StartCoroutine(puan_cekme());
        StartCoroutine(toplam_altin_cekme());
        StartCoroutine(toplam_elmas_cekme());
        StartCoroutine(Silah1Cekme());
        StartCoroutine(Silah2Cekme());
        StartCoroutine(Silah3Cekme());
        StartCoroutine(bolumKilidiAcmaCekmeİslemi());
    }

    public void SilahSatinAl_EvetBtn(int silahId)//int silahFiyati,int silahId
    {
        if (isOnline)
        {
            if (silahId == 1 || silahId == 3)
            {
                if (toplamAltin >= 5000)
                {
                    StartCoroutine(AltinGuncelleme_ekleme(5000));
                    StartCoroutine(SilahlarGuncelleme_ekleme(silahId)); //akm47 old. belirtmek için

                    soundManager.SatinAlmaSesiCal();//1 kere sesi çalar                   

                    if (silahId == 1)
                    {
                        silahSatinAlmaMesajTxt1.text = "ak47 silahı Satın Alındı .";
                        SkorlariCek();

                    }
                    else
                    {
                        silahSatinAlmaMesajTxt3.text = "m16-A4 silahı Satın Alındı .";
                        SkorlariCek();
                    }                  
                }
                else
                {
                    silahSatinAlmaMesajTxt1.text = "satın alma başarısız ! altınınız yetersiz!";
                    silahSatinAlmaMesajTxt3.text = "satın alma başarısız ! altınınız yetersiz!";
                }
            }
            else if (silahId == 2)
            {
                if (toplamElmas >= 200)
                {
                    StartCoroutine(ElmasGuncelleme_ekleme());
                    StartCoroutine(SilahlarGuncelleme_ekleme(silahId));//m416 old. belirtmek için

                    soundManager.SatinAlmaSesiCal();//1 kere sesi çalar       

                    silahSatinAlmaMesajTxt2.text = "m416 silahı Satın Alındı .";
                    Debug.Log("*** m416 satin alindi ***");
                    SkorlariCek();
                }
                else
                {
                    silahSatinAlmaMesajTxt2.text = "m416 satın alma başarısız ! altınınız yetersiz!";
                    Debug.Log(" m416 satın alma başarısız ! altınınız yetersiz!");
                }
            }
        }     
    }

    public void SilahSatinAl(int silahNo)
    {
        if (silahNo ==1)
        {
            Silah1_satinAlmaPaneli.SetActive(true); // karekter1 satınalma paneli açar
            Silah2_satinAlmaPaneli.SetActive(false);
            Silah3_satinAlmaPaneli.SetActive(false);
        }
        else if (silahNo == 2)
        {
            Silah2_satinAlmaPaneli.SetActive(true);// karekter2 satınalma paneli açar
            Silah1_satinAlmaPaneli.SetActive(false);
            Silah3_satinAlmaPaneli.SetActive(false);
        }
        else if (silahNo == 3)
        {
            Silah3_satinAlmaPaneli.SetActive(true); // karekter3 satınalma paneli açar
            Silah2_satinAlmaPaneli.SetActive(false);
            Silah1_satinAlmaPaneli.SetActive(false);
        }       
    }

    public void hayir_btn()
    {
        Silah1_satinAlmaPaneli.SetActive(false);
        Silah2_satinAlmaPaneli.SetActive(false);
        Silah3_satinAlmaPaneli.SetActive(false);
    }
    public void karekterpanelleri_kapat()
    {
        Silah1_satinAlmaPaneli.SetActive(false);
        Silah2_satinAlmaPaneli.SetActive(false);
        Silah3_satinAlmaPaneli.SetActive(false);

        altin1_satinalmapaneli.SetActive(false);
        altin2_satinalmapaneli.SetActive(false);
        elmas_satinalmapaneli.SetActive(false);
    }
    
    public void altina_cevir1_evet_btn()
    {
        if (isOnline)
        {
            if (toplamElmas >= 500)
            {
                StartCoroutine(donusturme1_altin_elmas_guncelleme());

                soundManager.SatinAlmaSesiCal();//1 kere sesi çalar

                altin1_MesajTxt.text = "Dönüştürme başarılı ";
                Debug.Log("*** Dönüştürme1 başarılı ***");
            }
            else
            {
                altin1_MesajTxt.text = "Dönüştürme başarısız ! elmasınız yetersiz!";
                Debug.Log(" Dönüştürme1 başarısız ! elmasınız yetersiz!");
            }
        }     
    }
    public void altina_cevir2_evet_btn()
    {
        if (isOnline)
        {
            if (toplamElmas >= 100)
            {
                StartCoroutine(donusturme2_altin_elmas_guncelleme());

                soundManager.SatinAlmaSesiCal();//1 kere sesi çalar

                altin2_MesajTxt.text = "Dönüştürme başarılı ";
                Debug.Log("*** Dönüştürme2 başarılı ***");
            }
            else
            {
                altin2_MesajTxt.text = "Dönüştürme başarısız ! elmasınız yetersiz!";
                Debug.Log(" Dönüştürme2 başarısız ! elmasınız yetersiz!");
            }
        }      
    }
    public void elmasa_cevir1_evet_btn()
    {
        if (isOnline)
        {
            if (toplamAltin >= 5000)
            {
                StartCoroutine(Donusturme3_altin_elmas_guncelleme());

                soundManager.SatinAlmaSesiCal();//1 kere sesi çalar

                elmas_mesajTxt.text = "Dönüştürme başarılı ";
                Debug.Log("*** Dönüştürme3 başarılı ***");
            }
            else
            {
                elmas_mesajTxt.text = "Dönüştürme başarısız ! altinınız yetersiz!";
                Debug.Log(" Dönüştürme3 başarısız ! altınınız yetersiz!");
            }
        }       
    }

    public IEnumerator bolumKilidiAcmaCekmeİslemi()
    {
        if (isOnline)
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
                    X = www.downloadHandler.text;
                   
                    PlayerPrefs.SetInt("level1Gectimi",int.Parse(X));

                    if (PlayerPrefs.GetInt("level1Gectimi") == 1)
                    {
                        kilitİmg.SetActive(false);
                        level2Btn.GetComponent<Button>().interactable = true;
                    }
                    else
                    {                       
                        kilitİmg.SetActive(true);
                    }

                    www.Dispose();
                }
            }
        }     
    }

    IEnumerator puan_cekme()
    {
        if (isOnline)
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
                    if (a != "başarısız" && !string.IsNullOrEmpty(a))
                    {
                        puanTxt.text = a.ToString();
                    }
                    else
                    {
                        // Boş veya "başarısız" durumu
                        Debug.LogWarning("Sorgu Sonucu Boş veya Başarısız: " + c);
                    }

                    www.Dispose();
                }
            }
        }
    }
    IEnumerator toplam_altin_cekme()
    {
        if (isOnline)
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
                    string b = www.downloadHandler.text;
                    if (b != "başarısız" && !string.IsNullOrEmpty(b))
                    {
                        altinTxt.text = b.ToString();
                        toplamAltin = int.Parse(b); // toplanan altin değeri  satın alma panelinde kullanmak için
                    }
                    else
                    {
                        // Boş veya "başarısız" durumu
                        Debug.LogWarning("Sorgu Sonucu Boş veya Başarısız: " + c);
                    }
                    www.Dispose();
                }
            }
        }
    }
    IEnumerator toplam_elmas_cekme()
    {
        if (isOnline)
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
                    if (!string.IsNullOrEmpty(c) && c != "başarısız")
                    {
                        elmasTxt.text = c.ToString();
                        toplamElmas = int.Parse(c);
                    }
                    else
                    {
                        // Boş veya "başarısız" durumu
                        Debug.LogWarning("Sorgu Sonucu Boş veya Başarısız: " + c);
                    }
                    www.Dispose();
                }
            }
        }
    }
    IEnumerator Silah1Cekme()
    {
        if (isOnline) 
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
                    PlayerPrefs.SetInt("silahalindimi1",int.Parse(a));
                    if (PlayerPrefs.GetInt("silahalindimi1") ==1)
                    {
                        ak47_satin_almabtn.SetActive(false);
                        ak47_satin_alındi_btn.SetActive(true);
                    }
                   
                    www.Dispose();
                }
            }
        }     
    }
    IEnumerator Silah2Cekme()
    {
        if (true)
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

                    PlayerPrefs.SetInt("silahalindimi2", int.Parse(b));
                    if (PlayerPrefs.GetInt("silahalindimi2") == 1)
                    {
                        m416_satin_almabtn.SetActive(false);
                        m416_satin_alındibtn.SetActive(true);
                    }
                    www.Dispose();
                }
            }
        }       
    }
    IEnumerator Silah3Cekme()
    {
        if (isOnline)
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

                    PlayerPrefs.SetInt("silahalindimi3", int.Parse(c));
                    if (PlayerPrefs.GetInt("silahalindimi3") == 1)
                    {
                        m16a4_satin_almabtn.SetActive(false);
                        m16a4_satin_alındibtn.SetActive(true);
                    }

                    www.Dispose();
                }
            }
        }       
    }

    IEnumerator donusturme1_altin_elmas_guncelleme()
    {
        if (isOnline)
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
                    www.Dispose();
                }
            }
        }      
    }
    IEnumerator donusturme2_altin_elmas_guncelleme()
    {
        if (isOnline)
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
                    www.Dispose();
                }
            }
        }    
    }
    IEnumerator Donusturme3_altin_elmas_guncelleme()
    {
        if (isOnline)
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
                    www.Dispose();
                }
            }
        }  
    }
  
    IEnumerator AltinGuncelleme_ekleme(int sayi) // Karekter satın alındıktan sonra top altın günceller
    {
        if (isOnline)
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
                    www.Dispose();
                }               
            }
        }      
    }
    IEnumerator ElmasGuncelleme_ekleme() // Karekter satın alındıktan sonra top elmas günceller
    {
        if (isOnline)
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
                    www.Dispose();
                }
            }
        }        
    }
    IEnumerator SilahlarGuncelleme_ekleme(int sayi)
    {
        if (isOnline)
        {
            WWWForm form = new WWWForm();
            form.AddField("unity", "silahSatinAlma_Guncelleme");
            form.AddField("kullaniciAdi", kuladi);
            form.AddField("sifre", sifre);

            if (sayi == 1)
            {
                form.AddField("ak47", 1);
            }
            else if (sayi == 2)
            {
                form.AddField("m416", 1);
            }
            else if (sayi == 3)
            {
                form.AddField("m16a4", 1);
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
                    Debug.Log("Silahı satın alma  sonucu :" + www.downloadHandler.text);
                    StartCoroutine(Silah1Cekme());
                    StartCoroutine(Silah2Cekme());
                    StartCoroutine(Silah3Cekme());
                    www.Dispose();
                }
            }
        }
    }

    public void cevirme_Paneli_AC_btn()
    {
        Conver_Paneli.SetActive(true);
    }
    public void Karekter_Paneli_AC_btn()
    {
        marketpaneli.SetActive(true);
        Conver_Paneli.SetActive(false);
    }

    public void market_paneli()
    {  
        market_btnTxt.color = Color.red;
        marketpaneli.SetActive(true);
    }
    public void market_paneli_kapat()
    {
        marketpaneli.SetActive(false);
        Conver_Paneli.SetActive(false);
        market_btnTxt.color = Color.yellow;
    }

    public void bolum1_basla()
    {
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
            Time.timeScale = 1.0f;
            SceneManager.LoadScene("bolum2");
            bolum1 = false;
            Cursor.lockState = CursorLockMode.Locked;// fare imlecini kilidini
        }
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void gold1_satinal_btn()
    {
        altin1_satinalmapaneli.SetActive(true);
    }
    public void gold2_satinal_btn()
    {
        altin2_satinalmapaneli.SetActive(true);
    }
    public void elmas_satinal_btn()
    {
        elmas_satinalmapaneli.SetActive(true);
    }
}
